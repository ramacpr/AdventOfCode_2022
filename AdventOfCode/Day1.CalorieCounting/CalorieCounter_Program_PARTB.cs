using AdventOfCode.DailyChallenge.Helper;

namespace AdventOfCode.DailyChallenge
{
    public class CalorieCounter_Program_PARTB
    {
        InputFileReader inputFileReader = null;        

        public CalorieCounter_Program_PARTB()
        {
            inputFileReader = new InputFileReader(Days.day1);
        }

        public int GetResult()
        {
            int? maxCalories_1 = null, maxCalories_2 = null, maxCalories_3 = null;
            int sum = 0;

            foreach (var itemLine in inputFileReader.GetNextLine())
            {
                if (string.IsNullOrEmpty(itemLine))
                {                    
                    if(maxCalories_1 == null || sum > maxCalories_1)
                    {
                        maxCalories_3 = maxCalories_2;
                        maxCalories_2 = maxCalories_1;
                        maxCalories_1 = sum;
                    }
                    else if(maxCalories_2 == null || sum > maxCalories_2)
                    {
                        maxCalories_3 = maxCalories_2;
                        maxCalories_2 = sum; 
                    }
                    else if(maxCalories_3 == null || sum > maxCalories_3)
                    {
                        maxCalories_3 = sum;
                    }

                    sum = 0;
                }
                else
                    sum += int.Parse(itemLine);
            }


            return (int)maxCalories_3 + (int)maxCalories_2 + (int)maxCalories_1;
        }
    }
}
