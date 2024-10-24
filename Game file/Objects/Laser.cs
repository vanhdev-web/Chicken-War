﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork.Objects
{
    internal class LaserAttack:Entity
    {
        
    public class Laser
    {
        public Point Position { get; set; }
        public int LifeOnScreen { get; set; } // To manage the charge-up effect
    }
    public class GameObject 
    {
        public Point Point { get; set; }
        private List<Laser> lasers = new List<Laser>();
        private const int MoveSpeed = 2; // Speed of the object's 
        private const int FireDelay = 1000; // 1 second delay
        private DateTime lastFireTime;
        public int chargingDelayTime = 1000; // Thời gian ngưng trước khi charge (2 giây)
        private bool isDelayBeforeCharge = false; // Cờ để kiểm tra xem đã dừng trước khi charge chưa
        private DateTime delayStartTime; // Để lưu thời gian bắt đầu dừng
        public bool IsCharging { get; private set; } // New property to track charging state
        private int direction = 1; // 1 for right, -1 for left
        private Random random = new Random(); // Random number generator
        public GameObject()
        {
            lastFireTime = DateTime.Now; // Initialize the last fire time
            IsCharging = false; // Initialize as not charging
        }


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
                    Laser newLaser = new Laser { Position = new Point { X = this.Point.X + 3, Y = this.Point.Y  }, LifeOnScreen = 10 }; // Start below character
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
        }
        

    }

    public class Bullet
    {
        public Point Position { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    
    }
}