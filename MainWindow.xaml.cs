using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FMatchGame
{
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int matchesFound; // сравнений найдено
        int TenthsOfSecondsElapsed; // отслеживание прошедшего времени
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e) // должно вызываться при нажатии таб после += строки(29)
        {
            TenthsOfSecondsElapsed++;
            timeTextBlock.Text = (TenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Сыграть снова? ";
            }
        }
        private void SetUpGame()
        {
            List<string> animalMemes = new List<string>()
            {
                "🐵", "🐵",
                "🐶", "🐶",
                "🐺", "🐺",
                "🦁", "🦁",
                "🐴", "🐴",
                "🦄", "🦄",
                "🐷", "🐷",
                "🐭", "🐭"
            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalMemes.Count);
                    string NextMeme = animalMemes[index];
                    textBlock.Text = NextMeme;
                    animalMemes.RemoveAt(index);
                } 
            }
            timer.Start();
            TenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (lastTextBlockClicked.Text == textBlock.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }    
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame(); // Чтобы сбросить игру, если были найдены 8 пар.
            }
        }
    }
}
