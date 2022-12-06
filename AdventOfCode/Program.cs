using AdventOfCode.Day1.CalorieCounting;
using AdventOfCode.Day2.RockPaperScissors;
using AdventOfCode.Day3.RucksackReorganization;
using AdventOfCode.Day4.CampCleanup;
using AdventOfCode.Day5.SupplyStacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            #region day 1: Calorie Counter
            //var day1_prog = new CalorieCounter_Program();
            //Console.WriteLine(day1_prog.GetResult());

            //var day1_prog_B = new CalorieCounter_Program_PARTB();
            //Console.WriteLine(day1_prog_B.GetResult()); 
            #endregion

            #region day2: Rock Paper Scissors
            //RockPaperScissors day2_prog = new RockPaperScissors();
            //Console.WriteLine(day2_prog.GetResult());

            //RockPaperScissors_PartB day2_prog_B = new RockPaperScissors_PartB();
            //Console.WriteLine(day2_prog_B.GetResult()); 
            #endregion

            #region Rucksack re-organization
            //RucksackReorganization_A day3_prog = new RucksackReorganization_A();
            //Console.WriteLine(day3_prog.GetResult()); 
            #endregion

            #region Day 4: Camp cleanup
            //CampCleanup_A day4_prog_A = new CampCleanup_A();
            //Console.WriteLine(day4_prog_A.GetResult());
            //CampCleanup_B day4_prog_B = new CampCleanup_B();
            //Console.WriteLine(day4_prog_B.GetResult()); 
            #endregion

            SupplyStack supplyStack_A = new SupplyStack();
            Console.WriteLine(supplyStack_A.GetResult());
            SupplyStack supplyStack_B = new SupplyStack();
            Console.WriteLine(supplyStack_B.GetResult_9001());

            Console.ReadKey(); 
        }
    }
}
