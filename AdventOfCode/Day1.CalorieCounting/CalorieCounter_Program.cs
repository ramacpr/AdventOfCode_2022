using AdventOfCode.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1.CalorieCounting
{
    public class CalorieCounter_Program
    {
        InputFileReader inputFileReader = null; 

        public CalorieCounter_Program()
        {
            inputFileReader = new InputFileReader(Days.day1);
        }

        public int GetResult()
        {
            int maxCalories = 0;
            int sum = 0; 

            foreach(var itemLine in inputFileReader.GetNextLine())
            {
                if(string.IsNullOrEmpty(itemLine))
                {
                    if (sum > maxCalories)
                        maxCalories = sum;
                    sum = 0;
                }
                else
                    sum += int.Parse(itemLine);
            }

            return maxCalories;
        }
    }
}
