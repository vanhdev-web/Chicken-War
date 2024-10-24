using System;
using System.Collections.Generic;
using TeamWork.Field;
using System.Threading;
using TeamWork.Background;
using System.Linq;

namespace TeamWork.Objects
{
    public class GameObject : Entity
    {
        /// <summary>
        /// GameObject Type - each with different looks, life, collision detection, points, effects.
        /// </summary>
        public enum ObjectType
        {
            Bullet,
            /* Used for bullets only             
                    -
             */
            Silver,
            /* Silver Meteorid with 4 life, gives 5 points, no additional functions
                    \ /
                     x
                    / \
             */
            Gold,
            /* Gold Meteorid with 3 life, gives 4 points, no additional functions
                     ^
                    <x>
                     v
             */
            Lenghty,
            /* Lenghty Meteorid with 3 life, gives 3 points, no additional functions
                    {==>
             */
            Quadcopter,
            /* Only agressive enemy type, shoots back, has 7 life, gives 10 points
                   __       __
                  _\_\_____/_|
                <[__\_\_-----<"
                     oo' 
             */
            Meteor1,
            Meteor2,
            Meteor3
        }

        public ObjectType objectType;
        public int life;

        /// <summary>
        /// Base constructor
        /// </summary>
        public GameObject()
        {
            base.Speed = 1;
          /*  lastFireTime = DateTime.Now; // Initialize the last fire time
            IsCharging = false; // Initialize as not charging*/
        }


        /// <summary>
        /// Constructor with assigned Point2D (type bullet)
        /// </summary>
        /// <param name="point">Point to create the object with</param>
        public GameObject(Point2D point)
            : base(point)
        {
            base.Speed = 1;
            base.Point = point;
            objectType = ObjectType.Bullet;
            base.Down = false;
        }


        /// <summary>
        /// Constructor with assigned Point2D with given type
        /// </summary>
        /// <param name="point">Point to create the object with</param>
        /// <param name="type">Object type</param>
        public GameObject(Point2D point, int type)
            : base(point)
        {
            base.Speed = 1;
            base.Point = point;
            objectType = (ObjectType)type;
        }

        /// <summary>
        /// Constructor with object type, everithing else is using the defaults for the given type
        /// </summary>
        /// <param name="type">Object type</param>
        public GameObject(int type)
        {
            base.Speed = 1;
            objectType = (ObjectType)type;

            switch (objectType)
            {
                case ObjectType.Silver:
                    life = 7;
                    base.Point = new Point2D(Engine.Rnd.Next(Engine.WindowWidth - 4), 5);
                    this.Down = true;
                    break;
                case ObjectType.Gold:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight / 2 - 4));
                    this.Down = false;
                    break;
                case ObjectType.Lenghty:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight / 2 - 4));
                    this.Down = false;
                    break;
                case ObjectType.Quadcopter:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight / 2 - 4));
                    this.Down = false;
                    break;
                case ObjectType.Meteor1:
                case ObjectType.Meteor2:
                case ObjectType.Meteor3:
                    life = 5;
                    if (Engine.Rnd.Next(2) == 0)
                    {
                        base.Point = new Point2D(Engine.Rnd.Next(Engine.WindowWidth - 4), 5);
                        this.Down = true;
                    }
                    else
                    {
                        base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(Engine.WindowHeight / 2, Engine.WindowHeight - 4));
                        this.Down = false;
                    }
                    break;

            }
        }

        public bool toBeDeleted; // Trigger to tell this object must be deleted
        private bool Moveable = true; // Toggable move state
        public override string ToString()
        {
            return string.Format("Object type:{0}, X:{1}, Y:{2},Moveable:{3}", objectType, Point.X, Point.Y, Moveable);
        }


        private int Frames = 1; // Frame counter for explosions and some calculations
        private Point2D diagonalInc = new Point2D(1, 1); // Diagonal calculation helper Point2D
        private Point2D diagonalDec = new Point2D(-1, 1); // Diagonal calculation helper Point2D
        private Point2D upRight; // up Right Diagonal point storage for explosion effect
        private Point2D upLeft; // up Left Diagonal point storage for explosion effect
        private Point2D downLeft; // down Left Diagonal point storage for explosion effect
        private Point2D downRight; // down Right Diagonal point storage for explosion effect

        private int projectileCounter = 1; // Counter to check with if the Quadcopter should fire 
        private int projectileChance = Engine.Rnd.Next(20, 50); // Random chance that quadcopters will fire a bullet

        public bool GotHit = false; //Toggle that helps with the explosion animation
        /// <summary>
        /// Print GameObject based on its type
        /// </summary>
        public void PrintObject()
        {
            switch (objectType)
            {
                case ObjectType.Bullet:
                    if (this.Point.Y > Engine.WindowHeight - 2)
                    {
                        return;
                    }
                    else
                    {

                        Printing.DrawAt(this.Point, '█', ConsoleColor.Yellow); // Standart print for bullets
                        break;
                    }
                case ObjectType.Silver:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if Quadcopter has to shoot
                        {
                            // If true, create a projectile in the main list of projectiles
                            Engine._objectProjectiles.Add(new GameObject(new Point2D(this.Point.X, this.Point.Y - 1), 0));
                            projectileCounter++; // Increase the counter
                        }
                        else
                        {
                            // If false, just increase the counter
                            projectileCounter++;
                        }
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, @"  █  █", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @" ███████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y, @"  █ █ █", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"  █████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"  █ █ █", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y, @"   █ █", ConsoleColor.Magenta);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 4, @" █     █", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X, Point.Y - 3, @" ███████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 2, @"  █ █ █", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"  █████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y, @"   █ █", ConsoleColor.Magenta);
                        }
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
                case ObjectType.Gold:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if Quadcopter has to shoot
                        {
                            // If true, create a projectile in the main list of projectiles
                            Engine._objectProjectiles.Add(new GameObject(new Point2D(this.Point.X - 1, this.Point.Y), 0));
                            projectileCounter++; // Increase the counter
                        }
                        else
                        {
                            // If false, just increase the counter
                            projectileCounter++;
                        }
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " █", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ █", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█ ", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ ██", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█  ", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ ██ ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█   ", ConsoleColor.Yellow);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, " ██ ██ ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███████", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "█   ", ConsoleColor.Yellow);
                        }
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
                case ObjectType.Lenghty:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if Quadcopter has to shoot
                        {
                            // If true, create a projectile in the main list of projectiles
                            Engine._objectProjectiles.Add(new GameObject(new Point2D(this.Point.X - 1, this.Point.Y), 0));
                            projectileCounter++; // Increase the counter
                        }
                        else
                        {
                            // If false, just increase the counter
                            projectileCounter++;
                        }

                        #region Quadcopter Entry animation
                        /* Only agressive enemy type, shoots back, has 7 life, gives 10 points
                   __       __
                  _\_\_____/_|
                <[__\_\_-----<"
                     oo' 
             */

                        // This makes the quadcopter entry smooth, not instant spawn in the center of the screen
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " █", ConsoleColor.Magenta);
                        }
                        if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " ██", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " ███", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█", ConsoleColor.Red);
                        }
                        if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█ █", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ███", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " ████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█ ", ConsoleColor.Red);
                        }
                        if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█ █ ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ███ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " █████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█  ", ConsoleColor.Red);
                        }
                        if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█ █  ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ███ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " █████ ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█   ", ConsoleColor.Red);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "█ █  ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█ ███ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "██ █ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, " █████ ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "█   ", ConsoleColor.Red);
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
                case ObjectType.Quadcopter:


                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if Quadcopter has to shoot
                        {
                            // If true, create a projectile in the main list of projectiles
                            Engine._objectProjectiles.Add(new GameObject(new Point2D(this.Point.X - 1, this.Point.Y), 0));
                            projectileCounter++; // Increase the counter
                        }
                        else
                        {
                            // If false, just increase the counter
                            projectileCounter++;
                        }

                        #region Quadcopter Entry animation

                        // This makes the quadcopter entry smooth, not instant spawn in the center of the screen
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, "█", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "███", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█ ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ██", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█ ", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█ █", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "█████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ██ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "█████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█ █", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█ █ ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "██████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ███ ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "██████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█ █ ", ConsoleColor.DarkMagenta);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "█ █  ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "███████", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "█ ███ █", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "███████", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "█ █ █", ConsoleColor.DarkMagenta);
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
                case ObjectType.Meteor1:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X, Point.Y - 1, @"███", ConsoleColor.DarkRed);
                        Printing.DrawAt(this.Point.X, Point.Y, @"████", ConsoleColor.Red);
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
                case ObjectType.Meteor2:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 1, @"███", ConsoleColor.DarkMagenta);
                        Printing.DrawAt(this.Point.X, Point.Y, @"████", ConsoleColor.Magenta);
                        Printing.DrawAt(this.Point.X, Point.Y + 1, @"██", ConsoleColor.DarkMagenta);
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
                case ObjectType.Meteor3:
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 1, @"██", ConsoleColor.DarkGray);
                        Printing.DrawAt(this.Point.X, Point.Y, @"████", ConsoleColor.Gray);
                        Printing.DrawAt(this.Point.X, Point.Y + 1, @"███", ConsoleColor.DarkGray);
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
        /// Clear GameObject based on its type
        /// </summary>
        public void ClearObject()
        {
            switch (objectType)
            {
                case ObjectType.Bullet:
                    Printing.DrawAt(this.Point.X, this.Point.Y, ' ');
                    break;
                case ObjectType.Silver:
                    #region Silver object clearing and breaking effect
                    if (!this.GotHit)
                    {
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, @"      ");
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"        ");
                            Printing.DrawAt(this.Point.X, Point.Y, @"       ");
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"       ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"       ");
                            Printing.DrawAt(this.Point.X, Point.Y, @"      ");
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 4, @"        ");
                            Printing.DrawAt(this.Point.X, Point.Y - 3, @"        ");
                            Printing.DrawAt(this.Point.X, Point.Y - 2, @"       ");
                            Printing.DrawAt(this.Point.X, Point.Y - 1, @"       ");
                            Printing.DrawAt(this.Point.X, Point.Y, @"      ");
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
                case ObjectType.Gold:
                    #region Gold object clearing and breaking effect math
                    if (!this.GotHit)
                    {
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "  ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "  ", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "   ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "   ", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "    ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "    ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, " ", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "     ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "     ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "  ", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "      ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "      ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "   ", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "       ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "    ", ConsoleColor.Yellow);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X, Point.Y, "       ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 1, "    ", ConsoleColor.Yellow);
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
                case ObjectType.Lenghty:
                    #region Lenghty object clear and breaking effect math
                    if (!GotHit)
                    {

                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "  ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "  ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "  ", ConsoleColor.Magenta);
                        }
                        if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "   ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "   ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "   ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, " ", ConsoleColor.Yellow);
                        }
                        if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, " ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "    ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "    ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "    ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, " ", ConsoleColor.Red);
                        }
                        if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "   ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "     ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "     ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "     ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "  ", ConsoleColor.Red);
                        }
                        if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "    ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "      ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "      ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "      ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "   ", ConsoleColor.Red);
                        }
                        if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "     ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "       ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "       ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "    ", ConsoleColor.Red);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y + 2, "     ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y, "       ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "       ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 3, Point.Y - 2, "    ", ConsoleColor.Red);
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
                case ObjectType.Quadcopter:
                    #region Quadcopter object clear and breaking effect
                    if (!GotHit)
                    {
                        if (this.Point.X + 2 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "  ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "  ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "   ", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 3 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 1, Point.Y - 2, " ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "  ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "  ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "  ", ConsoleColor.Magenta);
                        }
                        else if (this.Point.X + 4 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, " ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "   ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "   ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "   ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, " ", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 5 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "  ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "    ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "    ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "    ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "  ", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 6 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "   ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "     ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "     ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "     ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "   ", ConsoleColor.DarkMagenta);
                        }
                        else if (this.Point.X + 7 >= Engine.WindowWidth)
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "    ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "      ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "      ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "      ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "    ", ConsoleColor.DarkMagenta);
                        }
                        else
                        {
                            Printing.DrawAt(this.Point.X + 2, Point.Y - 2, "     ", ConsoleColor.Yellow);
                            Printing.DrawAt(this.Point.X, Point.Y - 1, "       ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point, "       ", ConsoleColor.Red);
                            Printing.DrawAt(this.Point.X, Point.Y + 1, "       ", ConsoleColor.Magenta);
                            Printing.DrawAt(this.Point.X + 1, Point.Y + 2, "     ", ConsoleColor.DarkMagenta);
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
                case ObjectType.Meteor1:
                    #region Small object clearing and breaking effect
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X, Point.Y - 1, @"   ");
                        Printing.DrawAt(this.Point.X, Point.Y, @"    ");
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
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
                case ObjectType.Meteor2:
                    #region Small object clearing and breaking effect
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 1, @"   ");
                        Printing.DrawAt(this.Point.X, Point.Y, @"    ");
                        Printing.DrawAt(this.Point.X, Point.Y + 1, @"  ");
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
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
                case ObjectType.Meteor3:
                    #region Small object clearing and breaking effect
                    if (!this.GotHit)
                    {
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 1, @"  ");
                        Printing.DrawAt(this.Point.X, Point.Y, @"    ");
                        Printing.DrawAt(this.Point.X, Point.Y + 1, @"   ");
                    }
                    else
                    {
                        upLeft = this.Point - diagonalInc * Frames;
                        upRight = this.Point - diagonalDec * Frames;
                        downRight = this.Point + diagonalInc * Frames;
                        downLeft = this.Point + diagonalDec * Frames;
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

            }
        }

        /// <summary>
        /// Move the object to the left 1 tile
        /// </summary>
        public void Shooting()
        {
            while (true)
            {

            }
        }
        public void MoveObject(bool down)
        {
            if (Moveable && down)
            {
                this.Point.Y += Speed;
                return;
            }
            this.Point.X -= Speed;
        }


        /// <summary>
        /// Clear and Print explosion effect
        /// </summary>
        /// <param name="clear">Set to true if you want to clear, false if you want to print</param>
        /// <param name="c">Set the characters you want to print with</param>
        /// <param name="clr">Set the color you want to print with</param>
        private void PrintAndClearExplosion(bool clear)
        {
            char[] c = new char[4];
            var colors = Enum.GetValues(typeof(ConsoleColor))
                 .Cast<ConsoleColor>()
                 .Where(color => color != ConsoleColor.White &&
                             color != ConsoleColor.Gray &&
                             color != ConsoleColor.DarkGray)
                 .ToList();

            // Create a Random object to pick random colors.
            Random random = new Random();

            if (!clear) // If theres no passed char[] and printing is ordered create standard one
            {
                c = new[] { '█', '█', '█', '█' };
            }
            else if (clear) // If clearing, create char[] with white spaces
            {
                c = new[] { ' ', ' ', ' ', ' ' };
            }
            // Then print/Clear the diagonals
            if ((upLeft.X > 1 && upLeft.X < 114) && (upLeft.Y > 1 && upLeft.Y < 30))
            {
                Printing.DrawAt(upLeft, c[0], colors[random.Next(colors.Count)]);
            }
            if ((upRight.X > 1 && upRight.X < 114) && (upRight.Y > 1 && upRight.Y < 30))
            {
                Printing.DrawAt(upRight, c[1], colors[random.Next(colors.Count)]);
            }
            if ((downLeft.X > 1 && downLeft.X < 114) && (downLeft.Y > 1 && downLeft.Y < 30))
            {
                Printing.DrawAt(downLeft, c[2], colors[random.Next(colors.Count)]);
            }
            if ((downRight.X > 1 && downRight.X < 114) && (downRight.Y > 1 && downRight.Y < 30))
            {
                Printing.DrawAt(downRight, c[3], colors[random.Next(colors.Count)]);
            }

        }
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
                case ObjectType.Silver:
                    /*  
                     * CFI
                     * ADG
                     * BEH
                     */
                    if ((x == this.Point.X + 1 && y == this.Point.Y - 4) ||  // Left upper dot
         (x == this.Point.X + 7 && y == this.Point.Y - 4) ||  // Right upper dot
         (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y - 3) ||  // Full row (Point.Y - 3)
         (x == this.Point.X + 2 && y == this.Point.Y - 2) ||  // Middle left dot
         (x == this.Point.X + 4 && y == this.Point.Y - 2) ||  // Center dot
         (x == this.Point.X + 6 && y == this.Point.Y - 2) ||  // Middle right dot
         (x >= this.Point.X + 1 && x <= this.Point.X + 5 && y == this.Point.Y - 1) ||  // Full row (Point.Y - 1)
         (x == this.Point.X + 3 && (y == this.Point.Y || y == this.Point.Y)))  // Bottom dots

                        return true;
                    return false;
                case ObjectType.Gold:
                    /*  
                     * CF(.)
                     * ADG
                     * BE(.)
                     */
                    if ((x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y + 1) ||  // Full row (Point.Y + 1)
                  (x == this.Point.X + 1 && y == this.Point.Y) ||  // Left dot
                  (x == this.Point.X + 4 && y == this.Point.Y) ||  // Right dot
                  (x >= this.Point.X + 3 && x <= this.Point.X + 5 && y == this.Point.Y - 1))  // Upper right side
                        return true;
                    return false;
                case ObjectType.Lenghty:
                    /*
                     * ABCD
                     */
                    if ((x == this.Point.X + 3 && y == this.Point.Y - 2) ||  // Top dot
              (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y - 1) ||  // Full row (Point.Y - 1)
              (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y) ||  // Full row (Point.Y)
              (x == this.Point.X && y == this.Point.Y + 1) ||  // Left dot
              (x >= this.Point.X + 2 && x <= this.Point.X + 4 && y == this.Point.Y + 1) ||  // Center row
              (x == this.Point.X + 6 && y == this.Point.Y + 1) ||  // Right dot
              (x == this.Point.X + 2 && y == this.Point.Y + 2) ||  // Bottom left dot
              (x == this.Point.X + 4 && y == this.Point.Y + 2))  // Bottom right dot
                        return true;
                    return false;
                case ObjectType.Quadcopter:
                    if ((x == this.Point.X + 2 && y == this.Point.Y - 2) ||  // Top dot
        (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y - 1) ||  // Full row (Point.Y - 1)
        (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y) ||  // Full row (Point.Y)
        (x >= this.Point.X && x <= this.Point.X + 6 && y == this.Point.Y + 1) ||  // Full row (Point.Y + 1)
        (x == this.Point.X + 1 && y == this.Point.Y + 2) ||  // Bottom left dot
        (x == this.Point.X + 3 && y == this.Point.Y + 2) ||  // Bottom center dot
        (x == this.Point.X + 5 && y == this.Point.Y + 2))  // Bottom right dot
                        return true;
                    return false;
                default:
                    return false;
                case ObjectType.Meteor1:
                    // Shape:
                    //  (.)ABC
                    //  ....DEFGH
                    if ((x == this.Point.X && (y == this.Point.Y || y == this.Point.Y - 1)) ||  // A
                        (x == this.Point.X + 1 && (y == this.Point.Y || y == this.Point.Y - 1)) ||  // B
                        (x == this.Point.X + 2 && y == this.Point.Y - 1) ||  // C
                        (x >= this.Point.X && x <= this.Point.X + 3 && y == this.Point.Y))  // D, E, F, G, H
                    {
                        return true;
                    }
                    return false;

                case ObjectType.Meteor2:
                    // Shape:
                    //   ..A
                    //   BCDEF
                    //   .G.
                    if ((x == this.Point.X + 1 && y == this.Point.Y - 1) ||  // A
                        (x >= this.Point.X && x <= this.Point.X + 3 && y == this.Point.Y) ||  // B, C, D, E, F
                        (x == this.Point.X && y == this.Point.Y + 1) ||  // G
                        (x == this.Point.X + 1 && y == this.Point.Y + 1))  // .
                    {
                        return true;
                    }
                    return false;

                case ObjectType.Meteor3:
                    // Shape:
                    //  ..A
                    //  BCDEF
                    //  .G.
                    if ((x == this.Point.X + 1 && y == this.Point.Y - 1) ||  // A
                        (x >= this.Point.X && x <= this.Point.X + 3 && y == this.Point.Y) ||  // B, C, D, E, F
                        (x == this.Point.X + 1 && y == this.Point.Y + 1))  // G
                    {
                        return true;
                    }
                    return false;

            }
        }
        /// <summary>
        /// Collision check with Point2D
        /// </summary>
        /// <param name="point">Point2D To check with</param>
        /// <returns>If there is a collision</returns>
        public bool Collided(Point2D point)
        {
            return Collided(point.X, point.Y);
        }
        Random random = new Random();
        List<BackgroundElements> danhSachBongTuyet = new List<BackgroundElements>();
        public void BGs()
        {

            if (random.Next(5) == 0)
            {
                danhSachBongTuyet.Add(new BackgroundElements());


            }
            int count = 0;
            // Vẽ tất cả các bông tuyết
            foreach (BackgroundElements bongTuyet in danhSachBongTuyet)
            {
                if (count == 5)
                {
                    break;
                }
                bongTuyet.Ve();
                bongTuyet.DiChuyen();
                count++;
            }
        }
    /*    public Point2D Point { get; set; }
        private List<Laser> lasers = new List<Laser>();
        private const int MoveSpeed = 2; // Speed of the object's 
        private const int FireDelay = 1000; // 1 second delay
        private DateTime lastFireTime;
        public int chargingDelayTime = 1000; // Thời gian ngưng trước khi charge (2 giây)
        private bool isDelayBeforeCharge = false; // Cờ để kiểm tra xem đã dừng trước khi charge chưa
        private DateTime delayStartTime; // Để lưu thời gian bắt đầu dừng
        public bool IsCharging { get; private set; } // New property to track charging state
        private int direction = 1; // 1 for right, -1 for left


        public void ChargeAndShoot()
        {
            if (!IsCharging && !isDelayBeforeCharge && random.Next(0, 100) < 10)
            {
                isDelayBeforeCharge = true;
                delayStartTime = DateTime.Now;
            }

            if (isDelayBeforeCharge)
            {
                if ((DateTime.Now - delayStartTime).TotalMilliseconds >= chargingDelayTime)
                {
                    Laser newLaser = new Laser { Position = new Point2D( this.Point.X + 3,this.Point.Y), LifeOnScreen = 10 }; // Start below character
                    lasers.Add(newLaser);
                    lastFireTime = DateTime.Now;
                    IsCharging = true;
                    isDelayBeforeCharge = false;
                }
            }
        }

        public void UpdateLasers()
        {
            for (int i = lasers.Count - 1; i >= 0; i--)
            {
                Laser laser = lasers[i];

                if (laser.LifeOnScreen > 8) // Charge-up effect
                {
                    // Vẽ chỉ số charge-up ngay dưới giữa nhân vật

                    Printing.DrawAtBG(this.Point.X + 2, this.Point.Y + 2, "|", ConsoleColor.DarkGray);
                    Printing.DrawAtBG(this.Point.X + 3, this.Point.Y + 2, "|", ConsoleColor.Gray);
                    Printing.DrawAtBG(this.Point.X + 4, this.Point.Y + 2, "|", ConsoleColor.DarkGray);
                }
                else
                {
                    // Draw laser beam
                    for (int j = 0; j <= 50; j++)
                    {
                        if (this.Point.Y + j + 2 < Engine.WindowHeight && (this.Point.Y + 1 + j) > laser.Position.Y) // Ensure it's below the character
                        {
                            Printing.DrawAtBG(this.Point.X + 2, this.Point.Y + j + 2, "|", ConsoleColor.DarkGray);
                            Printing.DrawAtBG(this.Point.X + 3, this.Point.Y + j + 2, "|", ConsoleColor.Gray);
                            Printing.DrawAtBG(this.Point.X + 4, this.Point.Y + j + 2, "|", ConsoleColor.DarkGray);
                        }
                    }
                    lasers.RemoveAt(i);
                    IsCharging = false;
                }

                laser.LifeOnScreen--;
            }
        }
        public void ClearLasers()
        {
            foreach (var laser in lasers)
            {
                for (int i = -50; i <= 50; i++)
                {
                    if (laser.Position.Y + i > 0 && laser.Position.Y + i < Engine.WindowHeight) // Ensure it's within screen bounds
                    {
                        // Clear left, middle, and right columns of the vertical beam
                        Printing.DrawAt(laser.Position.X - 1, laser.Position.Y + i, "");
                        Printing.DrawAt(laser.Position.X, laser.Position.Y + i, "");
                        Printing.DrawAt(laser.Position.X + 1, laser.Position.Y + i, "");
                    }
                }
            }
        }*/
    }
}

