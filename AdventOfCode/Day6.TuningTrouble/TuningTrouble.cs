using System.Collections.Generic;

namespace AdventOfCode.DailyChallenge
{

    public class TuningTrouble : Base
    {
        public TuningTrouble()
        {
            SetStream(Helper.Days.day6); 
        }

        public int GetResult(int markerLength = 4)
        {
            int charsProcessed = -1;
            HashSet<char> uniqueSet = new HashSet<char>();
            Dictionary<char, int> charIndexMap = new Dictionary<char, int>(); 


            var line = inputFileReader.GetFirstLine().Trim();


            int startIndex = 0, curIndex = 0;
            int charCount = 0; 

            while(curIndex < line.Length)
            {
                if(charCount == 0)
                {
                    uniqueSet.Add(line[curIndex]);
                    charIndexMap.Add(line[curIndex], curIndex);
                    curIndex++;
                    charCount++;
                    continue;
                }
                else if(charCount == markerLength)
                {
                    // unique marker found!
                    return curIndex; 
                }


                if(uniqueSet.Contains(line[curIndex]))
                {
                    var newStartIndex = charIndexMap[line[curIndex]] + 1; 

                    while(startIndex < newStartIndex)
                    {
                        charIndexMap.Remove(line[startIndex]);
                        uniqueSet.Remove(line[startIndex]);
                        startIndex++;
                        charCount--;
                    }

                    uniqueSet.Add(line[curIndex]);
                    charIndexMap.Add(line[curIndex], curIndex);
                    charCount++;
                    curIndex++;

                }
                else
                {
                    uniqueSet.Add(line[curIndex]);
                    charIndexMap.Add(line[curIndex], curIndex);
                    charCount++;
                    curIndex++;
                }
            }

            return charsProcessed;
        }
    }
}
