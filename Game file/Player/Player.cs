using System;
using TeamWork.Background;
using TeamWork.Field;
using TeamWork.Objects;

namespace TeamWork
{
    public class Player : Entity, IPlayer
    {
        private int lives = 3;
        private int score = 0;
        private int level = 1;
        private int speed = 2;

        public static Point2D PlayerPoint = new Point2D(30, 53); // Player default starting point 

        /// <summary>
        /// Constructor with default values
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
        /// Player move up and redraw
        /// </summary>
        public void MoveUp()
        {
            // Limit player movement on Y axis
            if (this.Point.Y - speed < 3) return;
            Clear();
            this.Point.Y-= speed;
            Print();
        }

        /// <summary>
        /// Player move down and redraw
        /// </summary>
        public void MoveDown()
        {
            // Limit player movement on Y axis
            if (this.Point.Y + speed >= Engine.WindowHeight - 4) return;
            Clear();
            this.Point.Y+= speed;
            Print();
        }

        /// <summary>
        /// Player move right and redraw
        /// </summary>
        public void MoveRight()
        {
            // Limit player movement on X axis
            if (this.Point.X + speed >= Engine.WindowWidth) return;
            Clear();
            this.Point.X+= speed;
            Print();
        }

        /// <summary>
        /// Player move left and redraw
        /// </summary>
        public void MoveLeft()
        {
            // Limit player movement on X axis
            if (this.Point.X - speed < 1) return;
            Clear();
            this.Point.X-= speed;
            Print();
        }

        public void setName(string newName)
        {
            this.Name = newName;
        }

        //Method to print the player at its current position
        public void Print()
        {

            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"   █    ", ConsoleColor.Yellow);
            Printing.DrawAt(this.Point.X, this.Point.Y, @"  ███  ", ConsoleColor.Yellow);
            Printing.DrawAt(this.Point.X+3, this.Point.Y, @"█", ConsoleColor.Cyan);
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @" █████ ", ConsoleColor.Yellow);
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"█  █  █", ConsoleColor.Yellow);

        }

        // Method to clear players last positionư
        public void Clear()
        {
            //Had to use strings to get rid of artefacts

            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"       ");
            Printing.DrawAt(this.Point.X, this.Point.Y, @"       ");
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @"       ");

            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"       ");
        }

        /// <summary>
        /// Increase points by one and calculate level
        /// </summary>
        public void IncreasePoints()
        {
            this.Score++;
            Engine.Player.Level = Engine.Player.Score / 50 + 1;
            if (Engine.Player.Level > 1)
            {
                // Set players difficulty
                Engine.Chance = Engine.StartingDifficulty - Engine.Player.Level * 2;
            }
        }

        /// <summary>
        /// Increase points by given amount and calculate level
        /// </summary>
        /// <param name="points">Points to give</param>
        public void IncreasePoints(int points)
        {
            this.Score += points;
            Engine.Player.Level = Engine.Player.Score / 50 + 1;
        }
        /// <summary>
        /// Decrease lifes
        /// </summary>
        public void DecreaseLifes()
        {
            this.Lifes--;
        }

        /// <summary>
        /// Ship collision check with X and Y
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <returns>If there's a collision</returns>
        public bool ShipCollided(int x, int y)
        {
            // Checks a bunch of point of the player model
            if ((x <= Point.X + 21 && x >= Point.X + 3 && y == Point.Y) ||
                (x <= Point.X + 3 && x >= Point.X && y == Point.Y - 1) ||
                (x <= Point.X + 21 && x >= Point.X + 3 && y == Point.Y + 1))
            {
                // If theres a overlapping point x and y decrease lifes and redraw UI
                Engine.Player.DecreaseLifes();

                Menu.Table();
                Menu.UIDescription();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Ship collision check with Point2D
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool ShipCollided(Point2D p)
        {
            return ShipCollided(p.X, p.Y);
        }
    }
}