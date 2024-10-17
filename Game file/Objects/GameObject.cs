using System;
using TeamWork.Field;

namespace TeamWork.Objects
{
    public class GameObject : Entity
    {
        /// <summary>
        /// Các quái vật trong Game
        /// </summary>
        public enum ObjectType
        {
            Bullet,
            /* Dùng cho viên đạn             
                    -
             */
            Normal,
            /* Thiên thạch chuẩn với 2 mạng sống, cho 2 điểm, không có chức năng bổ sung
                    /\
                    \/
             */
            Small,
            /* Thiên thạch nhỏ với 1 mạng sống, cho 1 điểm, không có chức năng bổ sung
                    <>
             */
            Silver,
            /* Thiên thạch bạc với 4 mạng sống, cho 5 điểm, không có chức năng bổ sung
                    \ /
                     x
                    / \
             */
            Gold,
            /* Thiên thạch vàng với 3 mạng sống, cho 4 điểm, không có chức năng bổ sung
                     ^
                    <x>
                     v
             */
            Lenghty,
            /* Thiên thạch dài với 3 mạng sống, cho 3 điểm, không có chức năng bổ sung
                    {==>
             */
            Quadcopter
            /* Chỉ loại kẻ thù hung hãn, bắn trả, có 7 mạng sống, cho 10 điểm
                   __       __
                  _\_\_____/_|
                <[__\_\_-----<"
                     oo' 
             */
        }

        private ObjectType objectType;
        public int life;

        /// <summary>
        /// Hàm khởi tạo cơ bản
        /// </summary>
        public GameObject()
        {
            base.Speed = 1;
        }


        /// <summary>
        /// Hàm khởi tạo với Point2D được chỉ định (loại viên đạn)
        /// </summary>
        /// <param name="point">Điểm để tạo đối tượng</param>
        public GameObject(Point2D point)
            : base(point)
        {
            base.Speed = 1;
            base.Point = point;
            objectType = ObjectType.Bullet;
        }


        /// <summary>
        /// Hàm khởi tạo với Point2D được chỉ định và loại nhất định
        /// </summary>
        /// <param name="point">Điểm để tạo đối tượng</param>
        /// <param name="type">Loại đối tượng</param>
        public GameObject(Point2D point, int type)
            : base(point)
        {
            base.Speed = 1;
            base.Point = point;
            objectType = (ObjectType)type;
        }

        /// <summary>
        /// Hàm khởi tạo với loại đối tượng, mọi thứ khác sử dụng mặc định cho loại đã cho
        /// </summary>
        /// <param name="type">Loại đối tượng</param>
        public GameObject(int type)
        {
            base.Speed = 1;
            objectType = (ObjectType)type;
            switch (objectType)
            {
                case ObjectType.Normal:
                    life = 2;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight - 3));
                    break;
                case ObjectType.Small:
                    life = 1;
                    base.Point = new Point2D(Engine.WindowWidth - 1, Engine.Rnd.Next(4, Engine.WindowHeight - 2));
                    break;
                case ObjectType.Silver:
                    life = 4;
                    base.Point = new Point2D(Engine.WindowWidth - 3, Engine.Rnd.Next(3, Engine.WindowHeight - 4));
                    break;
                case ObjectType.Gold:
                    life = 3;
                    base.Point = new Point2D(Engine.WindowWidth - 3, Engine.Rnd.Next(3, Engine.WindowHeight - 4));
                    break;
                case ObjectType.Lenghty:
                    life = 3;
                    base.Point = new Point2D(Engine.WindowWidth - 3, Engine.Rnd.Next(4, Engine.WindowHeight - 3));
                    break;
                case ObjectType.Quadcopter:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight - 4));
                    break;
            }
        }

        public bool toBeDeleted; // Kích hoạt để cho biết đối tượng này phải bị xóa
        private bool Moveable = true; // Trạng thái có thể di chuyển
        public override string ToString()
        {
            return string.Format("Loại đối tượng:{0}, X:{1}, Y:{2}, Có thể di chuyển:{3}", objectType, Point.X, Point.Y, Moveable);
        }


        private int Frames = 1; // Bộ đếm khung cho các hiệu ứng nổ và một số tính toán
        private Point2D diagonalInc = new Point2D(1, 1); // Điểm trợ giúp tính toán đường chéo
        private Point2D diagonalDec = new Point2D(-1, 1); // Điểm trợ giúp tính toán đường chéo
        private Point2D upRight; // Lưu trữ điểm đường chéo trên bên phải cho hiệu ứng nổ
        private Point2D upLeft; // Lưu trữ điểm đường chéo trên bên trái cho hiệu ứng nổ
        private Point2D downLeft; // Lưu trữ điểm đường chéo dưới bên trái cho hiệu ứng nổ
        private Point2D downRight; // Lưu trữ điểm đường chéo dưới bên phải cho hiệu ứng nổ

        private int projectileCounter = 1; // Bộ đếm để kiểm tra xem Quadcopter có nên bắn
        private int projectileChance = Engine.Rnd.Next(20, 50); // Cơ hội ngẫu nhiên rằng quadcopters sẽ bắn một viên đạn

        public bool GotHit = false; // Biến chuyển giúp với hoạt ảnh nổ
        /// <summary>
        /// In GameObject dựa trên loại của nó
        /// </summary>
        public void PrintObject()
        {
            switch (objectType)
            {
                case ObjectType.Bullet:
                    Printing.DrawAt(this.Point, '-', ConsoleColor.DarkCyan); // In tiêu chuẩn cho viên đạn
                    Printing.DrawAt(this.Point, '-', ConsoleColor.DarkCyan); // Standart print for bullets
                    break;
                case ObjectType.Normal:
                    if (!this.GotHit) // Nếu đối tượng này không bị giết bởi điều gì đó thì vẽ nó bình thường
                    {
                        Printing.DrawAt(this.Point, "/\\", ConsoleColor.Red);
                        Printing.DrawAt(this.Point.X, Point.Y + 1, "\\/", ConsoleColor.Red);
                    }
                    else // Nếu bị giết thì tạo hiệu ứng nổ
                    {
                        upLeft = this.Point - diagonalInc * Frames; // Tính toán vị trí hạt của hiệu ứng nổ
                        upRight = this.Point - diagonalDec * Frames; // Sử dụng các trợ giúp đường chéo
                        downRight = this.Point + diagonalInc * Frames; // Nhân với các khung trên màn hình
                        downLeft = this.Point + diagonalDec * Frames;
                        char[] c = { '/', '\\', '\\', '/' }; // Đặt các ký tự của vụ nổ
                        PrintAndClearExplosion(false, c, ConsoleColor.Red); // In nó
                    }

                    break;
                case ObjectType.Small:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point, "<>");
                    }
                    else
                    {
                        upRight = new Point2D(this.Point.X + Frames + 1, this.Point.Y);
                        upLeft = new Point2D(this.Point.X - Frames, this.Point.Y);
                        downRight = new Point2D(this.Point.X, this.Point.Y + Frames);
                        downLeft = new Point2D(this.Point.X, this.Point.Y - Frames);
                        char[] c = { '<', '>', '/', '\\' };
                        PrintAndClearExplosion(false, c, ConsoleColor.Gray);
                    }
                    break;
                case ObjectType.Silver:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X, this.Point.Y - 1, "\\ /", ConsoleColor.Gray);
                        Printing.DrawAt(this.Point, " X ", ConsoleColor.Gray);
                        Printing.DrawAt(this.Point.X, this.Point.Y + 1, "/ \\", ConsoleColor.Gray);
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        Printing.DrawAt(this.Point, 'x', ConsoleColor.Gray);
                        char[] c = { '\\', '/', '/', '\\' };
                        PrintAndClearExplosion(false, c, ConsoleColor.Gray);
                    }
                    break;
                case ObjectType.Gold:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X, this.Point.Y - 1, " \u25B2", ConsoleColor.Yellow);
                        Printing.DrawAt(this.Point, "\u25C4\u25A0\u25BA", ConsoleColor.Yellow);
                        Printing.DrawAt(this.Point.X, this.Point.Y + 1, " \u25BC", ConsoleColor.Yellow);
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        char[] c = { '\u25B2', '\u25BA', '\u25C4', '\u25BC' };
                        PrintAndClearExplosion(false, c, ConsoleColor.Yellow);
                    }
                    break;
                case ObjectType.Lenghty:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point, "{\u25A0\u25A0\u25BA");
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        char[] c = { '{', '\u25BA', '\u25A0', '\u25A0' };
                        PrintAndClearExplosion(false, c);
                    }
                    break;
                case ObjectType.Quadcopter:

                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Kiểm tra xem Quadcopter có phải bắn không
                        {
                            // Nếu đúng, tạo một viên đạn trong danh sách viên đạn chính
                            Engine._objectProjectiles.Add(new GameObject(new Point2D(this.Point.X - 1, this.Point.Y), 0));
                            projectileCounter++; // Tăng bộ đếm
                        }
                        else
                        {
                            // Nếu sai, chỉ tăng bộ đếm
                            projectileCounter++;
                        }

                        #region Hoạt ảnh vào của Quadcopter

                        // Điều này làm cho việc vào của quadcopter mượt mà, không xuất hiện ngay lập tức ở giữa màn hình
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point, @"         █ █", ConsoleColor.Red);
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"        █████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, @"       ██ █ ██", ConsoleColor.Red);
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"      █ █████ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"       ██ █ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, @"         █ █", ConsoleColor.Red);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 4, @"         █ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 3, @"        █████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 2, @"       ██ █ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"      █ █████ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, @"         █ █", ConsoleColor.Red);
                        }
                        #endregion
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(false);
                    }
                    break;
            }
        }


        /// <summary>
        /// Xóa GameObject dựa trên loại của nó
        /// </summary>
        public void ClearObject()
        {
            switch (objectType)
            {
                case ObjectType.Bullet:
                    Printing.DrawAt(this.Point.X, this.Point.Y, ' '); // Xóa viên đạn
                    break;
                case ObjectType.Normal:
                    #region Xóa đối tượng chuẩn và hiệu ứng vỡ
                    if (!this.GotHit) // Nếu không bị giết/hit thì xóa tiêu chuẩn
                    {
                        Printing.DrawAt(this.Point, "  ");
                        Printing.DrawAt(this.Point.X, Point.Y + 1, "  ");
                    }
                    else // Nếu bị hit, xóa sau hiệu ứng nổ
                    {
                        upRight = this.Point - diagonalDec * Frames;
                        upLeft = this.Point + diagonalDec * Frames;
                        downLeft = this.Point - diagonalInc * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        Moveable = false; // Đặt thiên thạch/asteroid thành tĩnh
                        PrintAndClearExplosion(true); // Xóa sau hiệu ứng
                        if (Frames == 5) // Kiểm tra nếu đã qua 4 khung thì...
                        {
                            this.toBeDeleted = true; // Đánh dấu đối tượng để xóa
                            Engine.Player.IncreasePoints(2); // Tăng điểm cho người chơi

                            Menu.Table(); // Vẽ lại bảng UI
                            Menu.UIDescription(); // Vẽ lại mô tả UI
                        }
                        Frames++; // Tăng số khung
                    }
                    #endregion
                    break;
                case ObjectType.Small:
                    #region Xóa đối tượng nhỏ và hiệu ứng vỡ
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point, "   ");
                    }
                    else
                    {
                        upRight = new Point2D(this.Point.X + Frames + 1, this.Point.Y);
                        upLeft = new Point2D(this.Point.X - Frames, this.Point.Y);
                        downRight = new Point2D(this.Point.X, this.Point.Y + Frames);
                        downLeft = new Point2D(this.Point.X, this.Point.Y - Frames);
                        Moveable = false;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            toBeDeleted = true;
                            Engine.Player.IncreasePoints(1);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Silver:
                    #region Xóa đối tượng bạc và hiệu ứng vỡ
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X, this.Point.Y - 1, "   ");
                        Printing.DrawAt(this.Point, "   ");
                        Printing.DrawAt(this.Point.X, this.Point.Y + 1, "   ");
                    }
                    else
                    {
                        upRight = this.Point - diagonalDec * Frames;
                        upLeft = this.Point + diagonalDec * Frames;
                        downRight = this.Point - diagonalInc * Frames;
                        downLeft = this.Point + diagonalInc * Frames;
                        Printing.ClearAtPosition(this.Point);
                        Moveable = false;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            this.toBeDeleted = true;
                            Engine.Player.IncreasePoints(5);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Gold:
                    #region Xóa đối tượng vàng và hiệu ứng vỡ
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X, this.Point.Y - 1, "  ");
                        Printing.DrawAt(this.Point, "   ");
                        Printing.DrawAt(this.Point.X, this.Point.Y + 1, "  ");
                    }
                    else
                    {
                        upRight = this.Point - diagonalDec * Frames;
                        upLeft = this.Point + diagonalDec * Frames;
                        downRight = this.Point - diagonalInc * Frames;
                        downLeft = this.Point + diagonalInc * Frames;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            this.toBeDeleted = true;
                            Engine.Player.IncreasePoints(4);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Lenghty:
                    #region Xóa đối tượng dài và hiệu ứng vỡ
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point, "    ");
                    }
                    else
                    {
                        upRight = this.Point - diagonalDec * Frames;
                        upLeft = this.Point + diagonalDec * Frames;
                        downLeft = this.Point - diagonalInc * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        Moveable = false;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            this.toBeDeleted = true;
                            Engine.Player.IncreasePoints(3);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
                case ObjectType.Quadcopter:
                    #region Xóa đối tượng quadcopter và hiệu ứng vỡ
                    if (!GotHit)
                    {
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point, @"            ");
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"             ");
                            Printing.DrawAt(this.Point, @"              ");
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"               ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"              ");
                            Printing.DrawAt(this.Point, @"            ");
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 4, @"            ");
                            Printing.DrawAt(this.Point.X, Point.Y - 3, @"             ");
                            Printing.DrawAt(this.Point.X, Point.Y - 2, @"              ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"               ");
                            Printing.DrawAt(this.Point.X, Point.Y, @"            ");
                        }
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
                        PrintAndClearExplosion(true);
                        if (Frames == 5)
                        {
                            this.toBeDeleted = true;
                            Engine.Player.IncreasePoints(10);

                            Menu.Table();
                            Menu.UIDescription();
                        }
                        Frames++;
                    }
                    #endregion
                    break;
            }
        }

        /// <summary>
        /// Di chuyển đối tượng sang trái 1 ô
        /// </summary>
        public void MoveObject()
        {
            if (Moveable)
            {
                this.Point.X -= Speed;
            }
        }

        /// <summary>
        /// Xóa và in hiệu ứng nổ
        /// </summary>
        /// <param name="clear">Đặt thành true nếu bạn muốn xóa, false nếu bạn muốn in</param>
        /// <param name="c">Đặt các ký tự bạn muốn in cùng</param>
        /// <param name="clr">Đặt màu bạn muốn in cùng</param>
        /// <param name="clear">Set to true if you want to clear, false if you want to print</param>
        /// <param name="c">Set the characters you want to print with</param>
        /// <param name="clr">Set the color you want to print with</param>
        public void PrintAndClearExplosion(bool clear, char[] c = null, ConsoleColor clr = ConsoleColor.White)
        {
            if (c == null && !clear) // Nếu không có char[] nào được truyền và yêu cầu in, tạo một cái tiêu chuẩn

            if (c == null && !clear) // If theres no passed char[] and printing is ordered create standard one
            {
                c = new[] { '*', '*', '*', '*' };
            }
            else if (clear) // Nếu xóa, tạo char[] với các khoảng trắng
            {
                c = new[] { ' ', ' ', ' ', ' ' };
            }
            // Sau đó in/xóa các đường chéo
            if ((upLeft.X > 1 && upLeft.X < 79) && (upLeft.Y > 1 && upLeft.Y < 30))
            {
                Printing.DrawAt(upLeft, c[0], clr);
            }
            if ((upRight.X > 1 && upRight.X < 79) && (upRight.Y > 1 && upRight.Y < 30))
            {
                Printing.DrawAt(upRight, c[1], clr);
            }
            if ((downLeft.X > 1 && downLeft.X < 79) && (downLeft.Y > 1 && downLeft.Y < 30))
            {
                Printing.DrawAt(downLeft, c[2], clr);
            }
            if ((downRight.X > 1 && downRight.X < 79) && (downRight.Y > 1 && downRight.Y < 30))
            {
                Printing.DrawAt(downRight, c[3], clr);
            }
        }



        /// <summary>
        /// kiểm tra va chạm
        /// </summary>
        /// <param name="x">Kiểm tra với X</param>
        /// <param name="y">Kiểm tra với Y</param>
        /// <returns>Với có va chạm</returns>
        public bool Collided(int x, int y)
            /// <summary>
            /// Collision check
            /// </summary>
            /// <param name="x">X to check with</param>
            /// <param name="y">Y to check with</param>
            /// <returns>If there is a collision</returns>
            public bool Collided(int x, int y)
        {
            if (GotHit)
            {
                return false;
            }
            switch (objectType)
            {
                case ObjectType.Normal:
                    /*
                     * (.)AB
                     * (.)CD
                     */
                    if ((x == this.Point.X && (y == this.Point.Y || y == this.Point.Y + 1)) || // A / C
                        (x == this.Point.X + 1 &&
                        (y == this.Point.Y || y == this.Point.Y + 1)) || // B / D
                        (x == this.Point.X - 1 &&
                        (y == this.Point.Y || y == this.Point.Y + 1)))// ..
                        return true;
                    return false;
                case ObjectType.Small:
                    /*
                     * (.)AB              
                     */
                    if ((x == this.Point.X && y == this.Point.Y) || // A
                        (x == this.Point.X + 1 && y == this.Point.Y) || // B
                        (x == this.Point.X - 1 && y == this.Point.Y)) // .
                        return true;
                    return false;
                case ObjectType.Silver:
                    /*  
                     * CFI
                     * ADG
                     * BEH
                     */
                    if ((x == this.Point.X && y == this.Point.Y) || //A
                        (x == this.Point.X &&
                        (y == this.Point.Y + 1 || y == this.Point.Y - 1)) || // B / C
                        (x == this.Point.X + 1 &&
                        (y == this.Point.Y || y == this.Point.Y + 1 || y == this.Point.Y - 1)) || // D / E / F
                        (x == this.Point.X + 2 &&
                        (y == this.Point.Y || y == this.Point.Y + 1 || y == this.Point.Y - 1))) // G / H / I
                        return true;
                    return false;
                case ObjectType.Gold:
                    /*  
                     * CF(.)
                     * ADG
                     * BE(.)
                     */
                    if ((x == this.Point.X &&
                         (y == this.Point.Y || y == this.Point.Y + 1 || y == this.Point.Y - 1)) || // A / B / C
                        (x == this.Point.X + 1 &&
                         (y == this.Point.Y || y == this.Point.Y + 1 || y == this.Point.Y - 1)) || // D / E / F
                        (x == this.Point.X + 2 &&
                         (y == this.Point.Y || y == this.Point.Y + 1 || y == this.Point.Y - 1))) // G / . / .
                    {
                        return true;
                    }
                    else 
                    { 
                        return false;
                    }
                    ;
                case ObjectType.Lenghty:
                    /*
                     * ABCD
                     */
                    if ((x == this.Point.X && y == this.Point.Y) || // A
                        (y == this.Point.Y &&
                        (x == this.Point.X || x == this.Point.X + 1 || // B / C / D
                        x == this.Point.X + 2 || x == this.Point.X + 3)))
                        return true;
                    return false;
                case ObjectType.Quadcopter:
                   if (
                        (x == this.Point.X && y == this.Point.Y) 
                        ||
                        (y == this.Point.Y && (x == this.Point.X || x == this.Point.X + 1 || x == this.Point.X + 2 || x == this.Point.X + 3))
                        ||
                        (y == this.Point.Y + 1 && (x == this.Point.X || x == this.Point.X + 1 || x == this.Point.X + 2 || x == this.Point.X + 3))
                        ||
                        (y == this.Point.Y - 1 && (x == this.Point.X || x == this.Point.X + 1 || x == this.Point.X + 2 || x == this.Point.X + 3))
                        ||
                        (y == this.Point.Y - 2 && (x == this.Point.X+3 || x == this.Point.X + 4 || x == this.Point.X + 5))
                       )
                        return true;
                    return false;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Kiểm tra va chạm với Point2D
        /// </summary>
        /// <param name="point">Sử dụng Point2D để kiểm tra</param>
        /// <returns>Nếu có va chạm</returns>
        public bool Collided(Point2D point)
        {
            return Collided(point.X, point.Y);
        }


    }
}
