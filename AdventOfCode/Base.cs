using AdventOfCode.DailyChallenge.Helper;

namespace AdventOfCode
{
    public class Base
    {
        public InputFileReader inputFileReader = null;

        public void SetStream(Days day)
        {
            inputFileReader = new InputFileReader(day);
        }


    }
}
