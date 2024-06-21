using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesCircles.Utils
{
    public class MiniMax_method
    {
        public int MiniMax(char[,] board, int depth, bool isMaximizing)
        {
            var result = Checker.CheckForWinner(board);
            if (result != null)
            {
                return result.WinnerShape == 'o' ? 1 : -1;
            }

            if (!Checker.IsBoardFull(board))
            {
                if (isMaximizing)
                {
                    int bestScore = int.MinValue;
                    for (int i = 0; i < board.GetLength(0); i++)
                    {
                        for (int j = 0; j < board.GetLength(1); j++)
                        {
                            if (board[i, j] == '.')
                            {
                                board[i, j] = 'o';
                                int score = MiniMax(board, depth + 1, false);
                                board[i, j] = '.';
                                bestScore = Math.Max(score, bestScore);
                            }
                        }
                    }
                    return bestScore;
                }
                else
                {
                    int bestScore = int.MaxValue;
                    for (int i = 0; i < board.GetLength(0); i++)
                    {
                        for (int j = 0; j < board.GetLength(1); j++)
                        {
                            if (board[i, j] == '.')
                            {
                                board[i, j] = 'x';
                                int score = MiniMax(board, depth + 1, true);
                                board[i, j] = '.';
                                bestScore = Math.Min(score, bestScore);
                            }
                        }
                    }
                    return bestScore;
                }
            }
            return 0;
        }
    }
}
