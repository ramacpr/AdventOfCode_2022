namespace AdventOfCode.DailyChallenge
{
    public class SupplyStack : Base
    {
        SupplyStackManager mgr = new SupplyStackManager();
        public SupplyStack()
        {
            SetStream(Helper.Days.day5); 
        }

        public string GetResult()
        {
            foreach(var inputLine in inputFileReader.GetNextLine())
            {
                var line = inputLine.Trim();

                if (line.StartsWith("#") || string.IsNullOrEmpty(line))
                    continue;

                var itemizedLine = line.Split(' ');

                var moveCount = int.Parse(itemizedLine[1]);
                var srcStack = int.Parse(itemizedLine[3]);
                var destStack = int.Parse(itemizedLine[5]);

                mgr.MoveStackData(srcStack, destStack, moveCount); 
            }

            return mgr.PeekAllStackData();
        }

        public string GetResult_9001()
        {
            foreach (var inputLine in inputFileReader.GetNextLine())
            {
                var line = inputLine.Trim();

                if (line.StartsWith("#") || string.IsNullOrEmpty(line))
                    continue;

                var itemizedLine = line.Split(' ');

                var moveCount = int.Parse(itemizedLine[1]);
                var srcStack = int.Parse(itemizedLine[3]);
                var destStack = int.Parse(itemizedLine[5]);

                mgr.MoveStackData_9001(srcStack, destStack, moveCount);
            }

            return mgr.PeekAllStackData();
        }
    }
}
