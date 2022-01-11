using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;

namespace HexMines
{
    public partial class MainPage : ContentPage
    {
        private static short BUTTON_X_SIZE = 35;
        private static short BUTTON_Y_SIZE = 35;

        public ObservableCollection<MineInfo> mines { get; set; }

        private Random random = new Random();
        public MainPage()
        {
            InitializeComponent();
            InitMinefield();
        }

        private void InitMinefield()
        {
            Trace.WriteLine("InitMinefield()");
            mines = new ObservableCollection<MineInfo>();
            Color myColor;
            for (int i = 0; i < 32; i++)
            {
                myColor = new Color(random.NextDouble(), random.NextDouble(), random.NextDouble());
                Trace.WriteLine($"Adding button {i}");
                MatrixButton button = new MatrixButton(i);
                button.Text = i.ToString();
                button.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
                button.TextColor = myColor;
                button.WidthRequest = BUTTON_X_SIZE;
                button.HeightRequest = BUTTON_Y_SIZE;
                button.BackgroundColor = myColor;
                button.Clicked += Button_Clicked;
                mines.Add(new MineInfo(random.Next() % 2 == 0, button));
                mineLayout.Children.Add(button,new Point(BUTTON_X_SIZE*(i % 4), BUTTON_Y_SIZE * (i / 4)));
            }

            int[] rowSum = new int[8];
            for(int j = 0; j < 32; j++)
            {
                var mine = mines.Single(m => m.MineButton.Index == j);
                if (mine.IsExplosive)
                {
                    Trace.WriteLine($"Caution: Mine # {mine.MineButton.Index} is explosive! Adding {Math.Pow(2, 3-(j % 4))} to sum. Row index: {3-(j % 4)}");
                    rowSum[j / 4] += (int)Math.Pow(2, 3 - (j % 4));
                }
            }
            for(int k = 0; k < 8; k++)
            {
                Trace.WriteLine($"Setting Hex {GenerateHexString(rowSum[k])} for sum {rowSum[k]} in row {k}");
                Label label = new Label
                {
                    Text = GenerateHexString(rowSum[k])
                };
                hexLayout.Children.Add(label, new Point(0, BUTTON_Y_SIZE * k));
            }
        }

        public string GenerateHexString(int i)
        {
            if(i > -1 && i < 16)
            {
                if(i < 10)
                {
                    return i.ToString();
                }
                else
                {
                    switch(i)
                    {
                        case 10:
                            return "A";
                            break;
                        case 11:
                            return "B";
                            break;
                        case 12:
                            return "C";
                            break;
                        case 13:
                            return "D";
                            break;
                        case 14:
                            return "E";
                            break;
                        case 15:
                            return "F";
                            break;
                        default:
                            return "?";
                    }
                }
            }
            else
            {
                return "?";
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var clickedButton = sender as Button;
            var index = int.Parse(clickedButton.Text);
            var mine = mines.Single(m => m.MineButton.Index == index);
            Trace.WriteLine($"You clicked me: {mine.MineButton.Index} IsExplosive: {mine.IsExplosive}");
            if(mine.IsExplosive)
            {
                clickedButton.BackgroundColor = Color.Red;
                clickedButton.TextColor = Color.Red;
            }
            else
            {
                clickedButton.BackgroundColor = Color.Green;
                clickedButton.TextColor = Color.Green;
            }
        }
    }
}
