using AdventOfCode.Solutions.Helper;

namespace AdventOfCode.Solutions
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
