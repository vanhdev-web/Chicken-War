namespace TeamWork.Field
{
    public class Point2D
    {
        public Point2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Toán tử so sánh bằng để kiểm tra xem 2 điểm 2D có bằng nhau không
        /// </summary>
        /// <param name="point">Điểm 2D đầu tiên</param>
        /// <param name="point2">Điểm 2D thứ hai</param>
        /// <returns>Nếu các điểm bằng nhau</returns>
        public static bool operator ==(Point2D point, Point2D point2)
        {
            return point.X == point2.X && point.Y == point2.Y;
        }

        /// <summary>
        /// Toán tử so sánh khác để kiểm tra xem 2 điểm 2D có không bằng nhau
        /// </summary>
        /// <param name="point">Điểm 2D đầu tiên</param>
        /// <param name="point2">Điểm 2D thứ hai</param>
        /// <returns>Nếu các điểm không bằng nhau</returns>
        public static bool operator !=(Point2D point, Point2D point2)
        {
            return point.X != point2.X || point.Y != point2.Y;
        }

        /// <summary>
        /// Toán tử trừ giảm cả X và Y của điểm 2D đầu tiên theo X và Y của điểm 2D khác
        /// </summary>
        /// <param name="point">Điểm 2D đầu tiên</param>
        /// <param name="point2">Điểm 2D thứ hai</param>
        /// <returns>Kết quả Point2D</returns>
        public static Point2D operator -(Point2D point, Point2D point2)
        {
            int x = point.X - point2.X;
            int y = point.Y - point2.Y;
            return new Point2D(x, y);
        }

        /// <summary>
        /// Toán tử cộng tăng cả X và Y của điểm 2D đầu tiên theo X và Y của điểm 2D khác
        /// </summary>
        /// <param name="point">Điểm 2D đầu tiên</param>
        /// <param name="point2">Điểm 2D thứ hai</param>
        /// <returns>Kết quả Point2D</returns>
        public static Point2D operator +(Point2D point, Point2D point2)
        {
            int x = point.X + point2.X;
            int y = point.Y + point2.Y;
            return new Point2D(x, y);
        }

        /// <summary>
        /// Toán tử nhân nhân X và Y của Point2D với một số đã cho
        /// </summary>
        /// <param name="point">Point2D để tăng</param>
        /// <param name="multiplier">Số nhân nguyên</param>
        /// <returns>Kết quả point2D</returns>
        public static Point2D operator *(Point2D point, int multiplier)
        {
            int x = point.X * multiplier;
            int y = point.Y * multiplier;
            return new Point2D(x, y);
        }

        /// <summary>
        /// Toán tử nhân nhân X và Y của Point2D với một số đã cho
        /// </summary>
        /// <param name="multiplier">Số nhân nguyên</param>
        /// <param name="point">Point2D để tăng</param>
        /// <returns>Kết quả point2D</returns>
        public static Point2D operator *(int multiplier, Point2D point)
        {
            int x = point.X * multiplier;
            int y = point.Y * multiplier;
            return new Point2D(x, y);
        }

        /// <summary>
        /// Kiểm tra xem các đối tượng có bằng nhau không
        /// </summary>
        /// <param name="obj">Đối tượng để kiểm tra</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Point2D s = obj as Point2D;
            if (s == null)
            {
                return false;
            }
            return (X == s.X) && (Y == s.Y);
        }

        public override int GetHashCode()
        {
            return this.X ^ this.Y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
