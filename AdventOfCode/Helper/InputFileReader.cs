using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helper
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
                yield return line;

            yield return string.Empty;
        }
    }
}
