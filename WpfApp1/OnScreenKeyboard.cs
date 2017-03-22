using LoveItBakeryG11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OnScreenKeyboard
{
    class OnScreenKeyboard
    {
        private static Grid gridKeyboard = new Grid()
        {
            Visibility = Visibility.Collapsed,
            Background = Brushes.LightGray,
        };
        
        public static void createOsk(Grid RootGrid)
        {
            Border gridBorder = new Border()
            {
                BorderBrush = Brushes.DarkGray,
                BorderThickness = new Thickness(0,6,0,0)
            };
            gridBorder.Child = gridKeyboard;

            string qwertyString = "qwertyuiopasdfghjklzxcvbnm";
            BrushConverter brushConverter = new BrushConverter();
            {
                Brush Background = (Brush)brushConverter.ConvertFrom("#FFBBBBBB");
            };
            for (int j = 0; j < 11; j++)
            {
                ColumnDefinition column = new ColumnDefinition();
                gridKeyboard.ColumnDefinitions.Add(column);
            }
            for (int k = 0; k < 4; k++)
            {
                RowDefinition row = new RowDefinition();
                gridKeyboard.RowDefinitions.Add(row);
            }
            Grid.SetRow(gridBorder, 1);
            RootGrid.Children.Add(gridBorder);
            for (int i = 0; i < 26; i++)
            {

                if (i == 10)
                {
                    Button keyboardBackspaceBtn = new Button()
                    {
                        Content = "<--",
                        Tag = "backspace",
                        Focusable = false
                    };
                    keyboardBackspaceBtn.Click += new RoutedEventHandler(MainWindow.keyboardBtn_Click);
                    Grid.SetColumnSpan(keyboardBackspaceBtn, 2);
                    Grid.SetColumn(keyboardBackspaceBtn, 10);
                    gridKeyboard.Children.Add(keyboardBackspaceBtn);
                }
                //create inner viewbox.
                Viewbox innerViewBox = new Viewbox();
                TextBlock innerText = new TextBlock();
                innerText.Text = qwertyString[i].ToString().ToUpper();
                innerViewBox.Child = innerText;
                Button keyboardLetterBtn = new Button()
                {
                    Content = innerViewBox,
                    //Content = qwertyString[i].ToString().ToUpper(),
                    Tag = qwertyString[i],
                    Focusable = false,
                    Margin = new Thickness(2, 2, 2, 2),
                    Height = 1000,
                    Width = 1000
                };
                keyboardLetterBtn.Click += new RoutedEventHandler(MainWindow.keyboardBtn_Click);
                Viewbox keyboardLetterViewBox = new Viewbox();
                keyboardLetterViewBox.Child = keyboardLetterBtn;
                if (i < 10)
                {
                    Grid.SetRow(keyboardLetterViewBox, 0);
                    Grid.SetColumn(keyboardLetterViewBox, i);
                }
                else if (i < 19)
                {
                    Grid.SetRow(keyboardLetterViewBox, 1);
                    Grid.SetColumn(keyboardLetterViewBox, i - 9);
                }
                else if (i < 26)
                {
                    Grid.SetRow(keyboardLetterViewBox, 2);
                    Grid.SetColumn(keyboardLetterViewBox, i - 17);
                }

                gridKeyboard.Children.Add(keyboardLetterViewBox);

            }
            Button keyboardSpaceBtn = new Button()
            {
                Content = "______________________",
                Tag = " ",
                Focusable = false
            };
            keyboardSpaceBtn.Click += new RoutedEventHandler(MainWindow.keyboardBtn_Click);
            Grid.SetRow(keyboardSpaceBtn, 3);
            Grid.SetColumn(keyboardSpaceBtn, 2);
            Grid.SetColumnSpan(keyboardSpaceBtn, 7);
            gridKeyboard.Children.Add(keyboardSpaceBtn);

        }

        public static void showOsk(Grid RootGrid)
        {
            gridKeyboard.Visibility = Visibility.Visible;
            RootGrid.RowDefinitions[0].Height = new GridLength(7, GridUnitType.Star);
            RootGrid.RowDefinitions[1].Height = new GridLength(3, GridUnitType.Star);
        }
        public static void hideOsk(Grid RootGrid)
        {
            gridKeyboard.Visibility = Visibility.Collapsed;
            RootGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            RootGrid.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
        }
        public static void buttonPress(Grid RootGrid, Object sender)
        {
            IInputElement focusedControl = Keyboard.FocusedElement;
            if (focusedControl is TextBox)
            {
                TextBox focusedTextBox = (TextBox)focusedControl;
                int caratPos = focusedTextBox.SelectionStart;
                if ((sender as Button).Tag == "backspace")
                {
                    if (caratPos > 0)
                    {
                        focusedTextBox.Text = focusedTextBox.Text.Remove(caratPos - 1, 1);
                        focusedTextBox.SelectionStart = caratPos - 1;
                    }
                }
                else
                {
                    if (caratPos > 0)
                    {
                        if (focusedTextBox.Text[caratPos - 1].ToString() == " ")
                        {
                            focusedTextBox.Text = focusedTextBox.Text.Insert(caratPos, (sender as Button).Tag.ToString().ToUpper());
                            focusedTextBox.SelectionStart = caratPos + 1;
                        }
                        else if (focusedTextBox.Text[caratPos - 1].ToString() != " ")
                        {
                            focusedTextBox.Text = focusedTextBox.Text.Insert(caratPos, (sender as Button).Tag.ToString().ToLower());
                            focusedTextBox.SelectionStart = caratPos + 1;
                        }
                    }
                    else
                    {
                        focusedTextBox.Text = focusedTextBox.Text.Insert(caratPos, (sender as Button).Tag.ToString().ToUpper());
                        focusedTextBox.SelectionStart = caratPos + 1;
                    }
                }
            }
        }
    }
}
