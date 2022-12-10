using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public class SupplyStackManager
    {
        Stack<char>[] stacks = null; 
        public SupplyStackManager()
        {
            InitStacks();
        }

        /*
                    [G] [W]         [Q]    
        [Z]         [Q] [M]     [J] [F]    
        [V]         [V] [S] [F] [N] [R]    
        [T]         [F] [C] [H] [F] [W] [P]
        [B] [L]     [L] [J] [C] [V] [D] [V]
        [J] [V] [F] [N] [T] [T] [C] [Z] [W]
        [G] [R] [Q] [H] [Q] [W] [Z] [G] [B]
        [R] [J] [S] [Z] [R] [S] [D] [L] [J]
         1   2   3   4   5   6   7   8   9 
        */
        private void InitStacks()
        {
            stacks = new Stack<char>[9];

            stacks[8] = new Stack<char>();
            stacks[8].Push('j');
            stacks[8].Push('b');
            stacks[8].Push('w');
            stacks[8].Push('v');
            stacks[8].Push('p');

            stacks[7] = new Stack<char>();
            stacks[7].Push('l');
            stacks[7].Push('g');
            stacks[7].Push('z');
            stacks[7].Push('d');
            stacks[7].Push('w');
            stacks[7].Push('r');
            stacks[7].Push('f');
            stacks[7].Push('q');

            stacks[6] = new Stack<char>();
            stacks[6].Push('d');
            stacks[6].Push('z');
            stacks[6].Push('c');
            stacks[6].Push('v');
            stacks[6].Push('f');
            stacks[6].Push('n');
            stacks[6].Push('j');

            stacks[5] = new Stack<char>();
            stacks[5].Push('s');
            stacks[5].Push('w');
            stacks[5].Push('t');
            stacks[5].Push('c');
            stacks[5].Push('h');
            stacks[5].Push('f');


            stacks[4] = new Stack<char>();
            stacks[4].Push('r');
            stacks[4].Push('q');
            stacks[4].Push('t');
            stacks[4].Push('j');
            stacks[4].Push('c');
            stacks[4].Push('s');
            stacks[4].Push('m');
            stacks[4].Push('w');

            stacks[3] = new Stack<char>();
            stacks[3].Push('z');
            stacks[3].Push('h');
            stacks[3].Push('n');
            stacks[3].Push('l');
            stacks[3].Push('f');
            stacks[3].Push('v');
            stacks[3].Push('q');
            stacks[3].Push('g');



            stacks[0] = new Stack<char>();
            stacks[0].Push('r');
            stacks[0].Push('g');
            stacks[0].Push('j');
            stacks[0].Push('b');
            stacks[0].Push('t');
            stacks[0].Push('v');
            stacks[0].Push('z');
            stacks[1] = new Stack<char>();
            stacks[1].Push('j');
            stacks[1].Push('r');
            stacks[1].Push('v');
            stacks[1].Push('l');
            stacks[2] = new Stack<char>();
            stacks[2].Push('s');
            stacks[2].Push('q');
            stacks[2].Push('f');


        }

        public string PeekAllStackData()
        {
            return stacks[0].Peek().ToString() +
                stacks[1].Peek().ToString() +
                stacks[2].Peek().ToString() +
                stacks[3].Peek().ToString() +
                stacks[4].Peek().ToString() +
                stacks[5].Peek().ToString() +
                stacks[6].Peek().ToString() +
                stacks[7].Peek().ToString() +
                stacks[8].Peek().ToString();
        }

        public void MoveStackData(int srcStackNum, int destStackNum, int moveCount)
        {
            while(stacks[srcStackNum - 1].Count > 0 && --moveCount >= 0)
            {
                var poppedItem = stacks[srcStackNum - 1].Pop();
                stacks[destStackNum - 1].Push(poppedItem);
            }
        }

        public void MoveStackData_9001(int srcStackNum, int destStackNum, int moveCount)
        {
            var tempStack = new Stack<char>(); 
            while (stacks[srcStackNum - 1].Count > 0 && --moveCount >= 0)
            {
                var poppedItem = stacks[srcStackNum - 1].Pop();
                tempStack.Push(poppedItem);      
            }

            while(tempStack.Count > 0)
            {
                var poppedItem = tempStack.Pop();
                stacks[destStackNum - 1].Push(poppedItem);
            }
        }
    }
}
