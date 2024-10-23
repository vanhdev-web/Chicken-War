using TeamWork.Field;

namespace TeamWork.Objects
{
    abstract public class Entity
    {
        private Point2D point = new Point2D(55, 25);
        private int speed;
        private bool down; // Added a new boolean field

        protected Entity()
        {
            this.Point = point;
            this.Speed = this.speed;
            this.down = false; // Initialize the bool field
        }

        protected Entity(Point2D point)
        {
            this.Point = point;
            this.Speed = this.speed;
            this.down = false; // Initialize the bool field
        }

        public Point2D Point
        {
            get { return this.point; }
            set { this.point = value; }
        }

        public int Speed { get; set; }

        // Property to access the down field
        public bool Down
        {
            get { return this.down; }
            set { this.down = value; }
        }
    }
}
