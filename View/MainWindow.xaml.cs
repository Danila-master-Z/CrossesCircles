using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CrossesCircles.Components;
using CrossesCircles.Model;
using CrossesCircles.Utils;

namespace CrossesCircles.View;

public partial class MainWindow
{
    private byte currentState;
    private readonly Grid grid;
    private readonly PlayField playField;
    private Image?[,] images = new Image?[3, 3];
    private FieldElementFactory factory = new();
    private GoBot bot = new();
    private Percent percent = new();

    public MainWindow()
    {
        InitializeComponent();
        grid = FindName("MainGrid") as Grid ?? throw new InvalidOperationException("Grid is null");
        playField = new PlayField();
        currentState = 0;
        InitGrid();
        Result();
    }

    private void ResetBtnClicked(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                playField.Field[i, j] = '.';
            }
        }

        foreach (var image in images)
        {
            grid.Children.Remove(image);     
        }

        ClearImageArray();
        InitGrid();
        currentState = 0;
        StatusTextBlock.Text = "Игра началась!";
    }

    public void ClearImageArray()
    {
        int width = images.GetLength(0);
        int height = images.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                images[i, j] = null;
            }
        }
    }

    private async void BtnClicked(object sender, RoutedEventArgs e)
    {
        if (sender is not OneClickableButton button) 
            throw new ArgumentNullException(nameof(button));
        if (button.IsClicked)
            return;

        if (currentState % 2 == 0)
        {
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);

            playField.Field[row, column] = 'x';
            UpdateGrid(column, row);
            Result();
            currentState++;
        }

        if (currentState % 2 == 1 && currentState < 9)
        {
            Tuple<int, int> bestMove = bot.Bot(playField.Field);
            (int row_b, int column_b) = bestMove;
            playField.Field[row_b, column_b] = 'o';
            UpdateGrid(column_b, row_b);
            Result();
            currentState++;
        }
    }

    private void UpdateGrid(int column, int row)
    {
        var symbol = playField.Field[row, column];
        var element = factory.GetElement(symbol);

        switch (element)
        {
            case Image image:
                images[row, column] = image;
                break;
            case Button btn:
                btn.Click += BtnClicked;
                break;
        }

        grid.Children.Add(element);
        Grid.SetColumn(element, column);
        Grid.SetRow(element, row);
    }

    private void InitGrid()
    {
        for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
        {
            UpdateGrid(j, i);
        }
    }

    private void Result()
    {
        var resultOfStep = Checker.CheckForWinner(playField.Field);
        var draw = Checker.IsBoardFull(playField.Field);
        if (resultOfStep != null)
        {
            var winner = resultOfStep.WinnerShape == 'x' ? "Крестик" : "Нолик";
            StatusTextBlock.Text = $"Игра окончена!\nПобедил: {winner}";
            Animation.AnimateWin((int)resultOfStep.TopLeftSideCoordinate.X, (int)resultOfStep.TopLeftSideCoordinate.Y, resultOfStep.WinnerLineType, images);
        }
        if (draw == false)
        {
            List<double> percent_l = percent.Percent_Win(playField.Field);
            PercentText.Text = $"X: {(int)Math.Round(percent_l[0])}% " +
                $"O: {(int)Math.Round(percent_l[1])}% \n" +
                $"Ничья: {(int)Math.Round(percent_l[2])}%";
        }
        if (draw)
        {
            StatusTextBlock.Text = $"Игра окончена!\nНичья!";
        }
    }
}