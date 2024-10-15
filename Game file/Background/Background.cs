using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeamWork.Field;

namespace TeamWork.Background
{
    internal class Background
    {
        
       public static void BackGround()
        {
            Console.CursorVisible = false;
            Random random = new Random();
            List<BackgroundElements> danhSachBongTuyet = new List<BackgroundElements>();


            //while (true)
            //{
                

                // Tạo bông tuyết mới với xác suất nhất định

                if (random.Next(10) == 0)
                {
                    danhSachBongTuyet.Add(new BackgroundElements());


                }



                int count = 0;
                // Vẽ tất cả các bông tuyết
                foreach (BackgroundElements bongTuyet in danhSachBongTuyet)
                {
                    if (count == 20)
                    {
                        break;
                    }
                    bongTuyet.Ve();
                    bongTuyet.DiChuyen();
                    count++;
                }

                //Thread.Sleep(30);
            //}

        }
    }
    
}
