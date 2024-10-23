using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWork.Field;

namespace TeamWork.Background
{
    internal class BackgroundElements
    {
        public int x;
        public int y;
        Random random = new Random();
        public BackgroundElements()
        {
            x = random.Next(Engine.WindowWidth);
            y = 2;

        }

        public void Ve()
        {
            if (y < Engine.WindowHeight - 2)
            {
                Console.SetCursorPosition(x, y);
                Console.Write('█');
            }
            else
            {

                Console.ForegroundColor = ConsoleColor.Magenta;
                DiChuyen();
                x = random.Next(Engine.WindowWidth);
                y = 2;
                Console.SetCursorPosition(x, y);
                Console.Write('*');
                return;
            }
            


        }

        public void DiChuyen()
        {
            // Xóa bông tuyết cũ
            try
            {

                Console.SetCursorPosition(x, y - 1);
                Console.Write(' ');
            }
            catch { return; }

            // Di chuyển xuống dưới
            y++;
        }
    }

}

