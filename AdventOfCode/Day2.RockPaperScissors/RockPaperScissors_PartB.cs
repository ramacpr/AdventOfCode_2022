using AdventOfCode.DailyChallenge.Helper;

namespace AdventOfCode.DailyChallenge
{
    enum RockPaperScissor_Col1
    {
        Rock = 1,
        A = 1,
        Paper = 2,
        B = 2,
        Scissors = 3,
        C = 3,
        Invalid = -1
    }

    enum ExpectedOutCome
    {
        Loose = 1,
        X = 1,
        Draw = 2,
        Y = 2,
        Win = 3,
        Z = 3,
        Invalid = -1
    }

    public class RockPaperScissors_PartB
    {
        InputFileReader inputFileReader = null;

        public RockPaperScissors_PartB()
        {
            inputFileReader = new InputFileReader(Days.day2);
        }

        public int GetResult()
        {
            int myTotalScore = 0;
            int opponentTotalScore = 0;

            foreach (var itemLine in inputFileReader.GetNextLine())
            {
                if (string.IsNullOrEmpty(itemLine))
                    continue;
                else
                {
                    var strPlay = itemLine.Trim().Split(' ');
                    int myScore = 0, opponentScore = 0;

                    Play(GetMove(strPlay[0]), GetExpectedOutcome(strPlay[1]), out myScore, out opponentScore);
                    myTotalScore += myScore;
                    opponentTotalScore += opponentScore;
                }
            }

            return opponentTotalScore;
        }

        RockPaperScissor_Col1 GetMove(string move)
        {
            switch (move)
            {
                case "A":
                    return RockPaperScissor_Col1.Rock;
                case "B":
                    return RockPaperScissor_Col1.Paper;
                case "C":
                    return RockPaperScissor_Col1.Scissors;
            }
            return RockPaperScissor_Col1.Invalid;
        }

        ExpectedOutCome GetExpectedOutcome(string outcome)
        {
            switch(outcome)
            {
                case "X":
                    return ExpectedOutCome.Loose;
                case "Y":
                    return ExpectedOutCome.Draw;
                case "Z":
                    return ExpectedOutCome.Win; 
            }
            return ExpectedOutCome.Invalid;
        }

        RockPaperScissor_Col1 DecideMyMove(RockPaperScissor_Col1 player1_move, ExpectedOutCome expectedOutCome)
        {
            if (expectedOutCome == ExpectedOutCome.Draw)
                return player1_move; 

            else if (expectedOutCome == ExpectedOutCome.Win)
            {
                if (player1_move == RockPaperScissor_Col1.Rock)
                    return RockPaperScissor_Col1.Paper;
                else if (player1_move == RockPaperScissor_Col1.Paper)
                    return RockPaperScissor_Col1.Scissors;
                else if (player1_move == RockPaperScissor_Col1.Scissors)
                    return RockPaperScissor_Col1.Rock;
            }
            else if(expectedOutCome == ExpectedOutCome.Loose)
            {
                if (player1_move == RockPaperScissor_Col1.Rock)
                    return RockPaperScissor_Col1.Scissors;
                else if (player1_move == RockPaperScissor_Col1.Paper)
                    return RockPaperScissor_Col1.Rock;
                else if (player1_move == RockPaperScissor_Col1.Scissors)
                    return RockPaperScissor_Col1.Paper;
            }
            return RockPaperScissor_Col1.Invalid;
        }

        void Play(RockPaperScissor_Col1 player1_move, ExpectedOutCome expectedOutcome, out int player1_score, out int player2_score)
        {
            player1_score = (int)player1_move;
            player2_score = (int)DecideMyMove(player1_move, expectedOutcome);


            if(expectedOutcome == ExpectedOutCome.Win)
            {
                player2_score += 6;
            }
            else if (expectedOutcome == ExpectedOutCome.Loose)
            {
                player1_move += 6; 
            }
            else if(expectedOutcome == ExpectedOutCome.Draw)
            {
                player1_score += 3;
                player2_score += 3;
            }            
        }
    }
}
