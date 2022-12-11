using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Monkey
    {
        public int MonkeyID;
        public Queue<long> Items = new Queue<long>();
        public string Operation1;        
        public long ConditionTestDivisibilityBy;
        public int TestResult_TrueMonkeyID;
        public int TestResult_FalseMonkeyID;
        public long ActivityScore = 0;

        public Monkey(int monkeyID) => MonkeyID = monkeyID;

        public long DoOp_A() => DoOp_A() / 3;

        public long DoOp_B(long newWorryControlFactor) => DoOp() % newWorryControlFactor;

        public int GetNextMonkeyID(long newWorryLevel) => newWorryLevel % ConditionTestDivisibilityBy == 0 ? TestResult_TrueMonkeyID : TestResult_FalseMonkeyID;

        private long DoOp()
        {
            ActivityScore++;
            long worryLevel = Items.Dequeue();
            long left, right;
            long newWorryLevel = 0;

            Operation1 = Operation1.Replace("new", "").Replace("=", "").Replace("operation", "").Trim();
            var op1 = Operation1.Split(' ');

            left = (op1[0] == "old") ? worryLevel : long.Parse(op1[0]);
            right = (op1[2] == "old") ? worryLevel : long.Parse(op1[2]);
            if (op1[op1.Length - 2] == "+")
                newWorryLevel = left + right;
            else if (op1[op1.Length - 2] == "*")
                newWorryLevel = left * right; ;

            return newWorryLevel;
        }
    }
    public class MonkeyInTheMiddle : Base
    {
        Dictionary<int, Monkey> monkeyMap = new Dictionary<int, Monkey>();
        int MonkeyCount = 0; 

        public MonkeyInTheMiddle()
        {
            SetStream(Helper.Days.day11);          
        }

        public long GetResult_A()
        {
            UpdateMonkeyData();

            int totalRounds = 20;
            for (int round = 0; round < totalRounds; round++)
            {
                for (int monkeyID = 0; monkeyID < MonkeyCount; monkeyID++)
                {
                    var monkey = monkeyMap[monkeyID];
                    while (monkey.Items.Count > 0)
                    {
                        long newWorryLevel = monkey.DoOp_A();
                        int throwToMonkeyID = monkey.GetNextMonkeyID(newWorryLevel);
                        monkeyMap[throwToMonkeyID].Items.Enqueue(newWorryLevel);
                    }
                }
            }       
            return CalculateMonkeyBusiness();
        }

        public long GetResult_B()
        {
            UpdateMonkeyData();

            // we cannot control our worries by dividing by 3 anymore... 
            // this has to be calculated here.. based on the
            // existing 'divisor' data we have on each monkey (as read from file)
            // let this be an aggregate values of all of the Monkey.ConditionTestDivisibilityBy values
            long newWorryControlFactor = monkeyMap.Values.Aggregate(1, (mod, monkey) => (int)(mod * monkey.ConditionTestDivisibilityBy));

            int totalRounds = 10000;
            for (int round = 0; round < totalRounds; round++)
            {
                for (int monkeyID = 0; monkeyID < MonkeyCount; monkeyID++)
                {
                    var monkey = monkeyMap[monkeyID];
                    while (monkey.Items.Count > 0)
                    {
                        long newWorryLevel = monkey.DoOp_B(newWorryControlFactor);
                        int throwToMonkeyID = monkey.GetNextMonkeyID(newWorryLevel);

                        var monkeyToThrowTo = monkeyMap[throwToMonkeyID];
                        monkeyToThrowTo.Items.Enqueue(newWorryLevel);
                    }
                }
            }
            return CalculateMonkeyBusiness();
        }

        void UpdateMonkeyData()
        {
            monkeyMap.Clear();
            MonkeyCount = 0;

            Monkey currentMonkey = null;
            foreach (var inputLine in GetNextLine())
            {
                string line = inputLine.Replace(':', ' ').Replace(',', ' ').Trim().ToLower();
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.StartsWith("monkey"))
                {
                    var splitLine = line.Split(' ');
                    int monkeyID = int.Parse(splitLine[1]);

                    if (!monkeyMap.TryGetValue(monkeyID, out currentMonkey))
                    {
                        currentMonkey = new Monkey(monkeyID);
                        monkeyMap.Add(monkeyID, currentMonkey);
                    }

                }
                else if (line.StartsWith("starting items"))
                {
                    var splitLine = line.Split(' ');
                    foreach (var worryLevel in splitLine)
                    {
                        int itemWorryLevel = 0;
                        if (int.TryParse(worryLevel, out itemWorryLevel))
                        {
                            // queue it
                            currentMonkey.Items.Enqueue(itemWorryLevel);
                        }
                    }
                }
                else if (line.StartsWith("operation"))
                {
                    currentMonkey.Operation1 = line.Replace("operation:", "");
                }
                else if (line.StartsWith("test"))
                {
                    var splitLine = line.Split(' ');
                    currentMonkey.ConditionTestDivisibilityBy = int.Parse(splitLine[splitLine.Length - 1]);
                }
                else if (line.StartsWith("if"))
                {
                    var splitLine = line.Split(' ');
                    if (line.Contains("true"))
                        currentMonkey.TestResult_TrueMonkeyID = int.Parse(splitLine[splitLine.Length - 1]);
                    else if (line.Contains("false"))
                        currentMonkey.TestResult_FalseMonkeyID = int.Parse(splitLine[splitLine.Length - 1]);
                }
            }

            MonkeyCount = monkeyMap.Keys.Count;
        }

        private long CalculateMonkeyBusiness()
        {
            long max1 = 0, max2 = 0; 
            foreach(var monkeyData in monkeyMap)
            {
                long activityScore = monkeyData.Value.ActivityScore;
                if (activityScore > max1)
                {
                    max2 = max1;
                    max1 = activityScore;                    
                }
                else if(activityScore > max2)
                {
                    max2 = activityScore;
                }
            }

            return max1 * max2;
        }
    }
}
