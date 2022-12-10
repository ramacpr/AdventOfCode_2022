using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{

    public class TreeData
    {
        public TreeData(int height, bool isVisible = false)
        {
            Height = height;
            if(isVisible == true)
                BoundaryTree = true;
        }

        public int Height;
        public bool IsVisible => BoundaryTree || VisibleFromLeft || VisibleFromRight || VisibleFromTop || VisibleFromBottom;

        public bool BoundaryTree = false;
        public bool VisibleFromLeft = false;
        public bool VisibleFromRight = false; 
        public bool VisibleFromTop = false;
        public bool VisibleFromBottom = false;

        public int LeftScenicScore = 1;
        public int RightScenicScore = 1;
        public int TopScenicScore = 1;
        public int BottomScenicScore = 1;
        public int TreeScenicScore => LeftScenicScore * RightScenicScore * TopScenicScore * BottomScenicScore; 

    }
    public class TreeTop_TreeHouse : Base
    {
        int RowCount = 99;
        int ColCount = 99;
        TreeData[,] treeGrid = new TreeData[99, 99]; 
        public TreeTop_TreeHouse()
        {
            SetStream(Helper.Days.day8);
            InitializeTreeData();
        }

        private void InitializeTreeData()
        {
            int rowIndex = 0, colIndex = 0;
            foreach(var line in GetNextLine())
            {
                if (string.IsNullOrEmpty(line))
                    continue; 

                foreach(char c in line)
                {
                    int num = int.Parse(c.ToString());  
                    // the tree is always visible at the boundary
                    treeGrid[rowIndex, colIndex] = new TreeData(num, IsBoundaryTree(rowIndex, colIndex));
                    colIndex++;
                }
                colIndex = 0;
                rowIndex++;
            }
        }

        private void EvaluateScenicScore()
        {
            EvaluateLeftScenicScore();
            EvaluateRightScenicScore();
            EvaluateTopScenicScore();
            EvaluateBottomScenicScore();
        }

        private void EvaluateLeftScenicScore()
        {
            int scenicScore = 0; 
            for(int row = 0; row < RowCount; row++)
            {
                for(int col = ColCount - 1; col >= 0; col --)
                {
                    if(col == 0)
                    {
                        treeGrid[row, col].LeftScenicScore = 1;
                        continue; 
                    }
                    int index = col; 
                    while (index > 0)
                    {
                        if (scenicScore == 0)
                            scenicScore++;
                        else
                        {
                            if (treeGrid[row, index].Height < treeGrid[row, col].Height)
                                scenicScore++;
                            else
                                break;
                        }
                        index--;
                    }
                    treeGrid[row, col].LeftScenicScore = scenicScore;
                    scenicScore = 0;                     
                }
            }
        }

        private void EvaluateRightScenicScore()
        {
            int scenicScore = 0;
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col <= ColCount - 1; col++)
                {
                    if (col == ColCount - 1)
                    {
                        treeGrid[row, col].RightScenicScore = 1;
                        continue;
                    }
                    int index = col;
                    while (index < ColCount - 1)
                    {
                        if (scenicScore == 0)
                            scenicScore++;
                        else
                        {
                            if (treeGrid[row, index].Height < treeGrid[row, col].Height)
                                scenicScore++;
                            else
                                break;
                        }
                        index++;
                    }
                    treeGrid[row, col].RightScenicScore = scenicScore;
                    scenicScore = 0;
                }
            }
        }

        private void EvaluateTopScenicScore()
        {
            int scenicScore = 0; 
            for (int col = 0; col < ColCount; col++)
            {
                for (int row = 0; row <= RowCount - 1; row++)
                {
                    if (row == RowCount - 1)
                    {
                        treeGrid[row, col].TopScenicScore = 1;
                        continue;
                    }
                    int index = row;
                    while (index < RowCount - 1)
                    {
                        if (scenicScore == 0)
                            scenicScore++;
                        else
                        {
                            if (treeGrid[index, col].Height < treeGrid[row, col].Height)
                                scenicScore++;
                            else
                                break;
                        }
                        index++;
                    }
                    treeGrid[row, col].TopScenicScore = scenicScore;
                    scenicScore = 0;
                }
            }
        }

        private void EvaluateBottomScenicScore()
        {
            int scenicScore = 0;
            for (int col = 0; col < ColCount; col++)
            {
                for (int row = RowCount - 1; row >= 0; row--)
                {
                    if (row == 0)
                    {
                        treeGrid[row, col].BottomScenicScore = 1;
                        continue;
                    }
                    int index = row;
                    while (index > 0)
                    {
                        if (scenicScore == 0)
                            scenicScore++;
                        else
                        {
                            if (treeGrid[index, col].Height < treeGrid[row, col].Height)
                                scenicScore++;
                            else
                                break;
                        }
                        index--;
                    }
                    treeGrid[row, col].BottomScenicScore = scenicScore;
                    scenicScore = 0;
                }
            }
        }

        private void CheckTreeVisibility()
        {
            // using stacks to determine the trees that are visible from 
            // either of the 4 directions... 
            Stack<Tuple<int,int>> evaluationStack = new Stack<Tuple<int, int>>();

            // step 1: 
            // check for visibility from left and right
            // check tree heights row wise excluding 0th and nth row
            // as they are on boundary
            //
            // from left
            #region evaluate visibility from left
            for (int rowIndex = 1; rowIndex < RowCount - 1; rowIndex++)
            {
                for (int colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    // add items onto stack if and only if the top of
                    // the stack is lesser than the new value
                    if (evaluationStack.Count() == 0)// || treeGrid[rowIndex, colIndex].Height > evaluationStack.Peek()) // add the first item
                    {
                        evaluationStack.Push(new Tuple<int, int>(rowIndex, colIndex));
                        continue;
                    }
                    var stackTop = evaluationStack.Peek();
                    if (treeGrid[rowIndex, colIndex].Height > treeGrid[stackTop.Item1, stackTop.Item2].Height)
                        evaluationStack.Push(new Tuple<int, int>(rowIndex, colIndex));
                }

                while (evaluationStack.Count() > 0)
                {
                    var item = evaluationStack.Pop();
                    treeGrid[item.Item1, item.Item2].VisibleFromLeft = true;
                }
            }
            evaluationStack.Clear();
            #endregion

            // from right
            #region evaluate visibility from right
            for (int rowIndex = 1; rowIndex < RowCount - 1; rowIndex++)
            {
                for (int colIndex = ColCount - 1; colIndex >= 0; colIndex--)
                {
                    // add items onto stack if and only if the top of
                    // the stack is lesser than the new value
                    if (evaluationStack.Count() == 0)// || treeGrid[rowIndex, colIndex].Height > evaluationStack.Peek()) // add the first item
                    {
                        evaluationStack.Push(new Tuple<int, int>(rowIndex, colIndex));
                        continue;
                    }
                    var stackTop = evaluationStack.Peek();
                    if (treeGrid[rowIndex, colIndex].Height > treeGrid[stackTop.Item1, stackTop.Item2].Height)
                        evaluationStack.Push(new Tuple<int, int>(rowIndex, colIndex));
                }

                while (evaluationStack.Count() > 0)
                {
                    var item = evaluationStack.Pop();
                    treeGrid[item.Item1, item.Item2].VisibleFromRight = true;
                }
            }
            evaluationStack.Clear();
            #endregion

            // step 2: 
            // check for visibility from top and bottom
            // now check tree heights column wise excluding 0th
            // and nth column as they are on boundary
            //
            // from top
            #region evaluate visibility from top
            for (int colIndex = 1; colIndex < ColCount - 1; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < RowCount; rowIndex++)
                {
                    // add items onto stack if and only if the top of
                    // the stack is lesser than the new value
                    if (evaluationStack.Count() == 0)// || treeGrid[rowIndex, colIndex].Height > evaluationStack.Peek()) // add the first item
                    {
                        evaluationStack.Push(new Tuple<int, int>(rowIndex, colIndex));
                        continue;
                    }
                    var stackTop = evaluationStack.Peek();
                    if (treeGrid[rowIndex, colIndex].Height > treeGrid[stackTop.Item1, stackTop.Item2].Height)
                        evaluationStack.Push(new Tuple<int, int>(rowIndex, colIndex));
                }

                while (evaluationStack.Count() > 0)
                {
                    var item = evaluationStack.Pop();
                    treeGrid[item.Item1, item.Item2].VisibleFromTop = true;
                }
            }
            evaluationStack.Clear();
            #endregion

            // from bottom
            #region evaluate visibility from right
            for (int colIndex = 1; colIndex < ColCount - 1; colIndex++)
            {
                for (int rowIndex = RowCount - 1; rowIndex >= 0; rowIndex--)
                {
                    // add items onto stack if and only if the top of
                    // the stack is lesser than the new value
                    if (evaluationStack.Count() == 0)// || treeGrid[rowIndex, colIndex].Height > evaluationStack.Peek()) // add the first item
                    {
                        evaluationStack.Push(new Tuple<int, int>(rowIndex, colIndex));
                        continue;
                    }
                    var stackTop = evaluationStack.Peek();
                    if (treeGrid[rowIndex, colIndex].Height > treeGrid[stackTop.Item1, stackTop.Item2].Height)
                        evaluationStack.Push(new Tuple<int, int>(rowIndex, colIndex));
                }

                while (evaluationStack.Count() > 0)
                {
                    var item = evaluationStack.Pop();
                    treeGrid[item.Item1, item.Item2].VisibleFromBottom = true;
                }
            }
            evaluationStack.Clear();
            #endregion
        }

        private bool IsBoundaryTree(int rowIndex, int colIndex)
        {
            if (rowIndex == 0 || colIndex == 0 || rowIndex == RowCount - 1 || colIndex == ColCount - 1)
                return true;
            return false;
        }

        public int GetResult_A()
        {
            int visibleTrees = 0;

            CheckTreeVisibility();

            for (int rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                for(int colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    if (treeGrid[rowIndex, colIndex].IsVisible)
                        visibleTrees++;
                }
            }

            return visibleTrees; 
        }

        public int GetResult_B()
        {
            int maxScenicScore = 0;

            EvaluateScenicScore();

            for (int rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    var score = treeGrid[rowIndex, colIndex].TreeScenicScore;
                    if (score > maxScenicScore)
                        maxScenicScore = score;
                }
            }

            return maxScenicScore;
        }
    }
}
