using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesCircles.Utils
{
    public class Percent
    {
        private MiniMax_method miniMax = new();
        public List<double> Percent_Win(char[,] board)
        {
            List<double> percent = new List<double>();
            double steps = 0;
            double o_wins = 0;
            double x_wins = 0;
            double draws = 0;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '.')
                    {
                        steps++;

                        char[,] tempboard = (char[,])board.Clone();
                        tempboard[i, j] = 'x';
                        int x_score = miniMax.MiniMax(tempboard, 0, true);

                        tempboard = (char[,])board.Clone();
                        tempboard[i, j] = 'o';
                        int o_score = miniMax.MiniMax(tempboard, 0, false);

                        if (x_score > 0)
                        {
                            x_wins++;
                        }
                        else if (o_score > 0)
                        {
                            o_wins++;
                        }
                        else
                        {
                            draws++;
                        }
                    }
                }
            }
            if (steps > 0)
            {
                percent.Add((o_wins / steps) * 100);
                percent.Add((x_wins / steps) * 100);
                percent.Add((draws / steps) * 100);
            }

            return percent;
        }

    }
}
