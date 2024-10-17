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
            WierdGuy,
        }

        public bool Movealbe = true; // Biến để làm cho boss không di chuyển được
        public int BossLife; // Máu của boss
        private BossType bossType; // Loại Boss (chỉ một loại ở 1 lúc)

        /// <summary>
        /// ạo một đối tượng boss từ một loại đã cho
        /// </summary>
        /// <param name="type">Type</param>
        public Boss(int type)
        {
            bossType = (BossType) type;
            this.Point = new Point2D(58,14);
            switch (bossType)
            {
                    case BossType.WierdGuy:
                    BossLife = 40;
                    break;
            }
        }
        /// <summary>
        /// Vẽ boss
        /// </summary>
        private void BossPrint()
        {
            Printing.DrawAt(this.Point.X+13, this.Point.Y - 12, @",");
            Printing.DrawAt(this.Point.X+12, this.Point.Y - 11, @"/(");
            Printing.DrawAt(this.Point.X+12, this.Point.Y - 10, @"\ \___   /");
            Printing.DrawAt(this.Point.X+12, this.Point.Y - 9, @"/- _  `-/");
            Printing.DrawAt(this.Point.X+11, this.Point.Y - 8, @"(/\/ \ \");
            Printing.DrawAt(this.Point.X+11, this.Point.Y - 7, @"/ /   | `");
            Printing.DrawAt(this.Point.X+11, this.Point.Y - 6, @"O O   ) /");
            Printing.DrawAt(this.Point.X+11, this.Point.Y - 5, @"`-^--'`<");
            Printing.DrawAt(this.Point.X+11, this.Point.Y - 4, @"(_.)  _  )");
            Printing.DrawAt(this.Point.X+11, this.Point.Y - 3, @"`.___/`");
            Printing.DrawAt(this.Point.X+13, this.Point.Y - 2, @"`-----' /");
            Printing.DrawAt(this.Point.X, this.Point.Y - 1, @"<----.     __ / __   \");
            Printing.DrawAt(this.Point,                     @"<----|====O)))==) \) /");
            Printing.DrawAt(this.Point.X, this.Point.Y+1,   @"<----'    `--' `.__,'");
            Printing.DrawAt(this.Point.X+13, this.Point.Y+2,   @"|");
            Printing.DrawAt(this.Point.X+14, this.Point.Y+3,   @"\");
            Printing.DrawAt(this.Point.X+9, this.Point.Y+4,   @"______( (_  /");
            Printing.DrawAt(this.Point.X+7, this.Point.Y+5,   @",'  ,-----'   |");
            Printing.DrawAt(this.Point.X+7, this.Point.Y+6,   @"`--{__________)");
        }
        /// <summary>
        /// Clear boss player 
        /// </summary>
        private void BossClear()
        {
            Printing.DrawAt(this.Point.X + 13, this.Point.Y - 12, @" ");
            Printing.DrawAt(this.Point.X + 12, this.Point.Y - 11, @"  ");
            Printing.DrawAt(this.Point.X + 12, this.Point.Y - 10, @"          ");
            Printing.DrawAt(this.Point.X + 12, this.Point.Y - 9, @"         ");
            Printing.DrawAt(this.Point.X + 11, this.Point.Y - 8, @"        ");
            Printing.DrawAt(this.Point.X + 11, this.Point.Y - 7, @"         ");
            Printing.DrawAt(this.Point.X + 11, this.Point.Y - 6, @"         ");
            Printing.DrawAt(this.Point.X + 11, this.Point.Y - 5, @"        ");
            Printing.DrawAt(this.Point.X + 11, this.Point.Y - 4, @"          ");
            Printing.DrawAt(this.Point.X + 11, this.Point.Y - 3, @"       ");
            Printing.DrawAt(this.Point.X + 13, this.Point.Y - 2, @"         ");
            Printing.DrawAt(this.Point.X     , this.Point.Y - 1, @"                      ");
            Printing.DrawAt(this.Point.X     , this.Point.Y,     @"                      ");
            Printing.DrawAt(this.Point.X     , this.Point.Y + 1, @"                     ");
            Printing.DrawAt(this.Point.X + 13, this.Point.Y + 2, @" ");
            Printing.DrawAt(this.Point.X + 14, this.Point.Y + 3, @" ");
            Printing.DrawAt(this.Point.X + 9 , this.Point.Y + 4, @"             ");
            Printing.DrawAt(this.Point.X + 7 , this.Point.Y + 5, @"               ");
            Printing.DrawAt(this.Point.X + 7 , this.Point.Y + 6, @"               ");
        }

        private List<BossObject> bossGameObjects = new List<BossObject>(); // Các đối tượng boss đã được sinh ra
        private int _counter = 1; // Bộ đếm
        private int chance = 30; // Cơ hội để sinh một đối tượng 1 trong # lần
        private bool _entryAnimationPlayed = false; // Biến để cho biết liệu hoạt hình bắt đầu đã được phát

        /// <summary>
        /// AI của boss, tính toán các chuyển động và xuất các đối tượng boss
        /// </summary>
        public void BossAI()
        {
            if (!_entryAnimationPlayed) // Nếu hoạt ảnh chưa được chạy
            {

                BossEntryAnimation(); // Chạy hoạt ảnh
                _entryAnimationPlayed = true; 
            }
            if (this.BossLife <= 0) // Nếu boss hết máu
            {
                Engine.BossActive = false; 
                //Xóa tất cả đối tượng boss từ màn hình
                foreach (var bossGameObject in bossGameObjects)
                {
                    bossGameObject.ClearObjectCheckColision();
                }
                MediaPlayer death = new MediaPlayer();
                death.Open(new Uri("Resources/cat.wav", UriKind.Relative));
                death.Play();
                BossDeathAnimation(); // Chạy hoạt ảnh chết của boss
                Engine.Player.IncreasePoints(90); // +90 điểm cho người chơi
                Menu.UIDescription(); // Vẽ lại UI
                return;
            }
            
            // Nếu tới lúc xuất đối tượng Boss
            if (_counter % chance == 0)
            {
                // Chọn 1 loại ngẫu nhiên
                int type = Engine.Rnd.Next(0, 4);
                switch (type)
                {
                    // Tạo 10 viên đạn
                    case 0: 
                        for (int i = 0; i < 10; i++)
                        {
                            bossGameObjects.Add(new BossObject(new Point2D(this.Point.X - 5, this.Point.Y + Engine.Rnd.Next(-5,5)), type));
                        }
                        break;
                    // Create bullets from the trident
                    case 1: bossGameObjects.Add(new BossObject(new Point2D(this.Point.X - 5, this.Point.Y), type));
                        break;
                    // Create Laser
                    case 2: bossGameObjects.Add(new BossObject(new Point2D(this.Point.X - 5, this.Point.Y), type));
                        break;
                    // Create a Mine
                    case 3: bossGameObjects.Add(new BossObject(new Point2D(this.Point.X - 5, this.Point.Y), type));
                        break;
                    // Create a soundwave (unimplemented)
                    case 4: bossGameObjects.Add(new BossObject(new Point2D(this.Point.X - 5, this.Point.Y), type));
                        break;
                }
            }
            _counter++;
            // Các số ngẫu nhiên để quyết định boss có nên di chuyển
            int move = Engine.Rnd.Next(0, 100);
            if (move > 20 && move < 30 && Movealbe && this.Point.Y + 1 <= Engine.WindowHeight - 9)
            {
                BossClear();
                this.Point.Y++;
            }
            if (move > 80 && move < 90 && Movealbe && this.Point.Y - 1 >= 14)
            {
                BossClear();
                this.Point.Y--;
            }
            // Vẽ boss
            BossPrint();
            BossObjectsMoveAndDraw();
            
        }

        /// <summary>
        /// Di chuyển và vẽ các đối tượng boss
        /// </summary>
        private void BossObjectsMoveAndDraw()
        {
            List<BossObject> newObjects = new List<BossObject>(); // Danh sách những đối tượng đã di chuuyeenr
            foreach (var bossGameObject in bossGameObjects) 
            {
                bossGameObject.ClearObjectCheckColision(); // Xóa khỏi màn hình và kiểm tra va chạm
                
                //Đối máu đối tượng < 0 hoặc nếu nó thoát ra khỏi ngoài màn hình
                if (bossGameObject.GetLifeOnScreen() <= 0 ||
                    (bossGameObject.Point.X < 5 || bossGameObject.Point.X > Engine.WindowWidth -5 || bossGameObject.Point.Y < 3 || bossGameObject.Point.Y >= Engine.WindowHeight - 3))
                {
                    // Không thêm vào danh sách những đối tượng đã di chuyển
                }
                else
                {
                    // Di chuyển đối tượng
                    bossGameObject.MoveObject();
                    // In nó tại vị trí mới
                    bossGameObject.PrintObject();
                    // Thêm vào danh sách các đối tượng đã di chuyển
                    newObjects.Add(bossGameObject);
                } 
            }
            bossGameObjects = newObjects; // Ghi đè các đối tượng cũ bằng các đối tượng đã di chuyển
        }

        /// <summary>
        /// Kiểm tra va chạm của boss
        /// </summary>
        /// <param name="point">Kiểm tra với Point2D</param>
        /// <returns>Nếu boss bị đánh trúng</returns>
        public bool BossHit(Point2D point ) 
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
                ((point.X >= this.Point.X + 0  && point.X <= this.Point.X + 30) && point.Y == this.Point.Y - 1) ||
                ((point.X >= this.Point.X + 0  && point.X <= this.Point.X + 30) && point.Y == this.Point.Y - 0) ||
                ((point.X >= this.Point.X + 0  && point.X <= this.Point.X + 30) && point.Y == this.Point.Y + 1) ||
                ((point.X >= this.Point.X + 13 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 2) ||
                ((point.X >= this.Point.X + 14 && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 3) ||
                ((point.X >= this.Point.X + 9  && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 4) ||
                ((point.X >= this.Point.X + 7  && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 5) ||
                ((point.X >= this.Point.X + 7  && point.X <= this.Point.X + 20) && point.Y == this.Point.Y + 6))
            {
                this.BossLife--; // giảm máu của boss
                Engine.PlayBossHit = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///Hoạt ảnh chết của Boss
        /// </summary>
        private void BossDeathAnimation()
        {
            Printing.DrawStringCharByChar(this.Point.X + 13, this.Point.Y - 12, @" ",5,false);
            Printing.DrawStringCharByChar(this.Point.X + 12, this.Point.Y - 11, @"  ",5,true);
            Printing.DrawStringCharByChar(this.Point.X + 12, this.Point.Y - 10, @"          ",5,false);
            Printing.DrawStringCharByChar(this.Point.X + 12, this.Point.Y - 9, @"         ", 5, true);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 8, @"        ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 7, @"         ", 5, true);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 6, @"         ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 5, @"        ", 5, true);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 4, @"          ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 3, @"       ", 5, true);
            Printing.DrawStringCharByChar(this.Point.X + 13, this.Point.Y - 2, @"         ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 1, @"                      ", 5, true);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y, @"                      ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 1, @"                     ", 5, true);
            Printing.DrawStringCharByChar(this.Point.X + 13, this.Point.Y + 2, @" ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 14, this.Point.Y + 3, @" ", 5, true);
            Printing.DrawStringCharByChar(this.Point.X + 9, this.Point.Y + 4, @"             ", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 7, this.Point.Y + 5, @"               ", 5, true);
            Printing.DrawStringCharByChar(this.Point.X + 7, this.Point.Y + 6, @"               ", 5, false);
        }

        /// <summary>
        /// Hoạt ảnh xuất hiện của Boss
        /// </summary>
        private void BossEntryAnimation()
        {
            Printing.DrawStringCharByChar(this.Point.X + 13, this.Point.Y - 12, @",", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 12, this.Point.Y - 11, @"/(", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 12, this.Point.Y - 10, @"\ \___   /", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 12, this.Point.Y - 9, @"/- _  `-/", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 8, @"(/\/ \ \", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 7, @"/ /   | `", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 6, @"O O   ) /", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 5, @"`-^--'`<", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 4, @"(_.)  _  )", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 11, this.Point.Y - 3, @"`.___/`", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 13, this.Point.Y - 2, @"`-----' /", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y - 1, @"<----.     __ / __   \", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y, @"<----|====O)))==) \) /", 5, false);
            Printing.DrawStringCharByChar(this.Point.X, this.Point.Y + 1, @"<----'    `--' `.__,'", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 13, this.Point.Y + 2, @"|", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 14, this.Point.Y + 3, @"\", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 9, this.Point.Y + 4, @"______( (_  /", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 7, this.Point.Y + 5, @",'  ,-----'   |", 5, false);
            Printing.DrawStringCharByChar(this.Point.X + 7, this.Point.Y + 6, @"`--{__________)", 5, false);
        }
    }
}
  
