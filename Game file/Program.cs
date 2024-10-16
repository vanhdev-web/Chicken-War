using System;
using System.Text;
using TeamWork.Field;
using System.Runtime.InteropServices;

namespace TeamWork
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Console.WindowWidth = Engine.WindowWidth;
            Console.BufferWidth = Engine.WindowWidth;
            Console.WindowHeight = Engine.WindowHeight;
            Console.BufferHeight = Engine.WindowHeight;
            Engine eng = new Engine();


        }
    }
}
