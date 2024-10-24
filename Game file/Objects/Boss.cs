using System;
using System.Collections.Generic;
using System.Windows.Media;
using TeamWork.Field;

namespace TeamWork.Objects
{
    public class Boss : Entity
    {
        public enum BossType
        {
            PurpleStorm,
        }

        public bool Movealbe = true; // Tag for making the boss imovable
        public int BossLife; // Boss lifepoints
        private BossType bossType; // Type(only one atm)

        /// <summary>
        /// Create a boss object from a given type
        /// </summary>
        /// <param name="type">Type</param>
        public Boss(int type)
        {
            bossType = (BossType)type;
            this.Point = new Point2D(30, 14);
            switch (bossType)
            {
                case BossType.PurpleStorm:
                    BossLife = 40;
                    break;
            }
        }
        /// <summary>
        /// Draw boss player
        /// </summary>
        private void BossPrint()
        {
            //Printing.DrawAt(this.Point.X+13, this.Point.Y - 12, @",");
            //Printing.DrawAt(this.Point.X+12, this.Point.Y - 11, @"/(");
            //Printing.DrawAt(this.Point.X+12, this.Point.Y - 10, @"\ \___   /");
            //Printing.DrawAt(this.Point.X+12, this.Point.Y - 9, @"/- _  `-/");
            //Printing.DrawAt(this.Point.X+11, this.Point.Y - 8, @"(/\/ \ \");
            //Printing.DrawAt(this.Point.X+11, this.Point.Y - 7, @"/ /   | `");
            //Printing.DrawAt(this.Point.X+11, this.Point.Y - 6, @"O O   ) /");
            //Printing.DrawAt(this.Point.X+11, this.Point.Y - 5, @"`-^--'`<");
            //Printing.DrawAt(this.Point.X+11, this.Point.Y - 4, @"(_.)  _  )");
            //Printing.DrawAt(this.Point.X+11, this.Point.Y - 3, @"`.___/`");
            //Printing.DrawAt(this.Point.X+13, this.Point.Y - 2, @"`-----' /");
            //Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"<----.     __ / __   \");
            //Printing.DrawAt(this.Point,                     @"<----|====O)))==) \) /");
            //Printing.DrawAt(this.Point.X, this.Point.Y+1,   @"<----'    `--' `.__,'");
            //Printing.DrawAt(this.Point.X+13, this.Point.Y+2,   @"|");
            //Printing.DrawAt(this.Point.X+14, this.Point.Y+3,   @"\");
            //Printing.DrawAt(this.Point.X+9, this.Point.Y+4,   @"______( (_  /");
            //Printing.DrawAt(this.Point.X+7, this.Point.Y+5,   @",'  ,-----'   |");
            //Printing.DrawAt(this.Point.X+7, this.Point.Y+6,   @"`--{__________)");

            Printing.DrawAt(this.Point.X, this.Point.Y - 10, @"                      ████", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 9, @"███████         ███  █    █      ███████", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 8, @"  █████        █   █ ████        █████  ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 7, @"   ████████     ███████ █     ████████  ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 6, @"    ██████████  █ ███████ ██████████    ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 5, @"                ████████████            ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 4, @"           █          ██          █     ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 3, @"       █████          ██          █████ ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 2, @"       ██████  ███ ██████████ ███ ██████", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"             █████ █ █████ ██ █████     ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y, @"       █████████   ████  ████   █████████", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @"      ████████  ██████  ██████  ████████ ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"      ████          █    █          ████ ", ConsoleColor.DarkMagenta);
        }
        /// <summary>
        /// Clear boss player 
        /// </summary>
        private void BossClear()
        {
            Printing.DrawAt(this.Point.X, this.Point.Y - 10, @"                          ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 9, @"                                        ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 8, @"                                        ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 7, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 6, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 5, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 4, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 3, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 2, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y, @"                                         ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @"                                         ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"                                         ", ConsoleColor.DarkMagenta);
        }

        private List<BossObject> bossGameObjects = new List<BossObject>(); // Boss spawned objects
        private int _counter = 1; // Counter
        private int chance = 30; // Chance to spawn a object 1 in # times
        private bool _entryAnimationPlayed = false; // Tag to tell if the starting animation is played

        /// <summary>
        /// Boss AI, all movement and boss object spawning is calculated here
        /// </summary>
        public void BossAI()
        {
            if (!_entryAnimationPlayed) // If the animation is not played yet
            {

                BossEntryAnimation(); // Play it
                _entryAnimationPlayed = true; // And trigger the entryAnimation tag
            }
            if (this.BossLife <= 0) // If the boss has no life left
            {
                Engine.BossActive = false; // Trigger the boss active boolean in Engine class
                //Clear all Boss spawned objects from the screen
                foreach (var bossGameObject in bossGameObjects)
                {
                    bossGameObject.ClearObjectCheckColision();
                }
                MediaPlayer death = new MediaPlayer();
                death.Open(new Uri("Resources/cat.wav", UriKind.Relative));
                death.Play();
                BossDeathAnimation(); // Play the boss death "animation"
                Engine.Player.IncreasePoints(90); // Increase player points by 90
                Menu.UIDescription(); // Redraw the UI Description
                return;
            }

            // If its time to spawn a new object
            if (_counter % chance == 0)
            {
                // Get a random type and pass it to the switch
                int type = Engine.Rnd.Next(0, 4);
                switch (type)
                {
                    // Create 10 rockets
                    case 0:
                        for (int i = 0; i < 3; i++)
                        {
                            bossGameObjects.Add(new BossObject(new Point2D(this.Point.X + 23 + Engine.Rnd.Next(-5, 5), this.Point.Y + 3), type));
                        }
                        break;
                    // Create bullets from the trident
                    case 1:
                        bossGameObjects.Add(new BossObject(new Point2D(this.Point.X + 23, this.Point.Y + 3), type));
                        break;
                    // Create Laser
                    case 2:
                        bossGameObjects.Add(new BossObject(new Point2D(this.Point.X + 23, this.Point.Y + 3), type));
                        break;
                    // Create a Mine
                    case 3:
                        bossGameObjects.Add(new BossObject(new Point2D(this.Point.X + 23, this.Point.Y + 3), type));
                        break;
                }
            }
            _counter++;
            // Random number to decide should the boss move
            int move = Engine.Rnd.Next(0, 100);
            if (move > 20 && move < 30 && Movealbe && this.Point.X + 1 <= Engine.WindowWidth - 30)
            {
                BossClear();
                this.Point.X += 3;
            }
            if (move > 70 && move < 90 && Movealbe && this.Point.X - 1 >= 2)
            {
                BossClear();
                this.Point.X -= 3;
            }
            // Print the boss
            BossPrint();
            BossObjectsMoveAndDraw();

        }

        /// <summary>
        /// Move and draw the boss objects
        /// </summary>
        private void BossObjectsMoveAndDraw()
        {
            List<BossObject> newObjects = new List<BossObject>(); // List of the moved objects
            foreach (var bossGameObject in bossGameObjects) // Itterate through all current objects
            {
                bossGameObject.ClearObjectCheckColision(); // Clear from the screen and check for collision with the player

                // if the object life is 0 or less, or its out of the screen
                if (bossGameObject.GetLifeOnScreen() <= 0 ||
                    (bossGameObject.Point.X < 5 || bossGameObject.Point.X > Engine.WindowWidth - 5 || bossGameObject.Point.Y < 3 || bossGameObject.Point.Y >= Engine.WindowHeight - 3))
                {
                    // Don't add it to the list with the moved objects
                }
                else
                {
                    // Move the object
                    bossGameObject.MoveObject();
                    // Print it at its new position
                    bossGameObject.PrintObject();
                    // Add it to the list with moved objects
                    newObjects.Add(bossGameObject);
                }
            }
            bossGameObjects = newObjects; // Overwrite old objects with the moved ones
        }

        /// <summary>
        /// Boss collision check
        /// </summary>
        /// <param name="point">Point2D to check with</param>
        /// <returns>If the boss is hit</returns>
        public bool BossHit(Point2D point)
        {
            if (((point.X == this.Point.X + 13 || point.X == this.Point.X + 14) && point.Y == this.Point.Y - 12) ||
                ((point.X >= this.Point.X + 12 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 11) ||
                ((point.X >= this.Point.X + 12 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 10) ||
                ((point.X >= this.Point.X + 12 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 9) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 8) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 7) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 6) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 5) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 4) ||
                ((point.X >= this.Point.X + 11 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 3) ||
                ((point.X >= this.Point.X + 13 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y - 2) ||
                ((point.X >= this.Point.X + 0 && point.X <= this.Point.X + 30) && point.Y == this.Point.Y - 1) ||
                ((point.X >= this.Point.X + 0 && point.X <= this.Point.X + 30) && point.Y == this.Point.Y - 0) ||
                ((point.X >= this.Point.X + 0 && point.X <= this.Point.X + 30) && point.Y == this.Point.Y + 1) ||
                ((point.X >= this.Point.X + 13 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 2) ||
                ((point.X >= this.Point.X + 14 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 3) ||
                ((point.X >= this.Point.X + 9 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 4) ||
                ((point.X >= this.Point.X + 7 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 5) ||
                ((point.X >= this.Point.X + 7 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 6))
            {
                this.BossLife--; // Decrease boss life
                Engine.PlayBossHit = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Boss death "Animation"
        /// </summary>
        private void BossDeathAnimation()
        {
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 10, @"                           ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 9, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 8, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 7, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 6, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 5, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 4, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 3, @"                                        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y, @"                                         ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 1, @"                                         ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 2, @"                                         ", 5, false);

            Printing.DrawAt(this.Point.X, this.Point.Y - 10, @"                          ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 9, @"                                        ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 8, @"                                        ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 7, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 6, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 5, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 4, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 3, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 2, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"                                        ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y, @"                                         ", ConsoleColor.Magenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 1, @"                                         ", ConsoleColor.DarkMagenta);
            Printing.DrawAt(this.Point.X, this.Point.Y + 2, @"                                         ", ConsoleColor.DarkMagenta);

        }

        /// <summary>
        /// Boss Entry "Entry animation"
        /// </summary>
        private void BossEntryAnimation()
        {
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 10, @"                      ████", 1, false, ConsoleColor.DarkMagenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 9, @"███████         ███  █    █      ███████", 1, false, ConsoleColor.DarkMagenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 8, @"  █████        █   █ ████        █████  ", 1, false, ConsoleColor.DarkMagenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 7, @"   ████████     ███████ █     ████████  ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 6, @"    ██████████  █ ███████ ██████████    ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 5, @"                ████████████            ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 4, @"           █          ██          █     ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 3, @"       █████          ██          █████ ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 2, @"       ██████  ███ ██████████ ███ ██████", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 1, @"             █████ █ █████ ██ █████     ", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y, @"       █████████   ████  ████   █████████", 1, false, ConsoleColor.Magenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 1, @"      ████████  ██████  ██████  ████████ ", 1, false, ConsoleColor.DarkMagenta);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 2, @"      ████          █    █          ████ ", 1, false, ConsoleColor.DarkMagenta);





        }
    }
}

