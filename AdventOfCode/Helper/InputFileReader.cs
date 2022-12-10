using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Solutions.Helper
{
    public class InputFileReader
    {
        StreamReader streamReader = null; 

        public InputFileReader(Days day)
        {
            InitReader(day);
        }
        
        void InitReader(Days day)
        {
            string filePath = "..\\..\\InputData\\" + day.ToString() + ".txt";
            streamReader = new StreamReader(filePath);
        }

        public IEnumerable<string> GetNextLine()
        {
            string line = "";

            while ((line = streamReader.ReadLine()) != null)            
                yield return line.Trim();

            streamReader.DiscardBufferedData();
            streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

            yield return string.Empty;
        }

        public string GetFirstLine()
        {
            return streamReader.ReadLine();
        }
    }
}
