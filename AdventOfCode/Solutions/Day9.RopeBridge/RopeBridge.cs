using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    public enum Direction
    {
        none = 0,
        Left,
        Right, 
        Up, 
        Down
    }

    public class NodeLocation
    {
        public NodeLocation(int rowIndex, int colIndex)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex; 
        }
        public int RowIndex;
        public int ColIndex;
    }

    public class NodeInfo
    {
        public int HVisitCount = 0;
        public int TVisitCount = 0; 
        public NodeInfo(int hvisitCount = 0, int tvisitCount = 0)
        {
            HVisitCount = hvisitCount;
            TVisitCount = tvisitCount;
        }
    }
    public class RopeBridge : Base
    {
        //Node HeadNode = new Node(true, true);
        Dictionary<NodeLocation, NodeInfo> moveMap = new Dictionary<NodeLocation, NodeInfo>();
        NodeLocation currHNodeLoc = null, prevHNodeLoc = null;
        NodeLocation currTNodeLoc = null;
        Direction prevDirection = Direction.none; 
        public RopeBridge()
        {
            SetStream(Helper.Days.day9);

            // to start with HT overlap at (0, 0)
            currHNodeLoc = new NodeLocation(0, 0);
            currTNodeLoc = currHNodeLoc;
            moveMap.Add(currHNodeLoc, new NodeInfo(1, 1));

            Init();
        }

        private Direction GetDirection(string dir)
        {
            switch(dir)
            {
                case "u": return Direction.Up;
                case "d": return Direction.Down;
                case "l": return Direction.Left;
                case "r": return Direction.Right;
                default: return Direction.none;
            }
        }

        private void Init()
        {
            foreach (var lineInput in GetNextLine())
            {
                var line = lineInput.Trim();
                string[] moveData = line.Split(' ');
                MoveHNode(GetDirection(moveData[0].ToLower()), int.Parse(moveData[1]));
            }
        }
        public int GetResult()
        {           
            var cnt = moveMap.Where(x => x.Value.TVisitCount >= 1).ToList();

            return cnt.Count(); 
        }

        private void GetNewHLocation(NodeLocation hNode, Direction direction, out int newRowLoc, out int newColLoc)
        {
            newRowLoc = 0;
            newColLoc = 0;
            switch (direction)
            {
                case Direction.Up:
                    newRowLoc = hNode.RowIndex - 1;
                    newColLoc = hNode.ColIndex;
                    break;
                case Direction.Down:
                    newRowLoc = hNode.RowIndex + 1;
                    newColLoc = hNode.ColIndex;
                    break;
                case Direction.Left:
                    newRowLoc = hNode.RowIndex;
                    newColLoc = hNode.ColIndex - 1;
                    break;
                case Direction.Right:
                    newRowLoc = hNode.RowIndex;
                    newColLoc = hNode.ColIndex + 1;
                    break; 
            }            
        }
        
        private int Distance(NodeLocation h, NodeLocation t)
        {
            return (int)Math.Floor(Math.Sqrt((Math.Pow(h.RowIndex - t.RowIndex, 2) + Math.Pow(h.ColIndex - t.ColIndex, 2))));
        }
        private bool AreAtValidDistance(NodeLocation hNode, NodeLocation tNode)
        {
            int dist = Distance(hNode, tNode);
            if (dist == 1 || dist == 0)
                return true;
            return false;
        }
        private void MoveHNode(Direction direction, int count)
        {
            int remaingMoves = count; 
            while(remaingMoves > 0)
            {
                int newRowLoc = 0, newColLoc = 0;
                GetNewHLocation(currHNodeLoc, direction, out newRowLoc, out newColLoc);

                // if this location already present increment Hvisit count by 1
                var node = moveMap.Where(x => x.Key.RowIndex == newRowLoc && x.Key.ColIndex == newColLoc).ToList();
                if (node.Count() > 0)
                {
                    node[0].Value.HVisitCount++;
                    prevHNodeLoc = currHNodeLoc;
                    currHNodeLoc = node[0].Key; 
                }
                else // create new node 
                {
                    prevHNodeLoc = currHNodeLoc;
                    currHNodeLoc = new NodeLocation(newRowLoc, newColLoc);
                    moveMap.Add(currHNodeLoc, new NodeInfo(1, 0)); 
                }

                // if H moved in a different direction for the first time, do not move T!
                if(prevDirection != Direction.none && direction != prevDirection)
                {
                    prevDirection = direction;
                }
                else
                {
                    if(AreAtValidDistance(currHNodeLoc, currTNodeLoc) == false)
                    {
                        currTNodeLoc = prevHNodeLoc;
                        moveMap[currTNodeLoc].TVisitCount++; 
                    }
                }                

                remaingMoves--;
            }
        }

    }
}
