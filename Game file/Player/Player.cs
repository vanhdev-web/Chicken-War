using System;
using TeamWork.Field;
using TeamWork.Objects;

namespace TeamWork
{
    public class Player : Entity, IPlayer
    {
        private int lives = 3;
        private int score = 0; 
        private int level = 1;

        public static Point2D PlayerPoint = new Point2D(10, 15); // Vị trí bắt đầu của người chơi 
        
        /// <summary>
        /// Xây dựng các giá trị mặc định của người chơi
        /// </summary>
        public Player()
        {
            this.Lifes = this.lives;
            this.Score = this.score;
            this.Level = this.level;

        }

        public int Score { get; set; }
        public int Lifes { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }


        /// <summary>
        /// Di chuyển người chơi đi lên bằng cách xóa và vẽ lại
        /// </summary>
        public void MoveUp()
        {
            // Giới hạn sự di chuyển của người chơi trên trục Y
            if (this.Point.Y - 1 < 3) return;
            Clear();
            this.Point.Y--;
            Print();
        }

        /// <summary>
        /// Di chuyển người chơi đi xuống bằng cách xóa và vẽ lại
        /// </summary>
        public void MoveDown()
        {
            // Giới hạn sự di chuyển của người chơi trên trục Y
            if (this.Point.Y + 1 >= Engine.WindowHeight - 4) return;
            Clear();
            this.Point.Y++;
            Print();
        }

        /// <summary>
        /// Di chuyển người chơi sang phải bằng cách xóa và vẽ lại
        /// </summary>
        public void MoveRight()
        {
            // Giới hạn sự di chuyển của người chơi trên trục X
            if (this.Point.X + 1 >= Engine.WindowWidth - 23) return;
            Clear();
            this.Point.X++;
            Print();
        }

        /// <summary>
        /// Di chuyển người chơi sang phải bằng cách xóa và vẽ lại
        /// </summary>
        public void MoveLeft()
        {
            // Giới hạn sự di chuyển của người chơi trên trục X
            if (this.Point.X - 1 < 1) return;
            Clear();
            this.Point.X--;
            Print();
        }

        public void setName(string newName)
        {
            this.Name = newName;
        }

        // Hàm in ra người chơi ở vị trí hiện tại
        public void Print()
        {
            Printing.DrawAt(Point.X, Point.Y - 1, @"____", ConsoleColor.Cyan);
            Printing.DrawAt(Point.X, Point.Y, @" \  \_____________", ConsoleColor.Cyan);
            Printing.DrawAt(Point.X, Point.Y + 1, @" <[=)_)_)_)_______)_ >", ConsoleColor.Cyan);
            Printing.DrawAt(Point.X + 20, Point.Y + 1, "=", ConsoleColor.DarkCyan);
        }

        // Hàm xóa người chơi ở vị trí cuối cùng
        public void Clear()
        {
            //Dùng khoảng trống để in đè 
            Printing.DrawAt(Point.X, Point.Y - 1, @"    ");
            Printing.DrawAt(Point.X, Point.Y, @"                  ");
            Printing.DrawAt(Point.X, Point.Y + 1, @"                      ");
        }

        /// <summary>
        /// Nâng điểm + 1 và tính level 
        /// </summary>
        public void IncreasePoints()
        {
            this.Score++;
            Engine.Player.Level = Engine.Player.Score/ 50 + 1;
            if (Engine.Player.Level > 1)
            {
                // Đặt độ khó của game
                Engine.Chance = Engine.StartingDifficulty - Engine.Player.Level * 2;
            }
        }

        /// <summary>
        /// Tăng 1 số điểm nhất định và tính level
        /// </summary>
        /// <param name="points">Số điểm được cho</param>
        public void IncreasePoints(int points)
        {
            this.Score += points;
            Engine.Player.Level = Engine.Player.Score / 50 + 1;
        }
        /// <summary>
        /// Giảm máu của người chơi
        /// </summary>
        public void DecreaseLifes()
        {
            this.Lifes--;
        }

        /// <summary>
        /// Kiểm tra va chạm với X và Y
        /// </summary>
        /// <param name="x">Số cột</param>
        /// <param name="y">Số dòng</param>
        /// <returns>Nếu có sự va chạm</returns>
        public bool ShipCollided(int x, int y)
        {
            // Kiểm tra vị trí của người chơi
            if ((x <= Point.X + 21 && x >= Point.X + 3 && y == Point.Y) ||
                (x <= Point.X + 3 && x >= Point.X && y == Point.Y-1) ||
                (x <= Point.X + 21 && x >= Point.X + 3 && y == Point.Y + 1))  
            {
                // Nếu có va chạm, giảm máu người chơi và vẽ lại UI
                Engine.Player.DecreaseLifes();

                Menu.Table();
                Menu.UIDescription();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra va chạm với Point2D
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool ShipCollided(Point2D p)
        {
            return ShipCollided(p.X, p.Y);
        }
    }
}