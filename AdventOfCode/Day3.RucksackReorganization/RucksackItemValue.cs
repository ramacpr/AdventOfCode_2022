using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3.RucksackReorganization
{
    public static class RucksackItemValue
    {
        static Dictionary<char, int> itemValues = new Dictionary<char, int>(); 

        static bool isInitialized = false;
        public static int GetItemValue(char item)
        {
            if (!isInitialized)
            {
                Initialize();
                isInitialized = true;
            }

            int priority = -1;
            if (itemValues.TryGetValue(item, out priority) == false)
                return -1;
            return priority;
        }

        private static void Initialize()
        {
            int itemValue = 1; 
            for(int item = ((int)'a'); item <= (int)'z'; item++)            
                itemValues.Add((char)item, itemValue++);
            for (int item = ((int)'A'); item <= (int)'Z'; item++)
                itemValues.Add((char)item, itemValue++);
        }
    }
}
