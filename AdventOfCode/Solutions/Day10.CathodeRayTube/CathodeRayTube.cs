using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public class CathodeRayTube : Base
    {
        int _defaultSigStCountVal = 20;
        int _sigStIncrementValue = 40;
        int _markerPoint = 20;

        Dictionary<int, int> _periodicSigStMap = new Dictionary<int, int>();
        int _registerX = 1; 

        public CathodeRayTube()
        {
            SetStream(Solutions.Helper.Days.day10);
        }

        public int GetResult_A()
        {
            _periodicSigStMap.Clear();
            _registerX = 1;

            int cycleCount = 0;
            _periodicSigStMap.Add(cycleCount, 1);
            foreach (var inputLine in GetNextLine())
            {
                string line = inputLine.Trim();
                var cmd = line.Split(' ');

                // NEW cycle start 
                cycleCount++;
                // - command start to execute

                // during the cycle 
                // - capture X value for the cycle count now
                _periodicSigStMap.Add(cycleCount, _registerX);

                // cycle ends
                // - cycle complete, so finish the job per command now
                if (cmd[0] == "noop") // nothing to do, just continue; 
                    continue; 
                else if(cmd[0].StartsWith("addx"))
                {
                    // add takes 2 cycles to comples 

                    // NEW cycle start 
                    cycleCount++;

                    // during the cycle 
                    // - capture X value for the cycle count now
                    _periodicSigStMap.Add(cycleCount, _registerX);

                    // cycle ends
                    // - cycle complete, so finish the job per command now
                    _registerX += int.Parse(cmd[1]);
                }
            }

            int result = 0;
            for (int i = _defaultSigStCountVal; i <= 220; i += +_sigStIncrementValue)
                result += (i * _periodicSigStMap[i]);

            return result; 
        }

        public void GetResult_B()
        {
            _periodicSigStMap.Clear();
            _registerX = 1;

            bool[,] matrix = new bool[6,40];
            for (int row = 0; row < 6; row++)
                for (int col = 0; col < 40; col++)
                    matrix[row, col] = false;

            int rowIndex = -1, colIndex = -1;

            int cycleCount = 0;
            _periodicSigStMap.Add(cycleCount, 1);
            foreach (var inputLine in GetNextLine())
            {
                string line = inputLine.Trim();
                var cmd = line.Split(' ');

                // NEW cycle start 
                cycleCount++;
                // - command start to execute

                // during the cycle 
                // - capture X value for the cycle count now
                _periodicSigStMap.Add(cycleCount, _registerX);

                // CRT draws a pixel at (row, col), if the registerX value coincides 
                // i.e, if col == registerX || col == registerX - 1 || col == registerX + 1
                colIndex = (colIndex + 1) % 40;
                if (colIndex == 0)
                    rowIndex++;
                if (colIndex == _registerX || colIndex == _registerX + 1 || colIndex == _registerX - 1)
                    matrix[rowIndex, colIndex] = true;

                // cycle ends
                // - cycle complete, so finish the job per command now
                if (cmd[0] == "noop") // nothing to do, just continue; 
                    continue;
                else if (cmd[0].StartsWith("addx"))
                {
                    // add takes 2 cycles to comples 

                    // NEW cycle start
                    cycleCount++;
                    _periodicSigStMap.Add(cycleCount, _registerX);

                    // CRT draws a pixel at (row, col), if the registerX value coincides 
                    // i.e, if col == registerX || col == registerX - 1 || col == registerX + 1
                    colIndex = (colIndex + 1) % 40;
                    if (colIndex == 0)
                        rowIndex++;
                    if (colIndex == _registerX || colIndex == _registerX + 1 || colIndex == _registerX - 1)
                        matrix[rowIndex, colIndex] = true;

                    // cycle ends
                    // - cycle complete, so finish the job per command now
                    _registerX += int.Parse(cmd[1]); // cmd execution complete for addx in 2 cycles
                }
            }

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 40; col++)
                {
                    if (matrix[row, col] == true)
                        Console.Write('#');
                    else
                        Console.Write('.');
                }
                Console.Write("\n");
            }
        }
    }
}
