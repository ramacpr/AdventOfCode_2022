namespace AdventOfCode.DailyChallenge
{
    public class CampCleanup_A : Base
    {
        public CampCleanup_A()
        {
            SetStream(Helper.Days.day4); 
        }

        public int GetResult()
        {
            int fullOverlapCount = 0; 

            foreach (var itemLine in inputFileReader.GetNextLine())
            {
                if (string.IsNullOrEmpty(itemLine))
                    continue;

                var pairData = itemLine.Trim().Split(',');
                var elf1Data = pairData[0].Split('-');
                var elf2Data = pairData[1].Split('-');

                var x1 = int.Parse(elf1Data[0]);
                var y1 = int.Parse(elf1Data[1]);

                var x2 = int.Parse(elf2Data[0]);
                var y2 = int.Parse(elf2Data[1]);

                if (x1 <= x2 && y1 >= y2 ||
                    x2 <= x1 && y2 >= y1)
                    fullOverlapCount++;
            }

            return fullOverlapCount; 
        }
    }
}
