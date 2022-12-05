using AdventOfCode.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
