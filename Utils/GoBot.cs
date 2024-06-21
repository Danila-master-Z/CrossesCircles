using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesCircles.Utils
{
    public class GoBot
    {
        private MiniMax_method miniMax = new();
        public Tuple<int, int> Bot(char[,] board)
        {
            int bestScore = int.MinValue;
            Tuple<int, int> bestMove = Tuple.Create(-1, -1);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '.')
                    {
                        board[i, j] = 'o';
                        int score = miniMax.MiniMax(board, 0, false);
                        board[i, j] = '.';

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = Tuple.Create(i, j);
                        }
                    }
                }
            }

            return bestMove;
        }
    }
}
