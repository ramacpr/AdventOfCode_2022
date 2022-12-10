using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    public class NodeInfoEx
    {
        public int HVisitCount = 0;
        public int[] TailVisitCount = new int[9];
        public NodeInfoEx(int hvisitCount = 0)
        {
            HVisitCount = hvisitCount;
        }
    }
    public enum FollowerID
    {
        follower_1 = 0,
        follower_2,
        follower_3,
        follower_4,
        follower_5,
        follower_6,
        follower_7,
        follower_8,
        follower_9
    }

    public class RopeBridge_B : Base
    {
        Dictionary<NodeLocation, NodeInfoEx> moveMap = new Dictionary<NodeLocation, NodeInfoEx>();
        NodeLocation currHNodeLoc = null, prevHNodeLoc = null;
        NodeLocation[] currTNodeLoc = new NodeLocation[9];

        public RopeBridge_B()
        {
            SetStream(Helper.Days.day9);

            // to start with HT overlap at (0, 0)
            currHNodeLoc = new NodeLocation(0, 0);
            var info = new NodeInfoEx(1);
            for (int i = 0; i < 9; i++)
                info.TailVisitCount[i] = 1;

            moveMap.Add(currHNodeLoc, info);

            for (int i = 0; i < 9; i++)
                currTNodeLoc[i] = new NodeLocation(0, 0);

            Init();
        }

        private Direction GetDirection(string dir)
        {
            switch (dir)
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
        public int GetResult() => moveMap.Values.Where(x => x.TailVisitCount[(int)FollowerID.follower_9] >= 1).ToList().Count();

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
        
        private void MoveHNode(Direction direction, int count)
        {
            int remaingMoves = count;
            while (remaingMoves > 0)
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
                    moveMap.Add(currHNodeLoc, new NodeInfoEx(1));
                }

                AdjustTailLoc();

                remaingMoves--;
            }
        }

        private int Distance(NodeLocation h, NodeLocation t)
        {
            return (int)Math.Floor(Math.Sqrt((Math.Pow(h.RowIndex - t.RowIndex, 2) + Math.Pow(h.ColIndex - t.ColIndex, 2))));
        }

        private void AdjustTailLoc(FollowerID? headID = null, FollowerID tailID = FollowerID.follower_1)
        {
            if ((int)tailID >= 9)
                return; 

            NodeLocation headNodeLoc = (headID == null) ? currHNodeLoc : currTNodeLoc[(int)headID];
            NodeLocation tailNodeLoc = currTNodeLoc[(int)tailID];

            int rowDist = headNodeLoc.RowIndex - currTNodeLoc[(int)tailID].RowIndex;
            if (rowDist < 0)
                rowDist *= -1;
            int colDist = headNodeLoc.ColIndex - currTNodeLoc[(int)tailID].ColIndex;
            if (colDist < 0)
                colDist *= -1;

            var dist = Distance(headNodeLoc, tailNodeLoc);
            // if distance in within limit, no need to do anything! 
            if (dist == 0 || dist == 1)
                return;

            int newTailRowIndex = tailNodeLoc.RowIndex, newTailColIndex = tailNodeLoc.ColIndex;
            #region Calculate the new location of tail node
            // now check how apart the rows and columns are 
            // 
            // if both row and col are apart... 
            if (rowDist > 1 && colDist > 1)
            {
                if (currTNodeLoc[(int)tailID].RowIndex < headNodeLoc.RowIndex)
                    newTailRowIndex = headNodeLoc.RowIndex - 1;
                else
                    newTailRowIndex = headNodeLoc.RowIndex + 1;

                if (currTNodeLoc[(int)tailID].ColIndex < headNodeLoc.ColIndex)
                    newTailColIndex = headNodeLoc.ColIndex - 1;
                else
                    newTailColIndex = headNodeLoc.ColIndex + 1;
            }

            // if only row is apart
            else if (rowDist > 1)
            {
                if (currTNodeLoc[(int)tailID].RowIndex < headNodeLoc.RowIndex)
                    newTailRowIndex = headNodeLoc.RowIndex - 1;
                else
                    newTailRowIndex = headNodeLoc.RowIndex + 1;
                newTailColIndex = headNodeLoc.ColIndex;
            }

            // if only col is apart 
            else if (colDist > 1)
            {
                newTailRowIndex = headNodeLoc.RowIndex;
                if (currTNodeLoc[(int)tailID].ColIndex < headNodeLoc.ColIndex)
                    newTailColIndex = headNodeLoc.ColIndex - 1;
                else
                    newTailColIndex = headNodeLoc.ColIndex + 1;
            }
            #endregion

            currTNodeLoc[(int)tailID].RowIndex = newTailRowIndex;
            currTNodeLoc[(int)tailID].ColIndex = newTailColIndex;


            var node = moveMap.Where(x => x.Key.RowIndex == newTailRowIndex && x.Key.ColIndex == newTailColIndex).ToList();
            if (node.Count() > 0)
            {
                node[0].Value.TailVisitCount[(int)tailID] += 1;
            }
            else // create new node 
            {
                var newTailNode = new NodeLocation(newTailRowIndex, newTailColIndex);
                var newNodeInfo = new NodeInfoEx(0);
                newNodeInfo.TailVisitCount[(int)tailID] += 1;
                moveMap.Add(newTailNode, newNodeInfo);

                currTNodeLoc[(int)tailID].RowIndex = newTailRowIndex;
                currTNodeLoc[(int)tailID].ColIndex = newTailColIndex;
            }
            
            AdjustTailLoc(tailID, tailID + 1);
        }
    }
}
