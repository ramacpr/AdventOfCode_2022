using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DailyChallenge
{
    #region File system data
    public interface IFileSystemNode
    {
        int UpdateSize();
        bool IsFolder();

        string GetName(); 
    }

    public class FileSystemNodeBase
    {
        public FolderNode ParentNode = null;
        public string Name;
        public int Size = 0;

        public string GetName() => Name;
    }

    public class FileNode : FileSystemNodeBase, IFileSystemNode
    {
        public FileNode(string fileName, int fileSize, FolderNode parent)
        {
            ParentNode = parent;
            Name = fileName;
            Size = fileSize;
        }
        public int UpdateSize() => Size;

        public bool IsFolder() => false;
    }

    public class FolderNode : FileSystemNodeBase, IFileSystemNode
    {
        public List<IFileSystemNode> Children = new List<IFileSystemNode>();
        public FolderNode(string fileName, FolderNode parent)
        {
            ParentNode = parent;
            Name = fileName; 
        }

        public bool IsFolder() => true;

        // size will be -1 if child is a folder type
        public IFileSystemNode AddChildNode(string name, FolderNode parent, int size = -1)
        {
            IFileSystemNode childNode = null;

            if (size == -1)// folder 
                childNode = new FolderNode(name, parent);
            else
                childNode = new FileNode(name, size, parent);

            Children.Add(childNode);
            return childNode; 
        }

        public FolderNode GetChildFolderNode(string folderName)
        {
            FolderNode folderNode = null; 
            foreach(var childNode in Children)
            {
                if (!childNode.IsFolder())
                    continue; 
                if(childNode.GetName() == folderName)
                {
                    folderNode = (FolderNode)childNode;
                    break;
                }    

            }
            // if no such folder is found.. create it 
            if(folderNode == null)            
                folderNode = AddChildNode(folderName, this) as FolderNode;            

            return folderNode;
        }

        public int UpdateSize()
        {
            int totalSize = 0;
            foreach (var node in Children)
                totalSize += node.UpdateSize();
            Size = totalSize;
            return totalSize;
        }
    } 
    #endregion
   
    
    public class NoSpaceLeftOnDevice : Base
    {
        int TotalSpace = 0;

        IFileSystemNode fileSystemRoot = null;
        IFileSystemNode currNode = null;
        public NoSpaceLeftOnDevice(int totalSystemSpace)
        {
            TotalSpace = totalSystemSpace;
            SetStream(Helper.Days.day7);
            foreach (var inputLine in GetNextLine())
            {
                ProcessLine(inputLine);
            }
        }

        private void ProcessLine(string line)
        {           
            if (string.IsNullOrEmpty(line))
                return;

            var cmdToProcess = line;
            if (cmdToProcess.StartsWith("$"))
            {
                // ex: $cd <something> or $ls 

                cmdToProcess = cmdToProcess.Replace('$', ' ');
                cmdToProcess = cmdToProcess.Trim();                

                
                if(cmdToProcess.StartsWith("cd"))
                {
                    // everytime there is a new 'cd' command,
                    // create the new node under the parent node and 
                    // update the folderNode reference to point 
                    // to the new node.
                    cmdToProcess = cmdToProcess.Replace("cd", " ");
                    var dirToNavigateTo = cmdToProcess.Trim();

                    // check if the folder navigation is possible, 
                    // i.e, if folder already present as one of the child nodes
                    // if at any point folder not present create it!

                    if(dirToNavigateTo == "/") // directly navigate to root node
                    {
                        // if root node not already created, create now!
                        if(fileSystemRoot == null)
                            fileSystemRoot = new FolderNode(dirToNavigateTo, null);
                        currNode = fileSystemRoot;
                    }
                    else
                    {
                        // the following are valid navigations 
                        // cd .. (jump to first level parent) 
                        // cd ../.. (jump bacl 2 levels of parent)
                        // cd <xyz> -> jump to child nade whose name is xyz
                        // cd xyz/abc jump to grandchild node abc under child node xyz
                        var navigateNodes = dirToNavigateTo.Split('/'); 
                        foreach(var nodeName in navigateNodes)
                        {
                            if (nodeName == "..")
                                currNode = ((FolderNode)currNode).ParentNode; 
                            else
                            {
                                currNode = ((FolderNode)currNode).GetChildFolderNode(nodeName); 
                            }
                        }
                    }                
                }
                else if(cmdToProcess.StartsWith("ls"))
                {
                    // in case of 'ls' command, get ready to update the child nodes of the folderNode
                }
                return; 
            }
            if(cmdToProcess.StartsWith("dir"))
            {
                // ex: dir <folder name>
                // new folder under the current folder
                cmdToProcess = cmdToProcess.Replace("dir", " ");
                cmdToProcess = cmdToProcess.Trim();

                ((FolderNode)currNode).AddChildNode(cmdToProcess, currNode as FolderNode); 
            }
            else if(Char.IsDigit(cmdToProcess[0])) // child file
            {
                // ex: 123 <filename>
                // this is a file under the current directory
                cmdToProcess = cmdToProcess.Trim();
                var fileData = cmdToProcess.Split(' ');

                ((FolderNode)currNode).AddChildNode(fileData[1], currNode as FolderNode, int.Parse(fileData[0]));
            }

        }

        private void UpdateFolderNodeSizes(FolderNode folderNode = null)
        {
            if (folderNode == null)
                folderNode = fileSystemRoot as FolderNode;

            folderNode.Size = folderNode.UpdateSize(); 
        }

        public int GetResult_A(int sizeLimit, FolderNode folderNode = null)
        {
            if(((FolderNode)fileSystemRoot).Size == 0)
            {
                // one time activity only
                UpdateFolderNodeSizes();
            }

            int totalSize = 0;
            // if folderNode is null, start from root
            if (folderNode == null)
                folderNode = fileSystemRoot as FolderNode; 

            if(folderNode.IsFolder() && folderNode.Size <= sizeLimit) 
                totalSize += folderNode.Size;

            var folders = ((FolderNode)folderNode).Children.Where(x => x.IsFolder()).ToList();
            foreach (var childNode in folders)
            {
                totalSize += GetResult_A(sizeLimit, childNode as FolderNode);
            }

            return totalSize; 
        }

        public int GetResult_B(int updateSize, FolderNode folderNode = null, int? minSize = null, int sizeLimit = -1)
        {
            if (((FolderNode)fileSystemRoot).Size == 0)
            {
                // one time activity only
                UpdateFolderNodeSizes();
            }

            // if folderNode is null, start from root
            if (folderNode == null)
                folderNode = fileSystemRoot as FolderNode;

            if(sizeLimit == -1)
            {
                // calculate the size limit based on the available data!
                sizeLimit = updateSize - (TotalSpace - ((FolderNode)fileSystemRoot).Size);
            }

            if (folderNode.IsFolder() && folderNode.Size >= sizeLimit &&
                (minSize == null || folderNode.Size < minSize))
                minSize = folderNode.Size;

            var folders = ((FolderNode)folderNode).Children.Where(x => x.IsFolder()).ToList();
            foreach (var childNode in folders)
            {
                minSize = GetResult_B(updateSize, childNode as FolderNode, minSize, sizeLimit);
            }

            return (int)minSize;
        }


    }
}
