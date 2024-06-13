using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using CrossesCircles.Model;

namespace CrossesCircles.Utils;

static class Animation
{
    private static async Task Anim(Image image)
    {
        for (double i = 1; i < 1.2; i += 0.005)
        {
            image.RenderTransform = new ScaleTransform(i, i);
            await Task.Delay(10);
        }
        image.RenderTransform = new ScaleTransform(1, 1);
    }
    public static void AnimateWin(int x, int y, LineType line, Image[,] images)
    {
        switch (line)
        {
            case LineType.Horizontal:
            {
                for (int i = 0; i < 3; i++)
                    Anim(images[y, i]);
            }
            break;
            case LineType.Vertical:
            {
                for (int i = 0; i < 3; i++)
                    Anim(images[i, x]);
            }
            break;
            case LineType.Diagonal:
            {
                switch (x)
                {
                    case 2 when y == 0:
                        Anim(images[0, 2]);
                        Anim(images[1, 1]);
                        Anim(images[2, 0]);
                        break;
                    case 0 when y == 0:
                        Anim(images[0, 0]);
                        Anim(images[1, 1]);
                        Anim(images[2, 2]);
                        break;
                }
            }
            break;
        }
    }
}