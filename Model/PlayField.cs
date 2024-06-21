namespace CrossesCircles.Model;

public class PlayField
{
    //хранение информации о текущем состоянии игрового поля

    private char[,] field = {
        { '.', '.', '.' },
        { '.', '.', '.' },
        { '.', '.', '.' } 
    };

    public char[,] Field => field;
}