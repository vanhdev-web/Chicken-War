using System;
using System.IO;
using System.Threading;
using System.Windows.Media;

namespace TeamWork.Field
{
    class Menu
    {
        public static bool menuActive = true;
        public static bool validInput = true;
        public static MediaPlayer mediaPlayer = new MediaPlayer();

        /// <summary>
        /// Màn hình load menu chính
        /// </summary>
        public static void StartMenu()
        {       
            //Menu Music Thread
            //mediaPlayer.Open(new Uri("Resources/GameMenu.mp3", UriKind.Relative));
            //mediaPlayer.Play();
            Printing.WelcomeScreen();
            Thread.Sleep(2000);
            while (menuActive)
            {
                if (validInput)
                {
                    Console.Clear();
                    Printing.StartMenu();
                    validInput = false;
                }

                if (UserChoice(Console.ReadKey(true)))
                {
                    validInput = true;
                }
                else
                {
                    validInput = false;
                }
            }
        }

        // Các nút giao diện menu (Tạm dừng, Điểm số, Thông tin, Thoát)
        public static bool UserChoice(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.P:
                    Console.Clear();
                    mediaPlayer.Stop();
                    menuActive = false;
                    return true;
                case ConsoleKey.S:
                    Console.Clear();
                    Printing.HighScore();
                    return true;
                case ConsoleKey.C:
                    Console.Clear();
                    Printing.Credits();
                    return true;
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Câu chuyện giới thiệu
        /// </summary>
        public static void EntryStoryLine()
        {
            Printing.LoadStory();
            Thread.Sleep(2500);
            Console.Clear();
            Printing.LoadContent();
            Console.Clear();
        }

        /// <summary>
        /// In viền trên và dưới của UI
        /// </summary>
        public static void Table()
        {
            int uiWidth = Math.Min(Console.WindowWidth, 115); // Cap the UI width to 115, or use the window width
            int nameBoard = 14 + Engine.Player.Name.Length;

            // Draw Top Border
            for (int i = 0; i < uiWidth; i++)
            {
                // Define the positions where the top border should NOT be drawn
                bool drawTopBorder = (i <= 3) || (i >= nameBoard && i < 38) || (i > 41);
                if (drawTopBorder)
                {
                    Printing.DrawAt(new Point2D(i, 0), '\u2588', ConsoleColor.DarkRed);
                }
            }

            // Draw Bottom Border
            for (int i = 0; i < uiWidth; i++)
            {
                // Define gaps for liveBoard and scoreBoard
                bool drawBottomBorder = (i <= 3) || (i > 13 && i < 29) || (i > 40);
                if (drawBottomBorder)
                {
                    Printing.DrawAt(new Point2D(i, 30), '\u2588', ConsoleColor.DarkRed);
                }
            }
        }

        /// <summary>
        /// Vẽ UI 
        /// </summary>
        public static void UIDescription()
        {
            string level = string.Format("{0}", Engine.Player.Level).PadLeft(2, '0');
            string score = string.Format("Score: {0} ", Engine.Player.Score).PadLeft(3, '0');
            string playerName = string.Format("Player: {0}", Engine.Player.Name);

            Printing.DrawAt(new Point2D(5, 0), playerName, ConsoleColor.DarkYellow);
            Printing.DrawAt(new Point2D(39, 0), level, ConsoleColor.DarkYellow);
            Printing.DrawAt(new Point2D(5, 30), "Lifes: ", ConsoleColor.DarkYellow);
            Printing.DrawHLineAt(11, 30, Engine.Player.Lifes, '\u2665', ConsoleColor.Red);
            Printing.ClearAtPosition(11 + Engine.Player.Lifes, 30);
            Printing.DrawAt(new Point2D(30, 30), score, ConsoleColor.DarkYellow);
        }

        #region Phương thức điểm cao và điểm số
        // Kiểm tra xem oldHighScore và CurrentHighScore có khác nhau không và thiết lập giá trị cao hơn là HighScore mới
        // Cũng thêm tất cả các điểm vào file Scores.txt
        public static void SetHighscore()
        {
            string highscore = String.Format("Player {0}, Highscore {1}, Time Achieved: {2} / {3} / {4}",
                Engine.Player.Name, Engine.Player.Score, DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);

            string[] oldText = File.ReadAllText("Resources/Highscore.txt").Split();

            string oldHighScore = oldText[3].Remove(oldText[3].Length - 1);
            int oldHighScoreToInt = Int32.Parse(oldHighScore);

            if (oldHighScoreToInt < Engine.Player.Score)
                File.WriteAllText("Resources/Highscore.txt", highscore);

            string currentScores = File.ReadAllText("Resources/Scores.txt");
            highscore = String.Format("Player {0}, Score {1}, Time Achieved: {2} / {3} / {4}",
                Engine.Player.Name, Engine.Player.Score, DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            currentScores += "#" + highscore + @"
";
            File.WriteAllText("Scores.txt", currentScores);
        }      
        /// <summary>
        /// In điểm cao trong màn hình điểm số menu chính
        /// </summary>
        public static void PrintHighscore()
        {
            string currentHighscore = File.ReadAllText("Resources/Highscore.txt");
            Printing.DrawAt(new Point2D(15, 14), "Current Highscore: ", ConsoleColor.Green);
            Printing.DrawAt(new Point2D(15, 15), currentHighscore, ConsoleColor.Green);
            Printing.DrawAt(new Point2D(15, 17), "Last Achieved Scores: ", ConsoleColor.Green);

            string[] currentScores = File.ReadAllLines("Resources/Scores.txt");
            int y = 15;
            int counter = 0;
            for (int i = currentScores.Length - 1; i >= currentScores.Length - 10; i--)
            {
                y++;
                counter++;
                Printing.DrawAt(new Point2D(15, y), counter + " " + currentScores[i], ConsoleColor.Green);
            }
        }
        #endregion
    }
}
