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
        public static void StartLogo()
        {
            Printing.WelcomeScreen();
            Thread.Sleep(2000);
        }
        public static void StartMenu()
        {      
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
                Printing.DrawAt(new Point2D(i, 0), '\u2588', ConsoleColor.DarkCyan); 

            }

            // Draw Bottom Border
            for (int i = 0; i < uiWidth; i++)
            {
                Printing.DrawAt(new Point2D(i, 30), '\u2588', ConsoleColor.DarkCyan);
            }
        }

        /// <summary>
        /// Vẽ UI 
        /// </summary>
        public static void UIDescription()
        {
            string level = string.Format(" Level: {0} ", Engine.Player.Level).PadLeft(2, '0');
            string score = string.Format("               Score: {0}               ", Engine.Player.Score).PadLeft(3, '0');
            string playerName = string.Format(" Player: {0} ", Engine.Player.Name);

            Printing.DrawAt(new Point2D(5, 0), playerName, ConsoleColor.Cyan) ;
            Printing.DrawAt(new Point2D(100, 0), level, ConsoleColor.Cyan);
            Printing.DrawAt(new Point2D(5, 30), " Lifes: ", ConsoleColor.Cyan); ;
            Printing.DrawHLineAt(12, 30, Engine.Player.Lifes, '\u2665', ConsoleColor.DarkRed);
            Printing.ClearAtPosition(12 + Engine.Player.Lifes, 30);
            Printing.DrawAt(new Point2D(41, 0), score, ConsoleColor.Cyan); ;
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
