using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CrossesCircles.Components;

namespace CrossesCircles.Utils;

public class FieldElementFactory
{
    private readonly BitmapImage circle = new BitmapImage(new Uri("../Resources/Circle.png", UriKind.Relative));
    private readonly BitmapImage cross = new BitmapImage(new Uri("../Resources/Cross.png", UriKind.Relative));

    public UIElement GetElement(char symbol) //возвращает различные элементы в зависимости от символа, который ему передается
    {
        switch (symbol)
        {
            case '.': //создает и возвращает объект кнопки
                var btn = new OneClickableButton();
                return btn;
            case 'o': //создается и возвращается изображение круга
                var circle = new Image
                {
                    Margin = new Thickness(5)
                };
                circle.Source = this.circle;
                return circle;
            case 'x': //создается и возвращается изображение крестика
                var cross = new Image
                {
                    Margin = new Thickness(5)
                };
                cross.Source = this.cross;
                return cross;
            default:
                throw new Exception("Invalid Symbol");
        }
    }
}