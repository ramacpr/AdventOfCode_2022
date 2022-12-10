using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public class RucksackReorganization_A : Base
    {
        public RucksackReorganization_A()
        {
            SetStream(Helper.Days.day3);
        }

        public int GetResult()
        {
            int prioritySum = 0; 
            HashSet<char> compartment1 = new HashSet<char>();
            HashSet<char> compartment2 = new HashSet<char>();
            foreach (var itemLine in inputFileReader.GetNextLine())
            {
                string line = itemLine.Trim();
                int startIndex = 0, endIndex = line.Length - 1, midIndex = (line.Length / 2);
                int c1_index = startIndex, c2_index = midIndex;

                compartment1.Clear();
                compartment2.Clear();

                while(c1_index >= 0 && c1_index < midIndex &&
                    c2_index >= midIndex && c2_index <= endIndex)
                {
                    char item1 = line[c1_index];
                    char item2 = line[c2_index];

                    compartment1.Add(item1);
                    compartment2.Add(item2);

                    if (compartment1.Contains(item2))
                    {
                        prioritySum += RucksackItemValue.GetItemValue(item2);
                        break;
                    }
                    else if(compartment2.Contains(item1))
                    {
                        prioritySum += RucksackItemValue.GetItemValue(item1);
                        break;
                    }

                    c1_index++;
                    c2_index++;
                }

            }

            return prioritySum;
        }
    }
}
