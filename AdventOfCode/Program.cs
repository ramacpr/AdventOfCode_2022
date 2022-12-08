using AdventOfCode.DailyChallenge;
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

            #region Day 3: Rucksack re-organization
            //RucksackReorganization_A day3_prog = new RucksackReorganization_A();
            //Console.WriteLine(day3_prog.GetResult()); 
            //RucksackReorganization_B day3_prog_B = new RucksackReorganization_B();
            //Console.WriteLine(day3_prog_B.GetResult());
            #endregion

            #region Day 4: Camp cleanup
            //CampCleanup_A day4_prog_A = new CampCleanup_A();
            //Console.WriteLine(day4_prog_A.GetResult());
            //CampCleanup_B day4_prog_B = new CampCleanup_B();
            //Console.WriteLine(day4_prog_B.GetResult()); 
            #endregion

            #region Day 5: Supply stack
            //SupplyStack supplyStack_A = new SupplyStack();
            //Console.WriteLine(supplyStack_A.GetResult());
            //SupplyStack supplyStack_B = new SupplyStack();
            //Console.WriteLine(supplyStack_B.GetResult_9001()); 
            #endregion

            #region Day 6: Tuning Trouble
            //TuningTrouble tuningTrouble = new TuningTrouble();
            //Console.WriteLine(tuningTrouble.GetResult());
            //TuningTrouble tuningTrouble_B = new TuningTrouble();
            //Console.WriteLine(tuningTrouble_B.GetResult(14)); 
            #endregion

            #region Day 7: No space left on device
            //NoSpaceLeftOnDevice day7 = new NoSpaceLeftOnDevice(70000000);
            //Console.WriteLine(day7.GetResult_A(100000));
            //Console.WriteLine(day7.GetResult_B(30000000)); 
            #endregion

            TreeTop_TreeHouse day8 = new TreeTop_TreeHouse();
            Console.WriteLine(day8.GetResult_A());
            Console.WriteLine(day8.GetResult_B());

            Console.ReadKey(); 
        }
    }
}
