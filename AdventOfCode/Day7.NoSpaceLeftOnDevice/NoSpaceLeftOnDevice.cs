using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DailyChallenge
{
    #region Data classes
    public interface INode
    {
        int CalculateNodeSize();
        string GetName();
        int GetSize();
        void SetSize(int size);
        IFolderNode GetParentNode();
    }

    public interface IFolderNode : INode
    {
        IFolderNode GetChildFolderNode(string folderName);
        INode AddChildNode(string name, IFolderNode parent, int size = -1);
        List<INode> GetChildrenNodes();
    }

    public class FileSystemNodeBase : INode
    {
        public IFolderNode ParentNode = null;
        public string Name;
        public int Size = 0;

        public string GetName() => Name;

        public void SetSize(int size) => Size = size;

        public IFolderNode GetParentNode() => ParentNode;

        virtual public int CalculateNodeSize() => Size;

        public int GetSize() => Size;
    }

    public class FileNode : FileSystemNodeBase
    {
        public FileNode(string fileName, int fileSize, IFolderNode parent)
        {
            ParentNode = parent;
            Name = fileName;
            Size = fileSize;
        }
    }

    public class FolderNode : FileSystemNodeBase, IFolderNode
    {
        public List<INode> Children = new List<INode>();
        public FolderNode(string fileName, IFolderNode parent)
        {
            ParentNode = parent;
            Name = fileName; 
        }

        public IFolderNode GetChildFolderNode(string folderName)
        {
            INode folderNode = null; 
            foreach(var childNode in Children)
            {
                if (!(childNode is IFolderNode))
                    continue; 
                if(childNode.GetName() == folderName)
                {
                    folderNode = childNode;
                    break;
                }    

            }
            // if no such folder is found.. create it 
            if(folderNode == null)            
                folderNode = AddChildNode(folderName, this);            

            return (IFolderNode)folderNode;
        }

        public override int CalculateNodeSize()
        {
            int totalSize = 0;
            foreach (var node in Children)
                totalSize += node.CalculateNodeSize();
            SetSize(totalSize);
            return totalSize;
        }

        public List<INode> GetChildrenNodes()
        {
            return Children;
        }

        // size will be -1 if child is a folder type
        public INode AddChildNode(string name, IFolderNode parent, int size = -1)
        {
            INode childNode = null;

            if (size == -1)
                childNode = new FolderNode(name, parent);
            else
                childNode = new FileNode(name, size, parent);

            Children.Add(childNode);
            return childNode;
        }
    } 
    #endregion
   
    public class NoSpaceLeftOnDevice : Base
    {
        int TotalSpace = 0;
        IFolderNode fileSystemRoot = null;
        IFolderNode currNode = null;
        public NoSpaceLeftOnDevice(int totalSystemSpace)
        {
            TotalSpace = totalSystemSpace;
            SetStream(Helper.Days.day7);
            foreach (var inputLine in GetNextLine())
            {
                ParseLine(inputLine);
            }
        }

        private void ParseLine(string line)
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
                                currNode = currNode.GetParentNode(); 
                            else
                                currNode = currNode.GetChildFolderNode(nodeName); 
                        }
                    }                
                }
                else if(cmdToProcess.StartsWith("ls"))
                {
                    // in case of 'ls' command, get ready to update the child nodes of the folderNode... nothing for now!
                }
                return; 
            }
            if(cmdToProcess.StartsWith("dir"))
            {
                // ex: dir <folder name>
                // new folder under the current folder
                cmdToProcess = cmdToProcess.Replace("dir", " ");
                cmdToProcess = cmdToProcess.Trim();

                currNode.AddChildNode(cmdToProcess, currNode as FolderNode); 
            }
            else if(Char.IsDigit(cmdToProcess[0])) // child file
            {
                // ex: 123 <filename>
                // this is a file under the current directory
                cmdToProcess = cmdToProcess.Trim();
                var fileData = cmdToProcess.Split(' ');

                currNode.AddChildNode(fileData[1], currNode as FolderNode, int.Parse(fileData[0]));
            }

        }

        private void UpdateFolderNodeSizes(FolderNode folderNode = null)
        {
            if (folderNode == null)
                folderNode = fileSystemRoot as FolderNode;

            folderNode.SetSize(folderNode.CalculateNodeSize());
        }

        public int GetResult_A(int sizeLimit, IFolderNode folderNode = null)
        {
            if(fileSystemRoot.GetSize() == 0)
            {
                // one time activity only
                UpdateFolderNodeSizes();
            }

            int totalSize = 0;
            // if folderNode is null, start from root
            if (folderNode == null)
                folderNode = fileSystemRoot; 

            if(folderNode is IFolderNode && folderNode.GetSize() <= sizeLimit) 
                totalSize += folderNode.GetSize();

            var folders = folderNode.GetChildrenNodes().Where(x => x is IFolderNode).ToList();
            foreach (var childNode in folders)
            {
                totalSize += GetResult_A(sizeLimit, childNode as FolderNode);
            }

            return totalSize; 
        }

        public int GetResult_B(int updateSize, IFolderNode folderNode = null, int? minSize = null, int sizeLimit = -1)
        {
            if (fileSystemRoot.GetSize() == 0)
            {
                // one time activity only
                UpdateFolderNodeSizes();
            }

            // if folderNode is null, start from root
            if (folderNode == null)
                folderNode = fileSystemRoot;

            if(sizeLimit == -1)
            {
                // calculate the size limit based on the available data!
                sizeLimit = updateSize - (TotalSpace - fileSystemRoot.GetSize());
            }

            if (folderNode is IFolderNode && folderNode.GetSize() >= sizeLimit &&
                (minSize == null || folderNode.GetSize() < minSize))
                minSize = folderNode.GetSize();

            foreach (var childNode in folderNode.GetChildrenNodes())
            {
                if(childNode is IFolderNode)
                    minSize = GetResult_B(updateSize, (IFolderNode)childNode, minSize, sizeLimit);
            }

            return (int)minSize;
        }


    }
}
