using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public class RucksackReorganization_B : Base
    {
        public RucksackReorganization_B()
        {
            SetStream(Helper.Days.day3);
        }

        public int GetResult()
        {
            int prioritySum = 0;

            HashSet<char> ruckSack_1 = new HashSet<char>();
            HashSet<char> ruckSack_2 = new HashSet<char>();
            HashSet<char> ruckSack_3 = new HashSet<char>();

            int groupCount = -1;
            string[] group = new string[3];
            foreach (var itemLine in inputFileReader.GetNextLine())
            {
                groupCount++;
                group[groupCount] = itemLine.Trim();
                if (groupCount < 2)
                {
                        continue;
                }


                int g1_index = 0, g2_index = 0, g3_index = 0;

                while(g1_index < group[0].Length || g2_index < group[1].Length || g3_index < group[2].Length)
                {
                    if (g1_index < group[0].Length)
                        ruckSack_1.Add(group[0][g1_index]);

                    if (g2_index < group[1].Length)
                        ruckSack_2.Add(group[1][g2_index]);

                    if (g3_index < group[2].Length)
                        ruckSack_3.Add(group[2][g3_index]);


                    if (g1_index < group[0].Length &&
                        ruckSack_1.Contains(group[0][g1_index]) && ruckSack_2.Contains(group[0][g1_index]) && ruckSack_3.Contains(group[0][g1_index]))
                    {
                        prioritySum += RucksackItemValue.GetItemValue(group[0][g1_index]);

                        ruckSack_1.Clear();
                        ruckSack_2.Clear();
                        ruckSack_3.Clear();
                        groupCount = -1;
                        break;
                    }

                    else if (g2_index < group[1].Length &&
                        ruckSack_1.Contains(group[1][g2_index]) && ruckSack_2.Contains(group[1][g2_index]) && ruckSack_3.Contains(group[1][g2_index]))
                    {
                        prioritySum += RucksackItemValue.GetItemValue(group[1][g2_index]);

                        ruckSack_1.Clear();
                        ruckSack_2.Clear();
                        ruckSack_3.Clear();
                        groupCount = -1;
                        break;
                    }

                    else if (g3_index < group[2].Length &&
                        ruckSack_1.Contains(group[2][g3_index]) && ruckSack_2.Contains(group[2][g3_index]) && ruckSack_3.Contains(group[2][g3_index]))
                    {
                        prioritySum += RucksackItemValue.GetItemValue(group[2][g3_index]);

                        ruckSack_1.Clear();
                        ruckSack_2.Clear();
                        ruckSack_3.Clear();
                        groupCount = -1;
                        break;
                    }

                    g1_index++;
                    g2_index++;
                    g3_index++;
                }

                groupCount = -1;
            }

            return prioritySum;
        }
    }
}
