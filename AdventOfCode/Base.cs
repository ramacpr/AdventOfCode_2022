using AdventOfCode.Solutions.Helper;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Base
    {
        public InputFileReader inputFileReader = null;

        public void SetStream(Days day)
        {
            inputFileReader = new InputFileReader(day);
        }

        public IEnumerable<string> GetNextLine()
        {
            foreach (var fileLine in inputFileReader.GetNextLine())
            {
                if (string.IsNullOrEmpty(fileLine))
                    continue;
                yield return fileLine;
            }
        }


    }
}
