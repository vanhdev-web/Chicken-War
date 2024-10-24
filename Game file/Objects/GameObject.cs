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
            Enemy1,
            /* Enemy1 Meteorid with 4 life, gives 5 points, no additional functions
                    \ /
                     x
                    / \
             */
            Enemy2,
            /* Enemy2 Meteorid with 3 life, gives 4 points, no additional functions
                     ^
                    <x>
                     v
             */
            Enemy3,
            /* Enemy3 Meteorid with 3 life, gives 3 points, no additional functions
                    {==>
             */
            Enemy4,
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
                case ObjectType.Enemy1:
                    life = 7;
                    base.Point = new Point2D(Engine.Rnd.Next(Engine.WindowWidth - 4), 5);
                    this.Down = true;
                    break;
                case ObjectType.Enemy2:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight / 2 - 4));
                    this.Down = false;
                    break;
                case ObjectType.Enemy3:
                    life = 7;
                    base.Point = new Point2D(Engine.WindowWidth - 2, Engine.Rnd.Next(6, Engine.WindowHeight / 2 - 4));
                    this.Down = false;
                    break;
                case ObjectType.Enemy4:
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

        private int projectileCounter = 1; // Counter to check with if the enemy should fire 
        private int projectileChance = Engine.Rnd.Next(20, 50); // Random chance that enemy will fire a bullet

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
                case ObjectType.Enemy1:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if enemy has to shoot
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
                case ObjectType.Enemy2:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if enemy has to shoot
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
                case ObjectType.Enemy3:
                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if enemy has to shoot
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

                        #region Enemy Entry animation
                        /* Only agressive enemy type, shoots back, has 7 life, gives 10 points
                   __       __
                  _\_\_____/_|
                <[__\_\_-----<"
                     oo' 
             */

                        // This makes the enemy entry smooth, not instant spawn in the center of the screen
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
                case ObjectType.Enemy4:


                    if (!this.GotHit)
                    {
                        if (projectileCounter % projectileChance == 0) // Check if enemy has to shoot
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
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"███", ConsoleColor.DarkMagenta);
                        Printing.DrawAt(this.Point.X, Point.Y - 1 , @"████", ConsoleColor.Magenta);
                        Printing.DrawAt(this.Point.X, Point.Y, @"██", ConsoleColor.DarkMagenta);
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
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"██", ConsoleColor.DarkGray);
                        Printing.DrawAt(this.Point.X, Point.Y - 1 , @"████", ConsoleColor.Gray);
                        Printing.DrawAt(this.Point.X, Point.Y, @"███", ConsoleColor.DarkGray);
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
                case ObjectType.Enemy1:
                    #region Enemy1 object clearing and breaking effect
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
                case ObjectType.Enemy2:
                    #region Enemy2 object clearing and breaking effect math
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
                case ObjectType.Enemy3:
                    #region Enemy3 object clear and breaking effect math
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
                case ObjectType.Enemy4:
                    #region Enemy4 object clear and breaking effect
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
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"   ");
                        Printing.DrawAt(this.Point.X, Point.Y - 1, @"    ");
                        Printing.DrawAt(this.Point.X, Point.Y, @"  ");
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
                        Printing.DrawAt(this.Point.X + 1, Point.Y - 2, @"  ");
                        Printing.DrawAt(this.Point.X, Point.Y - 1, @"    ");
                        Printing.DrawAt(this.Point.X, Point.Y, @"   ");
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
                case ObjectType.Enemy1:
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
                case ObjectType.Enemy2:
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
                case ObjectType.Enemy3:
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
                case ObjectType.Enemy4:
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
                   
                    if (((x >= this.Point.X && x <= this.Point.X + 4) && (y == this.Point.Y)) ||
     ((x >= this.Point.X && x <= this.Point.X + 3) && (y == this.Point.Y - 1)))
                    {
                        return true;
                    }
                    return false;

                case ObjectType.Meteor2:
                    // Shape:
                    //   ..A
                    //   BCDEF
                    //   .G.
                 
                    if (((x >= this.Point.X && x <= this.Point.X + 4) && (y == this.Point.Y)) ||
         ((x >= this.Point.X + 1 && x <= this.Point.X + 4) && (y == this.Point.Y - 1)) ||
         ((x >= this.Point.X && x <= this.Point.X + 2) && (y == this.Point.Y + 1)))
                    {
                        return true;
                    }
                    return false;

                case ObjectType.Meteor3:
                    // Shape:
                    //  ..A
                    //  BCDEF
                    //  .G.
                   
                    if (((x >= this.Point.X && x <= this.Point.X + 4) && (y == this.Point.Y)) ||
        ((x >= this.Point.X + 1 && x <= this.Point.X + 3) && (y == this.Point.Y - 1)) ||
        ((x >= this.Point.X && x <= this.Point.X + 3) && (y == this.Point.Y + 1)))
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
    }
}

