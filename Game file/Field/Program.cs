using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Meteor
{
    internal class Program
    {
       /* static void Main()
        {
              Console.OutputEncoding = Encoding.Unicode;
            MeteorSimulation simulation = new MeteorSimulation();
            simulation.Run();
        }*/
        public class Meteor
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Size { get; private set; } // Kích thước thiên thạch
            private const int FallSpeed = 1; // Tốc độ rơi cố định cho tất cả các thiên thạch

            public Meteor(int x, int y, int size)
            {
                X = x;
                Y = y;
                Size = size;
            }

            public void UpdatePosition()
            {
                Y += FallSpeed; // Rơi xuống với tốc độ cố định
            }

            public string GetBlockChar()
            {
                return new string('█', Size); // Trả về chuỗi đại diện cho thiên thạch
            }
        }

        public class MeteorSimulation
        {
            private const int ScreenWidth = 115;
            private const int ScreenHeight = 30;
            private Random random = new Random();
            private List<Meteor> meteors = new List<Meteor>();
            private int excludedColumn;
            private int timeSinceLastChange = 0;
            private const int ChangeInterval = 3000;

            private List<int> meteorSizes = new List<int> { 2, 3, 4, 5 };

            public MeteorSimulation()
            {
                // No player initialization
            }

            public void Run()
            {
                excludedColumn = random.Next(0, ScreenWidth - 5);
                int meteorSpawnRate = 4;
                int spawnCounter = 0;

                while (true) // Infinite loop without player lives
                {
                    timeSinceLastChange += 100;
                    if (timeSinceLastChange >= ChangeInterval)
                    {
                        excludedColumn = random.Next(0, ScreenWidth - 5);
                        timeSinceLastChange = 0;
                    }

                    spawnCounter++;
                    if (spawnCounter >= meteorSpawnRate)
                    {
                        spawnCounter = 0;
                        int meteorColumn = random.Next(0, ScreenWidth - 5);
                        if (meteorColumn != excludedColumn)
                        {
                            int size = meteorSizes[random.Next(meteorSizes.Count)];
                            meteors.Add(new Meteor(meteorColumn, 0, size));
                        }
                    }

                    // Update meteors
                    foreach (var meteor in meteors.ToList())
                    {
                        meteor.UpdatePosition();

                        if (meteor.Y >= ScreenHeight)
                        {
                            meteors.Remove(meteor); // Remove off-screen meteors
                        }
                    }

                    // Clear console and draw everything
                    Console.Clear();
                    foreach (var meteor in meteors)
                    {
                        for (int i = 0; i < meteor.Size; i++)
                        {
                            Console.SetCursorPosition(meteor.X + i, meteor.Y);
                            Console.Write('█');
                        }
                    }

                    // Control speed
                    Thread.Sleep(50);
                }
            }
        }
        public static class Printing
        {
            public static void DrawAt(int x, int y, object obj)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(obj.ToString());
            }


            /// <summary>
            /// Draw an object at a Point2D
            /// </summary>
            /// <param name="point">Point2D to print at</param>
            /// <param name="obj">Object to print</param>
            public static void DrawAt(Point2D point, object obj)
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write(obj.ToString());
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
                Console.ForegroundColor = clr;
                DrawAt(x, y, obj);
                Console.ResetColor();
            }

            /// <summary>
            /// Draw an object at given Point2D with color
            /// </summary>
            /// <param name="point">Point2D to print at</param>
            /// <param name="obj">Object to print</param>
            /// <param name="clr">Color to print with</param>
            public static void DrawAt(Point2D point, object obj, ConsoleColor clr)
            {
                Console.ForegroundColor = clr;
                DrawAt(point, obj);
                Console.ResetColor();
            }

            public static void DrawAtBG(int x, int y, object obj, ConsoleColor bclr)
            {
                Console.BackgroundColor = bclr;
                DrawAt(x, y, obj);
                Console.ResetColor();
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
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    y++;
                }

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

            /// <summary>
            /// Draw a vertical line with given lenght starting at Point2D
            /// </summary>
            /// <param name="point">Point2D to print at</param>
            /// <param name="lenght">Length of the line</param>
            /// <param name="obj">Object to print</param>
            /// <param name="clr">Color to print with</param>
            public static void DrawVLineAt(Point2D point, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
            {
                DrawVLineAt(point.X, point.Y, lenght, obj, clr);
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
                for (int i = 0; i < lenght; i++)
                {
                    DrawAt(x, y, obj, clr);
                    x++;
                }
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

            /// <summary>
            /// Draw a horizontal line with given lenght starting at Point2D
            /// </summary>
            /// <param name="point">Point2D to print at</param>
            /// <param name="lenght">Lenght of the line</param>
            /// <param name="obj">Object to print</param>
            /// <param name="clr">Color to print with</param>
            public static void DrawHLineAt(Point2D point, int lenght, object obj, ConsoleColor clr = ConsoleColor.White)
            {
                DrawHLineAt(point.X, point.Y, lenght, obj, clr);
            }

            /// <summary>
            /// Draw a Rectangle starting at given X Y position with set size
            /// </summary>
            /// <param name="x">Column number</param>
            /// <param name="y">Row number</param>
            /// <param name="size">Size of the rectangle</param>
            /// <param name="obj">Object to print</param>
            /// <param name="clr">Color to print with</param>
            public static void DrawRectangleAt(int x, int y, int size, object obj, ConsoleColor clr = ConsoleColor.White)
            {

                for (int i = 0, side1 = 0; i < size; i++)
                {
                    DrawAt(x + side1++, y, obj, clr);
                }

                for (int i = 0, side2 = 0; i < size; i++)
                {
                    DrawAt(x + size - 1, y + side2++, obj, clr);
                }

                for (int i = 0, side3 = 0; i < size; i++)
                {
                    DrawAt(x + side3++, y + size, obj, clr);
                }

                for (int i = 0, side4 = 0; i < size; i++)
                {
                    DrawAt(x, y + side4++, obj, clr);
                }
            }

            /// <summary>
            /// Draw a Rectangle starting at position Point2D with given size
            /// </summary>
            /// <param name="point">Point2D to start at</param>
            /// <param name="size">Size of the rectangle</param>
            /// <param name="obj">Object to print</param>
            /// <param name="clr">Color to print with</param>
            public static void DrawRectangleAt(Point2D point, int size, object obj, ConsoleColor clr = ConsoleColor.White)
            {
                DrawRectangleAt(point.X, point.Y, size, obj, clr);
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


            public static void ClearAtPosition(int x, int y)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(' ');
            }

            /// <summary>
            /// Clear a character at given position
            /// </summary>
            /// <param name="point">Point2D to clear at</param>
            public static void ClearAtPosition(Point2D point)
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write(' ');
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
                Console.SetCursorPosition(fromX, fromY);
                string x = new string(' ', toX - fromX);
                for (int i = fromY; i < toY; i++)
                {
                    Console.WriteLine(x);
                }
            }

            /// <summary>
            /// Clears an area from given coordinates to given coordinates
            /// </summary>
            /// <param name="startingPoint">Starting Point2D</param>
            /// <param name="endingPoint">Ending Point2D</param>
            public static void ClearFromTo(Point2D startingPoint, Point2D endingPoint)
            {
                ClearFromTo(startingPoint.X, startingPoint.Y, endingPoint.X, endingPoint.Y);
            }

            /// <summary>
            /// Clears a whole row at given position
            /// </summary>
            /// <param name="y">Row number</param>
            public static void ClearY(int y)
            {
                int gameWidth = 80; // should be assigned from a constant somewhere
                for (int i = 0; i < gameWidth; i++)
                {
                    DrawAt(i, y, ' ');
                }
            }

            /// <summary>
            /// Clears a whole column at given position
            /// </summary>
            /// <param name="x">Column number</param>
            public static void ClearX(int x)
            {
                int gameHeight = 30; // should be assigned from a constant somewhere
                for (int i = 0; i < gameHeight; i++)
                {
                    DrawAt(x, i, ' ');
                }
            }
        }
    }
}
