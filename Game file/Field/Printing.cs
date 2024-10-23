using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace TeamWork.Field
{
    public static class Printing
    {
        #region Printing Methods

        /// <summary>
        /// Draw an object at given X and Y Coordinates
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <param name="obj">Object to print</param>
        public static void DrawAt(int x, int y, object obj)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(obj.ToString());
            }
            catch { return; }
        }


        /// <summary>
        /// Draw an object at a Point2D
        /// </summary>
        /// <param name="point">Point2D to print at</param>
        /// <param name="obj">Object to print</param>
        public static void DrawAt(Point2D point, object obj)
        {
            try
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write(obj.ToString());
            }
            catch { return; }

        }
        /// <summary>
        /// Draw an object at given X and Y Coordinates with a color
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <param name="obj">Object to print</param>
        /// <param name="clr">Color to print with</param>
        public static void DrawAt(int x, int y, object obj, ConsoleColor clr)
        {
            try
            {
                Console.ForegroundColor = clr;
                DrawAt(x, y, obj);
                Console.ResetColor();
            }
            catch { return; }

        }

        /// <summary>
        /// Draw an object at given Point2D with color
        /// </summary>
        /// <param name="point">Point2D to print at</param>
        /// <param name="obj">Object to print</param>
        /// <param name="clr">Color to print with</param>
        public static void DrawAt(Point2D point, object obj, ConsoleColor clr)
        {
            try
            {
                Console.ForegroundColor = clr;
                DrawAt(point, obj);
                Console.ResetColor();
            }
            catch { return; }

        }

        public static void DrawAtBG(int x, int y, object obj, ConsoleColor bclr)
        {
            try
            {
                Console.BackgroundColor = bclr;
                DrawAt(x, y, obj);
                Console.ResetColor();
            }
            catch { return; }
        }
        public static void DrawAtBGPlus(Point2D point, object obj, ConsoleColor clr, ConsoleColor bclr) //Chữ  và hình có màu khác nhau 
        {
            try
            {
                Console.ForegroundColor = clr;
                DrawAt(point, obj);
                Console.BackgroundColor = bclr;
                DrawAt(point, obj);
                Console.ResetColor();
            }
            catch { return; }
        }

        /// <summary>
        /// Draw a vertical line with given lenght starting at X and Y
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <param name="lenght">Lenght of the line</param>
        /// <param name="obj">Object to print</param>
        /// <param name="clr">Color to print with</param>
        public static void DrawVLineAt(int x, int y, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    y++;
                }
            }
            catch { return; };

        }

        /// <summary>
        /// Draw vertical line with given lenght with a pause between characters
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <param name="lenght">Lenght of the line</param>
        /// <param name="obj">Object to print</param>
        /// <param name="sleep">Pause time between characters(ms)</param>
        /// <param name="reverse">True if you want to draw from right to left</param>
        /// <param name="clr">Color to print with</param>
        public static void DrawVLineAt(int x, int y, int lenght, object obj, int sleep, bool reverse, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    if (reverse)
                    {
                        y--;
                    }
                    else
                    {
                        y++;
                    }
                    Thread.Sleep(sleep);
                }
            }
           catch { return;  };

        }

        /// <summary>
        /// Draw a vertical line with given lenght starting at Point2D
        /// </summary>
        /// <param name="point">Point2D to print at</param>
        /// <param name="lenght">Length of the line</param>
        /// <param name="obj">Object to print</param>
        /// <param name="clr">Color to print with</param>
        public static void DrawVLineAt(Point2D point, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                DrawVLineAt(point.X, point.Y, lenght, obj, clr);
            }
            catch { return; }
        }


        /// <summary>
        /// Draw a horizontal line with given lenght starting at X and Y
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <param name="lenght">Lenght of the line</param>
        /// <param name="obj">Object to print</param>
        /// <param name="clr">Color to print with</param>
        public static void DrawHLineAt(int x, int y, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    x++;
                }
            }
            catch { return;  };
           
        }

        /// <summary>
        /// Draw a horizontal line with given lenght and pause bethween characters
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <param name="lenght">Lenght of the line</param>
        /// <param name="obj">Object to print</param>
        /// <param name="sleep">Pause time between characters(ms)</param>
        /// <param name="reverse">True if you want to draw from right to left</param>
        /// <param name="clr">Color to print with</param>
        public static void DrawHLineAt(int x, int y, int lenght, object obj, int sleep, bool reverse, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    if (reverse)
                    {
                        x--;
                    }
                    else
                    {
                        x++;
                    }
                    Thread.Sleep(sleep);
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Draw a horizontal line with given lenght starting at Point2D
        /// </summary>
        /// <param name="point">Point2D to print at</param>
        /// <param name="lenght">Lenght of the line</param>
        /// <param name="obj">Object to print</param>
        /// <param name="clr">Color to print with</param>
        public static void DrawHLineAt(Point2D point, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                DrawHLineAt(point.X, point.Y, lenght, obj, clr);
            }
            catch { return; }
        }
          





        /// <summary>
        /// Drawing given string character by character with a sleep between each one
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <param name="str">String to print</param>
        /// <param name="sleep">Sleep time between chars(ms)</param>
        /// <param name="reverse">If you want to print from right to left</param>
        /// <param name="clr"></param>
        public static void DrawStringCharByChar(int x, int y, string str, int sleep, bool reverse,
            ConsoleColor clr = ConsoleColor.White)
        {
            try
            {
                if (reverse)
                {
                    x = x + str.Length - 1;
                    for (int i = str.Length - 1; i >= 0; i--)
                    {
                        DrawAt(x, y, str[i], clr);
                        x--;
                        Thread.Sleep(sleep);
                    }
                }
                else
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        DrawAt(x, y, str[i], clr);
                        x++;
                        Thread.Sleep(sleep);
                    }
                }
            }
            catch { return; }
         
        }

        #endregion

        #region Grаphics
        /// <summary>
        /// Draw High Score screen
        /// linked to main menu
        /// </summary>
        public static void HighScore()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 3, false, ConsoleColor.Yellow);
            DrawVLineAt(114, 0, 30, '\u2588', 3, false, ConsoleColor.Yellow);
            DrawHLineAt(114, 29, 115, '\u2588', 3, true, ConsoleColor.Yellow);
            DrawVLineAt(0, 29, 30, '\u2588', 3, true, ConsoleColor.Yellow);

            DrawAt(1, 2, @".                                                               +         ", ConsoleColor.Cyan);
            DrawAt(1, 3, @"      .           +                 ,             *                       ", ConsoleColor.Cyan);
            DrawAt(1, 4, @"   .                             .     .                         .        ", ConsoleColor.Cyan);
            DrawAt(1, 5, @"     ,              *                     .                '        *     ", ConsoleColor.Cyan);
            DrawAt(1, 6, @"                                .                                       ' ", ConsoleColor.Cyan);
            DrawAt(1, 7, @"                                                +                        ", ConsoleColor.Cyan);
            DrawAt(1, 8, @"                                                              .          ", ConsoleColor.Cyan);
            DrawAt(1, 9, @"             *                                                           ", ConsoleColor.Cyan);
            DrawAt(1, 10, @"                           '                                             ", ConsoleColor.Cyan);
            DrawAt(1, 11, @"   .                               +                          .           ", ConsoleColor.Cyan);
            DrawAt(1, 12, @"                  *         .                       +                     ", ConsoleColor.Cyan);
            DrawAt(1, 13, @"      .                                                                 ", ConsoleColor.Cyan);
            DrawAt(1, 14, @"              ,                                                           ", ConsoleColor.Cyan);
            DrawAt(1, 15, @"                                                        +               ", ConsoleColor.Cyan);
            DrawAt(1, 16, @"                 *                                                      ", ConsoleColor.Cyan);
            DrawAt(1, 17, @"     .                                               *                  ", ConsoleColor.Cyan);
            DrawAt(1, 18, @"                                                *                      ", ConsoleColor.Cyan);
            DrawAt(1, 19, @".                    *                                                    ", ConsoleColor.Cyan);
            DrawAt(1, 20, @".                            *                                     +      ", ConsoleColor.Cyan);
            DrawAt(1, 21, @"                                                *                         ", ConsoleColor.Cyan);
            DrawAt(1, 22, @"                                                        ,                   ", ConsoleColor.Cyan);
            DrawAt(1, 23, @"         +                                 *                               ", ConsoleColor.Cyan);
            DrawAt(1, 24, @"                                                                           ", ConsoleColor.Cyan);
            DrawAt(1, 25, @".                    *                                                    ", ConsoleColor.Cyan);
            DrawAt(1, 26, @".                            *                                     +      ", ConsoleColor.Cyan);
            DrawAt(1, 27, @"                                                *                         ", ConsoleColor.Cyan);
            DrawAt(1, 28, @"                                                        ,                   ", ConsoleColor.Cyan);

            DrawStringCharByChar(18, 5, @" _  _ _      _      ___", 5, false, ConsoleColor.Magenta);
            DrawStringCharByChar(18, 6, @"| || (_)__ _| |_   / __| __ ___ _ _ ___", 3, true, ConsoleColor.Magenta);
            DrawStringCharByChar(18, 7, @"| __ | / _` | ' \  \__ \/ _/ _ \ '_/ -_)", 3, false, ConsoleColor.Magenta);
            DrawStringCharByChar(18, 8, @"|_||_|_\__, |_||_| |___/\__\___/_| \___|", 3, true, ConsoleColor.Magenta);
            DrawStringCharByChar(25, 9, @"|___/", 5, false, ConsoleColor.Magenta);
            Thread.Sleep(550);
            DrawAt(28, 28, @"(B)ack to Mine Menu", ConsoleColor.Yellow);
            Menu.PrintHighscore();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.B)
                {
                    Menu.validInput = true;
                    break;
                }
            }

        }
        /// <summary>
        /// Draw Welcome screen
        /// linked to main menu
        /// </summary>
        public static void WelcomeScreen()
        {
            DrawHLineAt(45, 4, 60, '\u2588', 1, false, ConsoleColor.DarkRed);
            DrawHLineAt(70, 25, 60, '\u2588', 1, true, ConsoleColor.DarkRed);

            DrawAt(35, 12, "██╗  ██╗███████╗██╗  ██╗███████╗███████╗██████╗", ConsoleColor.DarkRed);
            DrawAt(35, 12, "██╗  ██╗███████╗██╗  ██╗███████╗███████╗██████╗", ConsoleColor.DarkRed);
            DrawAt(35, 13, "██║  ██║██╔════╝██║  ██║██╔════╝██╔════╝██╔══██╗", ConsoleColor.DarkRed);
            DrawAt(35, 14, "███████║███████╗███████║█████╗  █████╗  ██████╔╝", ConsoleColor.DarkRed);
            DrawAt(35, 15, "╚════██║╚════██║██╔══██║██╔══╝  ██╔══╝  ██╔═══╝ ", ConsoleColor.DarkRed);
            DrawAt(35, 16, "     ██║███████║██║  ██║███████╗███████╗██║", ConsoleColor.DarkRed);
            DrawAt(35, 17, "     ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝     ", ConsoleColor.DarkRed);
        }
        /// <summary>
        /// Draw Main Menu screen
        /// linked to main menu
        /// </summary>
        public static void StartMenu()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Red);
            // Drawing the new background
            DrawAt(1, 1, @"                                         ♦                              ♦         ♦                              ", ConsoleColor.Cyan);
            DrawAt(1, 2, @"  ♦          ♦        ♦        ♦                        ♦       ♦                                                ", ConsoleColor.Cyan);
            DrawAt(1, 3, @"                            ███████╗████████╗███████╗██╗     ██╗      █████╗  ██████╗                            ", ConsoleColor.Cyan);
            DrawAt(1, 4, @"                            ██╔════╝╚══██╔══╝██╔════╝██║     ██║     ██╔══██╗ ██╔══██╗       ♦                   ", ConsoleColor.Cyan);
            DrawAt(1, 5, @"        ♦     ♦          ♦  ███████╗   ██║   █████╗  ██║     ██║     ███████║ ██████╔╝                           ", ConsoleColor.Cyan);
            DrawAt(1, 6, @"                            ╚════██║   ██║   ██╔══╝  ██║     ██║     ██╔══██║ ██╔══██╗                           ", ConsoleColor.Cyan);
            DrawAt(1, 7, @"    ♦          ♦            ███████║   ██║   ███████╗███████╗███████╗██║  ██║ ██║  ██║       ♦     ♦             ", ConsoleColor.Cyan);
            DrawAt(1, 8, @"                            ╚══════╝   ╚═╝   ╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝                           ", ConsoleColor.Cyan);
            DrawAt(1, 9, @"                     ♦                                                               ♦                          ", ConsoleColor.Cyan);
            DrawAt(1, 10, @"                                        ♦   ██╗    ██╗ █████╗ ██████╗       ♦                         ♦         ", ConsoleColor.Cyan);
            DrawAt(1, 11, @"                                            ██║    ██║██╔══██╗██╔══██╗                                       ♦  ", ConsoleColor.Cyan);
            DrawAt(1, 12, @"                          ♦       ♦         ██║ █╗ ██║███████║██████╔╝                                          ", ConsoleColor.Cyan);
            DrawAt(1, 13, @"    ♦                                       ██║███╗██║██╔══██║██╔══██╗          ♦                               ", ConsoleColor.Cyan);
            DrawAt(1, 14, @"                   ♦        ♦               ╚███╔███╔╝██║  ██║██║  ██║                                         ♦", ConsoleColor.Cyan);
            DrawAt(1, 15, @"                                    ♦        ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝                                          ", ConsoleColor.Cyan);
            DrawAt(1, 16, @"          ♦        ♦                                                        ♦                     ♦    ♦        ", ConsoleColor.Cyan);
            DrawAt(1, 17, @"                      ♦          ♦                                 ♦               ♦                   ♦      ♦ ", ConsoleColor.Cyan);

            DrawAt(29, 3, @"███████╗████████╗███████╗██╗     ██╗      █████╗  ██████╗ ", ConsoleColor.DarkRed);
            DrawAt(29, 4, @"██╔════╝╚══██╔══╝██╔════╝██║     ██║     ██╔══██╗ ██╔══██╗", ConsoleColor.DarkRed);
            DrawAt(29, 5, @"███████╗   ██║   █████╗  ██║     ██║     ███████║ ██████╔╝", ConsoleColor.DarkRed);
            DrawAt(29, 6, @"╚════██║   ██║   ██╔══╝  ██║     ██║     ██╔══██║ ██╔══██╗", ConsoleColor.Red);
            DrawAt(29, 7, @"███████║   ██║   ███████╗███████╗███████╗██║  ██║ ██║  ██║", ConsoleColor.Red);
            DrawAt(29, 8, @"╚══════╝   ╚═╝   ╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝ ╚═╝  ╚═╝", ConsoleColor.DarkRed);

            DrawAt(45, 10, @"██╗    ██╗ █████╗ ██████╗ ", ConsoleColor.DarkRed);
            DrawAt(45, 11, @"██║    ██║██╔══██╗██╔══██╗", ConsoleColor.Red);
            DrawAt(45, 12, @"██║ █╗ ██║███████║██████╔ ", ConsoleColor.Red);
            DrawAt(45, 13, @"██║███╗██║██╔══██║██╔══██╗", ConsoleColor.Red);
            DrawAt(45, 14, @"╚███╔███╔╝██║  ██║██║  ██║", ConsoleColor.DarkRed);
            DrawAt(45, 15, @" ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝", ConsoleColor.DarkRed);


            DrawAt(88, 10, @"     ████", ConsoleColor.Red);
            DrawAt(88, 11, @"   ██    ██", ConsoleColor.Red);
            DrawAt(88, 12, @"  █        █", ConsoleColor.Red);
            DrawAt(88, 13, @"  █        █", ConsoleColor.Red);
            DrawAt(88, 14, @"██████████████", ConsoleColor.Magenta);
            DrawAt(88, 15, @"   ██    ██ ", ConsoleColor.DarkMagenta);
            DrawAt(88, 16, @"     ████", ConsoleColor.DarkMagenta);
            Thread.Sleep(50);
            DrawAt(102, 1, @"  ██████████", ConsoleColor.Magenta);
            DrawAt(102, 2, @" ███████████", ConsoleColor.Magenta);
            DrawAt(102, 3, @"████████████", ConsoleColor.Red);
            DrawAt(102, 4, @"████████████", ConsoleColor.Red);
            DrawAt(102, 5, @" ███████████", ConsoleColor.Red);
            DrawAt(102, 6, @"  ██████████", ConsoleColor.Red);
            DrawAt(102, 7, @"   █████████", ConsoleColor.Yellow);
            DrawAt(102, 8, @"     ███████", ConsoleColor.Yellow);
            Thread.Sleep(50);

            DrawAt(1, 23, @"████                 ♦          ♦        ♦                                      ♦                         ♦    ██", ConsoleColor.Cyan);
            DrawAt(1, 24, @"██████                                               ♦      ♦                                                ████", ConsoleColor.Cyan);
            DrawAt(1, 25, @"███████          ♦                     ♦      ♦         ♦                                            ♦      █████", ConsoleColor.Cyan);
            DrawAt(1, 26, @"████████                                                                                                   ██████", ConsoleColor.Magenta);
            DrawAt(1, 27, @"█████████                                                                                                 ███████", ConsoleColor.DarkMagenta);
            DrawAt(1, 28, @"█████████████████████████████████████████████████████████████████████████████████████████████████████████████████", ConsoleColor.DarkMagenta);
            Thread.Sleep(50);
            DrawAt(25, 19, @" ██", ConsoleColor.Red);
            DrawAt(25, 20, @"████", ConsoleColor.DarkMagenta);
            DrawAt(25, 21, @"████", ConsoleColor.Magenta);
            DrawAt(25, 22, @"█  █", ConsoleColor.Magenta);
            DrawAt(25, 23, @"█  █", ConsoleColor.Magenta);
            DrawAt(25, 24, @"████", ConsoleColor.Magenta);
            DrawAt(25, 25, @"████", ConsoleColor.Red);
            Thread.Sleep(50);
            DrawAt(1, 23, @"████", ConsoleColor.DarkMagenta);
            DrawAt(1, 24, @"██████", ConsoleColor.Magenta);
            DrawAt(1, 25, @"███████", ConsoleColor.Magenta);
            Thread.Sleep(50);
            DrawStringCharByChar(68, 20, @" ████", 1, false, ConsoleColor.DarkMagenta);
            DrawStringCharByChar(68, 21, @"█    █", 1, true, ConsoleColor.Magenta);
            DrawAt(68, 22, @"█ ██ █", ConsoleColor.Magenta);
            DrawAt(65, 23, @"   █ ██ █", ConsoleColor.Magenta);
            DrawAt(65, 24, @" ██████████", ConsoleColor.DarkRed);
            DrawAt(65, 25, @"█          █", ConsoleColor.DarkRed);
            Thread.Sleep(50);
            DrawAt(92, 22, @"  █", ConsoleColor.Yellow);
            DrawAt(92, 23, @" ███", ConsoleColor.Red);
            DrawAt(92, 24, @"█████", ConsoleColor.Red);
            DrawAt(92, 25, @" ███", ConsoleColor.DarkMagenta);
            Thread.Sleep(50);
            DrawAt(109, 23, @"   ██", ConsoleColor.DarkMagenta);
            DrawAt(109, 24, @" ████", ConsoleColor.Magenta);
            DrawAt(109, 25, @"█████", ConsoleColor.Magenta);
            Thread.Sleep(50);





            //Adding decorations 

            DrawAt(11, 9, "  █ █  ", ConsoleColor.Yellow);
            DrawAt(11, 10, "███████", ConsoleColor.Red);
            DrawAt(11, 11, "█ ███ █", ConsoleColor.Red);
            DrawAt(11, 12, "███████", ConsoleColor.Magenta);
            DrawAt(11, 13, " █ █ █", ConsoleColor.DarkMagenta);
            Thread.Sleep(50);





            // Adding menu options
            DrawStringCharByChar(53, 18, @"[P]lay ", 1, false, ConsoleColor.White);
            DrawStringCharByChar(53, 19, @"[S]core Board ", 1, true, ConsoleColor.White);
            DrawStringCharByChar(53, 20, @"[C]redits  ", 1, false, ConsoleColor.White);
            DrawStringCharByChar(53, 21, @"[Q]uit  ", 1, true, ConsoleColor.White);

            // Finishing up the rest
        }
        /// <summary>
        /// Draw Credits screen
        /// SoftUni and GitHub links
        /// linked to main menu
        /// </summary>
        public static void Credits()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 3, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 3, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 3, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 3, true, ConsoleColor.Red);
            DrawAt(90, 1, "♦             ♦", ConsoleColor.Cyan);
            DrawAt(19, 2, "♦             ♦", ConsoleColor.Cyan);
            DrawAt(43, 2, "♦              ♦", ConsoleColor.Cyan);
            DrawAt(9, 3, "♦                 ♦", ConsoleColor.Cyan);
            DrawAt(25, 5, "                                                     ", ConsoleColor.Yellow);
            DrawAt(25, 6, "                                                    ", ConsoleColor.Yellow);
            DrawAt(25, 7, "                                                    ", ConsoleColor.Yellow);
            DrawAt(25, 8, "                                                    ", ConsoleColor.Yellow);
            DrawAt(25, 9, "                                                    ", ConsoleColor.Yellow);
            DrawAt(25, 10, "                                                   ", ConsoleColor.Yellow);
            DrawAt(35, 12, "                ", ConsoleColor.Green);
            DrawAt(35, 13, "           ", ConsoleColor.Gray);
            DrawAt(20, 14, "♦", ConsoleColor.Cyan); // Trang trí thêm dấu ♦
            DrawAt(35, 14, "              ", ConsoleColor.Gray);
            DrawAt(55, 14, "♦", ConsoleColor.Cyan); // Trang trí thêm dấu ♦
            DrawAt(35, 15, "           ", ConsoleColor.Gray);
            DrawAt(35, 16, "              ", ConsoleColor.Gray);

            // Tùy chọn điều hướng
            DrawAt(10, 20, "                   ", ConsoleColor.Yellow);
            DrawAt(55, 20, "                    ", ConsoleColor.Yellow);
            DrawAt(55, 21, "                ", ConsoleColor.Yellow);

            // Trang trí thêm các biểu tượng ♦ ở cuối màn hình
            DrawAt(10, 22, "♦", ConsoleColor.Cyan);
            DrawAt(29, 22, "♦", ConsoleColor.Cyan);
            DrawAt(85, 22, "♦", ConsoleColor.Cyan);
            DrawAt(70, 23, "♦", ConsoleColor.Cyan);
            DrawAt(45, 24, "♦", ConsoleColor.Cyan);

            Thread.Sleep(650);

            DrawAt(25, 5, "██████╗██████╗ ███████╗██████╗ ██╗████████╗███████╗", ConsoleColor.Yellow);
            DrawAt(25, 6, "██╔════╝██╔══██╗██╔════╝██╔══██╗██║╚══██╔══╝██╔════╝", ConsoleColor.Yellow);
            DrawAt(25, 7, "██║     ██████╔╝█████╗  ██║  ██║██║   ██║   ███████╗", ConsoleColor.Yellow);
            DrawAt(25, 8, "██║     ██╔══██╗██╔══╝  ██║  ██║██║   ██║   ╚════██║", ConsoleColor.Yellow);
            DrawAt(25, 9, "╚██████╗██║  ██║███████╗██████╔╝██║   ██║   ███████║", ConsoleColor.Yellow);
            DrawAt(25, 10, " ╚═════╝╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝   ╚═╝   ╚══════╝", ConsoleColor.Yellow);
            Thread.Sleep(1500);
            DrawAt(35, 12, @"         TEAM BỐN CON CỪU:", ConsoleColor.Yellow);
            Thread.Sleep(650);
            DrawAt(35, 13, @"         Lê Quốc Bảo", ConsoleColor.Gray);
            Thread.Sleep(500);
            DrawAt(35, 14, @"         Tăng Thoại Hào", ConsoleColor.Gray);
            Thread.Sleep(500);
            DrawAt(35, 15, @"         Đỗ Việt Anh ", ConsoleColor.Gray);
            Thread.Sleep(500);
            DrawAt(35, 16, @"         Trần Minh Tuấn", ConsoleColor.Gray);
            Thread.Sleep(500);
            DrawAt(15, 20, @"[B]ack to Main Menu", ConsoleColor.Yellow);
            DrawAt(65, 20, @"Give us Feedback at:", ConsoleColor.Yellow);
            DrawAt(65, 21, @"[L]ink to Github", ConsoleColor.Yellow);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.B)
                {
                    Menu.validInput = true;
                    break;
                }
                else if (key.Key == ConsoleKey.L)
                {
                    System.Diagnostics.Process.Start("https://github.com/AdvancedCSharpTeam/AdvancedCSharpProject");
                }
                else if (key.Key == ConsoleKey.S)
                {
                    System.Diagnostics.Process.Start("https://softuni.bg/");
                }
            }
        }
        /// <summary>
        /// Draw Game Over screen
        /// </summary>
        public static void GameOver()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 3, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 3, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 3, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 3, true, ConsoleColor.Red);
            DrawAt(3, 1, "   ♦      ♦                   ♦       ♦               ♦       ♦                   ♦       ♦                    ", ConsoleColor.Cyan);
            DrawAt(1, 2, "                  ♦                                  █████                            ♦                         ", ConsoleColor.Cyan);
            DrawAt(1, 3, "                                                  ███████████            ♦      ♦                               ", ConsoleColor.Cyan);
            DrawAt(1, 4, "                       ♦        ♦                █████████████                          ♦                       ", ConsoleColor.Cyan);
            DrawAt(1, 5, "███████████     ♦                                █████████████       ♦                                          ", ConsoleColor.Cyan);
            DrawAt(1, 6, "          █                                      █████████████          ♦      ♦                       ██████████", ConsoleColor.Cyan);
            DrawAt(1, 7, "          █               ♦       ♦               ███████████    ♦                                     █        ", ConsoleColor.Cyan);
            DrawAt(1, 8, "█████████ █                                          █████                                             █        ", ConsoleColor.Cyan);
            DrawAt(1, 9, "        █ ██                                                                                          ██   ██████", ConsoleColor.Cyan);
            DrawAt(1, 10, "████    █  █                                                                                          █  ███    ", ConsoleColor.Cyan);
            DrawAt(1, 11, "   █    █  ██                                                                                       ███  █      ", ConsoleColor.Cyan);
            DrawAt(1, 12, "   █    █   █      ██████╗  █████╗  ███╗   ███╗███████╗    ██████╗ ██╗   ██╗███████╗██████╗        ███  ███     ", ConsoleColor.Cyan);
            DrawAt(1, 13, "   ███  █   █      ██╔════╝ ██╔══██╗████╗ ████║██╔════╝   ██╔═══██╗██║   ██║██╔════╝██╔══██╗       █    █       ", ConsoleColor.Cyan);
            DrawAt(1, 14, "     █  ██  █      ██║  ███╗███████║██╔████╔██║█████╗     ██║   ██║██║   ██║█████╗  ██████╔        █    ███     ", ConsoleColor.Cyan);
            DrawAt(1, 15, "     █   █  ██     ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝     ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗     ███   ██  ██████", ConsoleColor.Cyan);
            DrawAt(1, 16, "     █    █  █     ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗   ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║     █    ██   █    ", ConsoleColor.Cyan);
            DrawAt(1, 17, "     █    █  ██     ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝    ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝  ████  ███  ███    ", ConsoleColor.Cyan);
            DrawAt(1, 18, "      ██  █   █                                                                               █   ██   ████     ", ConsoleColor.Cyan);
            DrawAt(1, 19, "       █  ██   █                       Made with passion by:                                ██    █    █        ", ConsoleColor.Cyan);
            DrawAt(1, 20, "       █   █   █                                                                            █     █    █        ", ConsoleColor.Cyan);
            DrawAt(1, 21, "       ███ ██  ██                                                                       █████   ██    ██        ", ConsoleColor.Cyan);
            DrawAt(1, 22, "         █  █   █                                                                     ██       █      █         ", ConsoleColor.Cyan);
            DrawAt(1, 23, "         █  █   █                                                                    ██       ██   ████         ", ConsoleColor.Cyan);
            DrawAt(1, 24, "         █  █   █                                                                    █     ███     █            ", ConsoleColor.Cyan);
            DrawAt(1, 25, "         ██ ██  █                                                                    █     █      ██            ", ConsoleColor.Cyan);
            DrawAt(1, 26, "          █  █  █                                                                    █   ██       █             ", ConsoleColor.Cyan);
            DrawAt(1, 27, "          █  █  █                                                                    █   █      ███             ", ConsoleColor.Cyan);
            DrawAt(1, 28, "          █  █  █                                                                    █   █      █               ", ConsoleColor.Cyan);

            DrawAt(40, 19, "Made with passion by: 4Sheep", ConsoleColor.White);
            DrawAt(50, 2, "    █████    ", ConsoleColor.DarkYellow);
            DrawAt(50, 3, " ███████████ ", ConsoleColor.DarkYellow);
            DrawAt(50, 4, "█████████████", ConsoleColor.Yellow);
            DrawAt(50, 5, "█████████████", ConsoleColor.Yellow);
            DrawAt(50, 6, "█████████████", ConsoleColor.Yellow);
            DrawAt(50, 7, " ███████████ ", ConsoleColor.DarkYellow);
            DrawAt(50, 8, "    █████    ", ConsoleColor.DarkYellow);


            DrawStringCharByChar(20, 12, @"██████╗  █████╗  ███╗   ███╗███████╗    ██████╗ ██╗   ██╗███████╗██████╗ ", 2, false, ConsoleColor.DarkYellow);
            DrawStringCharByChar(20, 13, @"██╔════╝ ██╔══██╗████╗ ████║██╔════╝   ██╔═══██╗██║   ██║██╔════╝██╔══██╗", 2, true, ConsoleColor.DarkYellow);
            DrawStringCharByChar(20, 14, @"██║  ███╗███████║██╔████╔██║█████╗     ██║   ██║██║   ██║█████╗  ██████╔", 2, false, ConsoleColor.Yellow);
            DrawStringCharByChar(20, 15, @"██║   ██║██╔══██║██║╚██╔╝██║██╔══╝     ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗", 2, true, ConsoleColor.Yellow);
            DrawStringCharByChar(20, 16, @"╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗   ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║", 3, false, ConsoleColor.Yellow);
            DrawStringCharByChar(20, 17, @" ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝    ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝", 3, true, ConsoleColor.DarkYellow);
            Thread.Sleep(600);
            DrawAt(45, 21, @"[P]lay Again", ConsoleColor.Green);
            DrawAt(45, 22, @"[Q]uit The Game", ConsoleColor.Red);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.P)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
            }

        }
        /// <summary>
        /// Draw Enter Name screen
        /// </summary>
        public static void EnterName()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Magenta);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Magenta);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Magenta);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Magenta);
            DrawAt(17, 1, "♦                                       ♦               ♦     ♦                          ♦", ConsoleColor.Cyan);
            DrawAt(4, 2, "♦                                                                                                            ♦", ConsoleColor.Cyan);
            DrawAt(12, 3, "♦                                                                                       ", ConsoleColor.Cyan);
            DrawAt(13, 4, "                                                                                         ♦", ConsoleColor.Cyan);
            DrawAt(3, 5, "♦                                                                                     ", ConsoleColor.Cyan);
            DrawAt(13, 6, "                                                                                            ♦", ConsoleColor.Cyan);
            DrawAt(9, 7, "♦                                                                                      ", ConsoleColor.Cyan);
            DrawAt(13, 8, "                                                                                    ", ConsoleColor.Cyan);
            DrawAt(19, 9, "♦                                                                                   ", ConsoleColor.Cyan);
            DrawAt(36, 10, "♦        ♦                                                               ♦", ConsoleColor.Cyan);
            DrawAt(4, 11, "♦    ♦     ♦          ♦   ♦                                ♦    ♦       ♦     ♦                          ♦", ConsoleColor.Cyan);
            DrawAt(83, 12, "♦        ♦", ConsoleColor.Cyan);
            DrawAt(40, 13, "                 ♦                            ♦           ♦", ConsoleColor.Cyan);
            DrawAt(10, 14, "♦                ♦           ♦", ConsoleColor.Cyan);
            DrawAt(3, 15, "♦        ♦                               ♦                                  ♦       ♦", ConsoleColor.Cyan);
            DrawAt(1, 16, "                     ♦    █                        ♦", ConsoleColor.Cyan);
            DrawAt(1, 17, "         ♦               ███                                                         ███        █████         ♦♦", ConsoleColor.Cyan);
            DrawAt(1, 18, "                        █████                                   █████████           ████        █████", ConsoleColor.Cyan);
            DrawAt(1, 19, "                        █ █ █                                   █████████          █████       ███████", ConsoleColor.Cyan);
            DrawAt(1, 20, "          ♦             █████                                   █ █ █ █ █          █ █ █       █ █ █ █", ConsoleColor.Cyan);
            DrawAt(1, 21, "                        █ █ █        ██████████████████         █████████          █████       ███████", ConsoleColor.Cyan);
            DrawAt(1, 22, "                        █████        ██████████████████         █ █ █ █ █          █ █ █       █ █ █ █", ConsoleColor.Cyan);
            DrawAt(1, 23, "████                    █ █ █        ██ ██ █    █ ██ ██         █████████          █████       ███████       ████", ConsoleColor.Cyan);
            DrawAt(1, 24, "██████          █       █████        ██ ██ █ ████ ██ ██         █ █ █ █ █          █ █ █       █ █ █ █     ██████", ConsoleColor.Cyan);
            DrawAt(1, 25, "███████        ███      █ █ █        ██ ██ █    █    ██         █████████          █████       ███████    ███████", ConsoleColor.Cyan);
            DrawAt(1, 26, "████████      █████     █████        ██ ██ █ ████ ██ ██         █ █ █ █ █          █ █ █       █ █ █ █   ████████", ConsoleColor.Cyan);
            DrawAt(1, 27, "████████      █████     █████        ██    █    █ ██ ██         █████████          █████       ███████  █████████", ConsoleColor.Cyan);
            DrawAt(1, 28, "██████████████████████████████████████████████████████████████████████████████████████████████████████████████████", ConsoleColor.Black);

            DrawAt(1, 23, "████", ConsoleColor.DarkMagenta);
            DrawAt(1, 24, "██████", ConsoleColor.Magenta);
            DrawAt(1, 25, "███████", ConsoleColor.Magenta);
            DrawAt(1, 26, "████████", ConsoleColor.Magenta);
            DrawAt(1, 27, "████████", ConsoleColor.DarkMagenta);
            DrawAt(1, 28, "█████████", ConsoleColor.DarkMagenta);

            DrawAt(14, 24, "   █    ", ConsoleColor.DarkMagenta);
            DrawAt(14, 25, "  ███   ", ConsoleColor.Magenta);
            DrawAt(14, 26, " █████  ", ConsoleColor.Magenta);
            DrawAt(14, 27, " █████  ", ConsoleColor.Magenta);
            DrawAt(14, 28, "███████ ", ConsoleColor.DarkMagenta);

            DrawAt(24, 16, "   █   ", ConsoleColor.DarkMagenta);
            DrawAt(24, 17, "  ███  ", ConsoleColor.DarkMagenta);
            DrawAt(24, 18, " █████ ", ConsoleColor.DarkMagenta);
            DrawAt(24, 19, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(24, 20, " █████ ", ConsoleColor.Magenta);
            DrawAt(24, 21, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(24, 22, " █████ ", ConsoleColor.Magenta);
            DrawAt(24, 23, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(24, 24, " █████ ", ConsoleColor.Magenta);
            DrawAt(24, 25, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(24, 26, " █████ ", ConsoleColor.Magenta);
            DrawAt(24, 27, " █████ ", ConsoleColor.DarkMagenta);
            DrawAt(24, 28, "███████", ConsoleColor.DarkMagenta);

            DrawAt(37, 21, " ██████████████████  ", ConsoleColor.DarkMagenta);
            DrawAt(37, 22, " ██████████████████  ", ConsoleColor.DarkMagenta);
            DrawAt(37, 23, " ██ ██ █    █ ██ ██  ", ConsoleColor.Magenta);
            DrawAt(37, 24, " ██ ██ █ ████ ██ ██  ", ConsoleColor.Magenta);
            DrawAt(37, 25, " ██ ██ █    █    ██  ", ConsoleColor.Magenta);
            DrawAt(37, 26, " ██ ██ █ ████ ██ ██  ", ConsoleColor.Magenta);
            DrawAt(37, 27, " ██    █    █ ██ ██  ", ConsoleColor.Magenta);
            DrawAt(37, 28, "████████████████████ ", ConsoleColor.DarkMagenta);

            DrawAt(64, 18, " █████████ ", ConsoleColor.DarkMagenta);
            DrawAt(64, 19, " █████████ ", ConsoleColor.DarkMagenta);
            DrawAt(64, 20, " █ █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(64, 21, " █████████ ", ConsoleColor.Magenta);
            DrawAt(64, 22, " █ █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(64, 23, " █████████ ", ConsoleColor.Magenta);
            DrawAt(64, 24, " █ █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(64, 25, " █████████ ", ConsoleColor.Magenta);
            DrawAt(64, 26, " █ █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(64, 27, " █████████ ", ConsoleColor.Magenta);
            DrawAt(64, 28, "███████████", ConsoleColor.DarkMagenta);

            DrawAt(83, 17, "   ███ ", ConsoleColor.DarkMagenta);
            DrawAt(83, 18, "  ████ ", ConsoleColor.DarkMagenta);
            DrawAt(83, 19, " █████ ", ConsoleColor.DarkMagenta);
            DrawAt(83, 20, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(83, 21, " █████ ", ConsoleColor.Magenta);
            DrawAt(83, 22, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(83, 23, " █████ ", ConsoleColor.Magenta);
            DrawAt(83, 24, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(83, 25, " █████ ", ConsoleColor.Magenta);
            DrawAt(83, 26, " █ █ █ ", ConsoleColor.Magenta);
            DrawAt(83, 27, " █████ ", ConsoleColor.Magenta);
            DrawAt(83, 28, "███████", ConsoleColor.DarkMagenta);

            DrawAt(95, 17, "  █████  ", ConsoleColor.DarkMagenta);
            DrawAt(95, 18, "  █████  ", ConsoleColor.DarkMagenta);
            DrawAt(95, 19, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 20, " █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(95, 21, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 22, " █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(95, 23, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 24, " █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(95, 25, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 26, " █ █ █ █ ", ConsoleColor.Magenta);
            DrawAt(95, 27, " ███████ ", ConsoleColor.Magenta);
            DrawAt(95, 28, "█████████", ConsoleColor.DarkMagenta);

            DrawAt(105, 23, "     ████", ConsoleColor.Magenta);
            DrawAt(105, 24, "   ██████", ConsoleColor.Magenta);
            DrawAt(105, 25, "  ███████", ConsoleColor.Magenta);
            DrawAt(105, 26, " ████████", ConsoleColor.Magenta);
            DrawAt(105, 27, " ████████", ConsoleColor.DarkMagenta);
            DrawAt(105, 28, "█████████", ConsoleColor.DarkMagenta);

            DrawAt(114, 28, "█", ConsoleColor.Magenta);




            Thread.Sleep(250);
            DrawAt(1, 3, "               ███████╗███╗   ██╗████████╗███████╗██████╗     ███╗   ██╗ █████╗ ███╗   ███╗███████╗", ConsoleColor.DarkMagenta);
            Thread.Sleep(250);
            DrawAt(1, 4, "               ██╔════╝████╗  ██║╚══██╔══╝██╔════╝██╔══██╗    ████╗  ██║██╔══██╗████╗ ████║██╔════╝      ", ConsoleColor.DarkMagenta);
            Thread.Sleep(250);
            DrawAt(1, 5, "               █████╗  ██╔██╗ ██║   ██║   █████╗  ██████╔╝    ██╔██╗ ██║███████║██╔████╔██║█████╗", ConsoleColor.Magenta);
            Thread.Sleep(250);
            DrawAt(1, 6, "               ██╔══╝  ██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗    ██║╚██╗██║██╔══██║██║╚██╔╝██║██╔══╝           ", ConsoleColor.Magenta);
            Thread.Sleep(250);
            DrawAt(1, 7, "               ███████╗██║ ╚████║   ██║   ███████╗██║  ██║    ██║ ╚████║██║  ██║██║ ╚═╝ ██║███████╗", ConsoleColor.DarkMagenta);
            Thread.Sleep(250);
            DrawAt(1, 8, "               ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝    ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝", ConsoleColor.DarkMagenta);




        }
        /// <summary>
        /// Draw Load Content screen
        /// </summary>
        public static void LoadContent()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Red);

            DrawAt(1, 1, @"██████                                                                                                         ", ConsoleColor.Gray);
            DrawAt(1, 2, @"██████        •          •   •                               •                               ████              ", ConsoleColor.White);
            DrawAt(1, 3, @"█████               •                      •                           ███████         ███  █    █      ███████", ConsoleColor.White);
            DrawAt(1, 4, @"████                                               ──                    █████        █   █ ████        █████  ", ConsoleColor.Yellow);
            DrawAt(1, 5, @"██                                                                        ████████     ███████ █    ████████   ", ConsoleColor.Gray);
            DrawAt(1, 6, @"                                          ──                ──             ██████████  █ ███████ ██████████    ", ConsoleColor.Yellow);
            DrawAt(1, 7, @"                        •                                                              ████████████          ", ConsoleColor.White);
            DrawAt(1, 8, @"                                                   ──                             █          ██          █     ", ConsoleColor.Yellow);
            DrawAt(1, 9, @"                                    ──                     ──      ──   ──    █████          ██          █████ ", ConsoleColor.Yellow);
            DrawAt(1, 10, @"                                           ─             ──                  ██████  ███ ██████████ ███ ██████ ", ConsoleColor.Yellow);
            DrawAt(1, 11, @"                                                           ──                      █████ █ █████ ██ █████      ", ConsoleColor.Yellow);
            DrawAt(1, 12, @"                                      ──           ──             ──         █████████   ████  ████   █████████", ConsoleColor.Yellow);
            DrawAt(1, 13, @"                                                                            ████████  ██████  ██████  ████████", ConsoleColor.Gray);
            DrawAt(1, 14, @"                                              ──                            ████          █    █          ████", ConsoleColor.Yellow);
            DrawAt(1, 15, @"                                   ──                  ──                                                     ", ConsoleColor.Yellow);
            DrawAt(1, 16, @"                                                               ──                                             ", ConsoleColor.Yellow);
            DrawAt(1, 17, @"                                                         ──                                                   ", ConsoleColor.Yellow);
            DrawAt(1, 18, @"                                                                                                              ", ConsoleColor.Gray);
            DrawAt(1, 19, @"                                                                                        ██                    ", ConsoleColor.Gray);
            DrawAt(1, 20, @"                       ███                                                              ██                    ", ConsoleColor.Gray);
            DrawAt(1, 21, @"                       ███                                                              ██                    ", ConsoleColor.Gray);
            DrawAt(1, 22, @"██                      █                              ██                               ██                    ", ConsoleColor.Gray);
            DrawAt(1, 23, @"████                    █                              ██                             █████                   ", ConsoleColor.Gray);
            DrawAt(1, 24, @"█████                  ███                             ██             •              ██████                   ", ConsoleColor.Gray);
            DrawAt(1, 25, @"██████               •█████                           ████                         █████████               ██████", ConsoleColor.DarkRed);
            DrawAt(1, 26, @"███████              ███████                         ██████                     █████████████             ███████", ConsoleColor.Red);
            DrawAt(1, 27, @"█████████     ██████████████████████       █████████████████████        █████████████████████████        ████████", ConsoleColor.DarkRed);
            DrawAt(1, 28, @"██████████   ████████████████████████     ███████████████████████      ███████████████████████████      █████████", ConsoleColor.DarkRed);

            DrawAt(13, 6, @"█", ConsoleColor.Yellow);
            DrawAt(13, 7, @" █", ConsoleColor.Yellow);
            DrawAt(13, 8, @" ██", ConsoleColor.Yellow);
            DrawAt(13, 9, @"████", ConsoleColor.Yellow);
            DrawAt(13, 10, @" ██", ConsoleColor.Yellow);
            DrawAt(13, 11, @" █", ConsoleColor.Yellow);
            DrawAt(13, 12, @"█", ConsoleColor.Yellow);



            DrawAt(72, 2, @"                      ████", ConsoleColor.DarkMagenta);
            DrawAt(72, 3, @"███████         ███  █    █      ███████", ConsoleColor.DarkMagenta);
            DrawAt(72, 4, @"  █████        █   █ ████        █████  ", ConsoleColor.DarkMagenta);
            DrawAt(72, 5, @"   ████████     ███████ █     ████████  ", ConsoleColor.Magenta);
            DrawAt(72, 6, @"    ██████████  █ ███████ ██████████    ", ConsoleColor.Magenta);
            DrawAt(72, 7, @"                ████████████            ", ConsoleColor.Magenta);
            DrawAt(72, 8, @"           █          ██          █     ", ConsoleColor.Magenta);
            DrawAt(72, 9, @"       █████          ██          █████ ", ConsoleColor.Magenta);
            DrawAt(72, 10, @"       ██████  ███ ██████████ ███ ██████", ConsoleColor.Magenta);
            DrawAt(72, 11, @"             █████ █ █████ ██ █████     ", ConsoleColor.Magenta);
            DrawAt(72, 12, @"       █████████   ████  ████   █████████", ConsoleColor.Magenta);
            DrawAt(72, 13, @"      ████████  ██████  ██████  ████████ ", ConsoleColor.DarkMagenta);
            DrawAt(72, 14, @"      ████          █    █          ████ ", ConsoleColor.DarkMagenta);


            DrawAt(1, 22, @"██", ConsoleColor.DarkRed);
            DrawAt(1, 23, @"████", ConsoleColor.DarkRed);
            DrawAt(1, 24, @"█████", ConsoleColor.Red);
            DrawAt(1, 25, @"██████", ConsoleColor.Red);
            DrawAt(1, 26, @"███████", ConsoleColor.Red);
            DrawAt(1, 27, @"█████████", ConsoleColor.DarkRed);
            DrawAt(1, 28, @"██████████", ConsoleColor.DarkRed);

            DrawAt(14, 20, @"          ███", ConsoleColor.DarkRed);
            DrawAt(14, 21, @"          ███", ConsoleColor.Red);
            DrawAt(14, 22, @"           █ ", ConsoleColor.DarkRed);
            DrawAt(14, 23, @"           █ ", ConsoleColor.Red);
            DrawAt(14, 24, @"          ███", ConsoleColor.Red);
            DrawAt(14, 25, @"         █████", ConsoleColor.Red);
            DrawAt(14, 26, @"        ███████", ConsoleColor.Red);
            DrawAt(14, 27, @" ██████████████████████", ConsoleColor.DarkRed);
            DrawAt(14, 28, @"████████████████████████", ConsoleColor.DarkRed);

            DrawAt(43, 22, @"             ██", ConsoleColor.DarkRed);
            DrawAt(43, 23, @"             ██", ConsoleColor.Red);
            DrawAt(43, 24, @"             ██", ConsoleColor.DarkRed);
            DrawAt(43, 25, @"            ████", ConsoleColor.Red);
            DrawAt(43, 26, @"           ██████", ConsoleColor.Red);
            DrawAt(43, 27, @" █████████████████████", ConsoleColor.DarkRed);
            DrawAt(43, 28, @"███████████████████████", ConsoleColor.DarkRed);

            DrawAt(72, 19, @"                 ██", ConsoleColor.DarkRed);
            DrawAt(72, 20, @"                 ██", ConsoleColor.Red);
            DrawAt(72, 21, @"                 ██", ConsoleColor.Red);
            DrawAt(72, 22, @"                 ██", ConsoleColor.DarkRed);
            DrawAt(72, 23, @"               █████", ConsoleColor.DarkRed);
            DrawAt(72, 24, @"              ██████", ConsoleColor.Red);
            DrawAt(72, 25, @"            █████████", ConsoleColor.Red);
            DrawAt(72, 26, @"         █████████████", ConsoleColor.Red);
            DrawAt(72, 27, @" █████████████████████████", ConsoleColor.DarkRed);
            DrawAt(72, 28, @"███████████████████████████", ConsoleColor.DarkRed);

            DrawAt(1, 1, @"██████", ConsoleColor.DarkRed);
            DrawAt(1, 2, @"██████", ConsoleColor.DarkRed);
            DrawAt(1, 3, @"█████", ConsoleColor.DarkRed);
            DrawAt(1, 4, @"████", ConsoleColor.DarkRed);
            DrawAt(1, 5, @"██", ConsoleColor.DarkRed);

            DrawAt(30, 19, @"Loading content", ConsoleColor.Yellow);
            DrawHLineAt(45, 19, 20, '\u2588', 100, false, ConsoleColor.Yellow);
            DrawAt(35, 22, @"Tip: W,S,D,A - to control the ship,", ConsoleColor.Green);
            DrawAt(35, 23, @"Space - to fire", ConsoleColor.Green);


        }
        /// <summary>
        /// Draw Load Story screen
        /// </summary>
        public static void LoadStory()
        {
            DrawHLineAt(0, 0, 115, '\u2588', 1, false, ConsoleColor.Red);
            DrawVLineAt(114, 0, 30, '\u2588', 1, false, ConsoleColor.Red);
            DrawHLineAt(114, 29, 115, '\u2588', 1, true, ConsoleColor.Red);
            DrawVLineAt(0, 29, 30, '\u2588', 1, true, ConsoleColor.Red);

            DrawAt(1, 1, @"████████            ♦               ♦                            ♦                                       ████████", ConsoleColor.Cyan);
            DrawAt(1, 2, @"███████            ♦               ♦                            ♦                                         ███████", ConsoleColor.Cyan);
            DrawAt(1, 3, @"█████                                                                                 ♦                     █████", ConsoleColor.Cyan);
            DrawAt(1, 4, @"███                                                  ♦                        ♦                               ███", ConsoleColor.Cyan);
            DrawAt(1, 5, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 6, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 7, @"█                  ♦                                                                            ♦               █", ConsoleColor.Cyan);
            DrawAt(1, 8, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 9, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 10, @"█      ♦                                                                                ♦                       █", ConsoleColor.Cyan);
            DrawAt(1, 11, @"█                                                                                    █   █   █                  █", ConsoleColor.Cyan);
            DrawAt(1, 12, @"█              ♦                                                                    ███  █  ███       ♦         █", ConsoleColor.Cyan);
            DrawAt(1, 13, @"█                                                                                  ██ ███████ ██                █", ConsoleColor.Cyan);
            DrawAt(1, 14, @"█                                                                                 ██   ██ ██   ██               █", ConsoleColor.Cyan);
            DrawAt(1, 15, @"█                                                                                 ██           ██               █", ConsoleColor.Cyan);
            DrawAt(1, 16, @"█                        ♦                                                        ██ ██     ██ ██   ♦           █", ConsoleColor.Cyan);
            DrawAt(1, 17, @"█                                                                                 ██  ██   ██  ██               █", ConsoleColor.Cyan);
            DrawAt(1, 18, @"█                                                                                 ██   █████   ██               █", ConsoleColor.Cyan);
            DrawAt(1, 19, @"█        ♦                                                                        ██    ███    ██               █", ConsoleColor.Cyan);
            DrawAt(1, 20, @"█                    ♦        ♦                         ♦             ♦           ███    █    ███               █", ConsoleColor.Cyan);
            DrawAt(1, 21, @"█                                                                                  ███       ███                █", ConsoleColor.Cyan);
            DrawAt(1, 22, @"█                                                                                   ███     ███                 █", ConsoleColor.Cyan);
            DrawAt(1, 23, @"█                                                                                    ███   ███                  █", ConsoleColor.Cyan);
            DrawAt(1, 24, @"█                         ♦              ♦                                             █████                    █", ConsoleColor.Cyan);
            DrawAt(1, 25, @"█                                                                                                               █", ConsoleColor.Cyan);
            DrawAt(1, 26, @"██                                                                                                             ██", ConsoleColor.Cyan);
            DrawAt(1, 27, @"███                                                                                                           ███", ConsoleColor.Cyan);
            DrawAt(1, 28, @"████                                                                                                         ████", ConsoleColor.Cyan);


            DrawAt(16, 5, @"          ███", ConsoleColor.Blue);
            DrawAt(16, 6, @"         █  █", ConsoleColor.Blue);
            DrawAt(16, 7, @"        █  █", ConsoleColor.Blue);
            DrawAt(16, 8, @"       █  █", ConsoleColor.Blue);
            DrawAt(16, 9, @" █    █  █", ConsoleColor.Blue);
            DrawAt(16, 10, @"  █  █  █", ConsoleColor.Blue);
            DrawAt(16, 11, @"   ██  █", ConsoleColor.Blue);
            DrawAt(16, 12, @"   ████", ConsoleColor.Blue);
            DrawAt(16, 13, @"   █████", ConsoleColor.Blue);
            DrawAt(16, 14, @"  ███   █", ConsoleColor.Blue);
            DrawAt(16, 15, @" ███     █", ConsoleColor.Blue);
            DrawAt(16, 16, @"███", ConsoleColor.Blue);
            DrawAt(16, 17, @"██", ConsoleColor.Blue);

            DrawAt(83, 11, @"   █   █   █", ConsoleColor.White);
            DrawAt(83, 12, @"  ███  █  ███", ConsoleColor.White);
            DrawAt(83, 13, @" ██ ███████ ██", ConsoleColor.White);
            DrawAt(83, 14, @"██   ██ ██   ██", ConsoleColor.White);
            DrawAt(83, 15, @"██           ██", ConsoleColor.White);
            DrawAt(83, 16, @"██           ██", ConsoleColor.White);
            DrawAt(83, 17, @"██           ██", ConsoleColor.White);
            DrawAt(83, 18, @"██           ██", ConsoleColor.White);
            DrawAt(83, 19, @"██           ██", ConsoleColor.White);
            DrawAt(83, 20, @"███         ███", ConsoleColor.White);
            DrawAt(83, 21, @" ███       ███", ConsoleColor.White);
            DrawAt(83, 22, @"  ███     ███", ConsoleColor.White);
            DrawAt(83, 23, @"   ███   ███", ConsoleColor.White);
            DrawAt(83, 24, @"     █████", ConsoleColor.White);

            DrawAt(86, 16, @"██     ██", ConsoleColor.Red);
            DrawAt(86, 17, @" ██   ██ ", ConsoleColor.Red);
            DrawAt(86, 18, @"  █████  ", ConsoleColor.Red);
            DrawAt(86, 19, @"   ███   ", ConsoleColor.Red);
            DrawAt(86, 20, @"    █    ", ConsoleColor.Red);

            Thread.Sleep(650);

            DrawAt(36, 9, @"Hành tinh của chúng tôi đã bị phá hủy", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(43, 10, @"không còn lại gì cả,", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(36, 11, @"bạn là hi vọng cuối cùng của chúng tôi.", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(38, 12, @"Chúng tôi hi vọng bạn có thể", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(33, 13, @"tiêu diệt chúng mặc dù chúng rất mạnh mẽ", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(29, 14, @"Chúc bạn may mắn trên chuyến hành trình đầy khó khăn", ConsoleColor.DarkYellow);
            Thread.Sleep(1050);
            DrawAt(35, 15, @"Xin hãy cứu lấy hành tinh của chúng tôi ", ConsoleColor.DarkYellow);


        }
        #endregion

        #region Clearing Methods

        /// <summary>
        /// Clear a character at given position
        /// </summary>
        /// <param name="x" >Column number</param>
        /// <param name="y" >Row number</param>
        public static void ClearAtPosition(int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(' ');
            }
            catch { return; }

        }

        /// <summary>
        /// Clear a character at given position
        /// </summary>
        /// <param name="point">Point2D to clear at</param>
        public static void ClearAtPosition(Point2D point)
        {
            try
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write(' ');
            }
            catch { return; }
        }

        /// <summary>
        /// Clears an area from given coordinates to given coordinates
        /// </summary>
        /// <param name="fromX"> Starting Column number</param>
        /// <param name="fromY"> Starting Row number</param>
        /// <param name="toX">Ending Column number</param>
        /// <param name="toY">Ending Row number</param>
        public static void ClearFromTo(int fromX, int fromY, int toX, int toY)
        {
            try
            {
                Console.SetCursorPosition(fromX, fromY);
                string x = new string(' ', toX - fromX);
                for (int i = fromY; i < toY; i++)
                {
                    Console.WriteLine(x);
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Clears an area from given coordinates to given coordinates
        /// </summary>
        /// <param name="startingPoint">Starting Point2D</param>
        /// <param name="endingPoint">Ending Point2D</param>
        public static void ClearFromTo(Point2D startingPoint, Point2D endingPoint)
        {
            try
            {
                ClearFromTo(startingPoint.X, startingPoint.Y, endingPoint.X, endingPoint.Y);
            }
           catch { return; }
        }

        /// <summary>
        /// Clears a whole row at given position
        /// </summary>
        /// <param name="y">Row number</param>
        public static void ClearY(int y)
        {
            try
            {
                int gameWidth = 115; // should be assigned from a constant somewhere
                for (int i = 0; i < gameWidth; i++)
                {
                    DrawAt(i, y, ' ');
                }
            }
          catch { return; }
        }

        /// <summary>
        /// Clears a whole column at given position
        /// </summary>
        /// <param name="x">Column number</param>
        public static void ClearX(int x)
        {
            try
            {
                int gameHeight = 30; // should be assigned from a constant somewhere
                for (int i = 0; i < gameHeight; i++)
                {
                    DrawAt(x, i, ' ');
                }
            }
            catch { return; }
           
        }
        #endregion      
    }
}