using AdventOfCode.DailyChallenge.Helper;

namespace AdventOfCode.DailyChallenge
{
    enum RockPaperScissor_Moves
    {
        Rock = 1, 
        A = 1, 
        X = 1, 
        Paper = 2, 
        B = 2, 
        Y = 2, 
        Scissors = 3, 
        C = 3, 
        Z = 3, 
        Invalid = -1
    }

    public class RockPaperScissors
    {
        InputFileReader inputFileReader = null;

        public RockPaperScissors()
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

                    Play(GetMove(strPlay[0]), GetMove(strPlay[1]), out myScore, out opponentScore);
                    myTotalScore += myScore;
                    opponentTotalScore += opponentScore;
                }
            }

            return opponentTotalScore;
        }

        RockPaperScissor_Moves GetMove(string move)
        {
            switch(move)
            {
                case "A":
                case "X":
                    return RockPaperScissor_Moves.Rock;
                case "B":
                case "Y":
                    return RockPaperScissor_Moves.Paper;
                case "C":
                case "Z":
                    return RockPaperScissor_Moves.Scissors;
            }
            return RockPaperScissor_Moves.Invalid;
        }

        void Play(RockPaperScissor_Moves player1_move, RockPaperScissor_Moves player2_move, out int player1_score, out int player2_score)
        {
            player1_score = (int)player1_move;
            player2_score = (int)player2_move;

            // draw
            if (player1_move == player2_move)
            {
                player1_score += 3;
                player2_score += 3; 
            }
            // Paper defeats Rock
            else if (player1_move == RockPaperScissor_Moves.Rock && player2_move == RockPaperScissor_Moves.Paper)
                player2_score += 6;

            // Rock defeats Scissors
            else if (player1_move == RockPaperScissor_Moves.Rock && player2_move == RockPaperScissor_Moves.Scissors)
                player1_score += 6;

            // Paper defeats Rock
            else if (player1_move == RockPaperScissor_Moves.Paper && player2_move == RockPaperScissor_Moves.Rock)
                player1_score += 6;

            // Scissors defeats Paper
            else if (player1_move == RockPaperScissor_Moves.Paper && player2_move == RockPaperScissor_Moves.Scissors)
                player2_score += 6;

            // Rock defeats Scissors
            else if (player1_move == RockPaperScissor_Moves.Scissors && player2_move == RockPaperScissor_Moves.Rock)
                player2_score += 6;

            // Scissors defeats Paper
            else if (player1_move == RockPaperScissor_Moves.Scissors && player2_move == RockPaperScissor_Moves.Paper)
                player1_score += 6;


        }
    }
}
