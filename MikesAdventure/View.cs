using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Media;


namespace MikesAdventure
{
    public partial class View : Form
    {
        public static int TRAVEL_DISTANCE = 10; // distanca na pomestuvanje na karakterot
        public static int MIN_SIZE = 10; // minimalna golemina na radius na karakterot
        public static int MAX_SIZE = 100; // maksimalna golemina na radius na karakterot
        public static float BLOCK_HEIGHT = 30; // visina na sekoj blok
        public static Timer timer;
        public static Timer bullets;
        public static Timer enemies;
        public static Timer enemyGenerator;
        public static Timer moveBoss;
        public static Timer changeDirection;
        public static Timer bossShoot;
        public string FileName { get; set; }
        public bool Pause = false;   
        public State State { get; set; }
        public Color HeadColor { get; set; }
        public Color EyesColor { get; set; }
        public View(Color hc, Color ec, bool bossTest)
        {
            InitializeComponent();
            State = new State(hc,ec);
            if (bossTest)
            {
                State.Score = 2500;
                State.Level = 6;
            }
            this.DoubleBuffered = true;
            State.generateBlocks();
            State.generateStrawberry();
           
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            enemyGenerator = new Timer();
            enemyGenerator.Interval = 4000;
            enemyGenerator.Tick+=new EventHandler(enemyGenerator_Tick);
            enemyGenerator.Start();

            bullets = new Timer();
            bullets.Interval = 20;
            bullets.Tick += new EventHandler(bullets_Tick);
            bullets.Start();

            enemies = new Timer();
            enemies.Interval = 20;
            enemies.Tick += new EventHandler(enemies_Tick);
            enemies.Start();

            moveBoss = new Timer();
            moveBoss.Interval = 100;
            moveBoss.Tick+=new EventHandler(moveBoss_Tick);

            changeDirection = new Timer();
            changeDirection.Interval = 3000;
            changeDirection.Tick+=new EventHandler(changeDirection_Tick);

            bossShoot = new Timer();
            bossShoot.Interval = 600;
            bossShoot.Tick+=new EventHandler(bossShoot_Tick);
        }

        public void bossShoot_Tick(Object sender, EventArgs e)
        {
            State.AddBossBullet(new Bullet(new Point(State.Boss.Point.X + State.Boss.Radius / 2,State.Boss.Point.Y + State.Boss.Radius), Color.Black));
            Invalidate();
        }

        public void changeDirection_Tick(Object sender, EventArgs e)
        {
            State.ChangeDirection();
            Invalidate();
        }
        public void moveBoss_Tick(Object sender, EventArgs e)
        {
            if (State.Boss != null)
            {
                State.MoveBoss();
                Invalidate();
            }
            
        }
        public void enemyGenerator_Tick(Object sender, EventArgs e)
        {
            State.generateEnemy();
            Invalidate();
        }
        public void enemies_Tick(Object sender, EventArgs e)
        {
            State.MoveEnemies();
            Invalidate();
        }
        public void bullets_Tick(Object sender, EventArgs e)
        {
            State.MoveBullets();
            Invalidate();
        }

        public void timer_Tick(Object sender, EventArgs e)
        {
            State.COUNTER++;
            if (State.COUNTER == 50)
            {
                State.generateBlocks();
                State.generateStrawberry();
                State.COUNTER = 0;
            }
            State.MoveObstacles();
            Invalidate();

        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == (Keys.Left & Keys.Space) && !Pause)
            {
                State.Mike.MoveLeft(TRAVEL_DISTANCE);
                State.Mike.Enlarge(5);
            }
            else if (e.KeyCode == Keys.Up && !Pause)
            {
                State.Mike.MoveUp(TRAVEL_DISTANCE);
            }
            else if (e.KeyCode == Keys.Down && !Pause)
            {
                State.Mike.MoveDown(TRAVEL_DISTANCE);
            }
            else if (e.KeyCode == Keys.Left && !Pause)
            {
                State.Mike.MoveLeft(TRAVEL_DISTANCE);
            }
            else if (e.KeyCode == Keys.Right && !Pause)
            {
                State.Mike.MoveRight(TRAVEL_DISTANCE);
            }
            else if (e.KeyCode == Keys.Space && !Pause)
            {
                State.Mike.Enlarge(5);
            }
            else if (e.KeyCode == Keys.ControlKey && !Pause)
            {
                State.Mike.Shrink(5);
            }
            else if (e.KeyCode == Keys.Z && State.Score >= 5 && !Pause)
            {
                State.AddBullet(new Bullet(new Point(State.Mike.Point.X, State.Mike.Point.Y - State.Mike.Radius), State.Mike.HeadColor));
                    State.UpdateScore(-5);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (!Pause)
                {
                    Pause = true;
                    stopTimers();
                }
                else
                {
                    Pause = false;
                    startTimers();
                }
            }
                Invalidate();
        }

        public void updateTimers()
        {
            if (State.Level == 1)
            {
                timer.Interval = 200;
                enemyGenerator.Interval = 4000;
                enemies.Interval = 20;
            }
            else if (State.Level == 2)
            {
                timer.Interval = 160;
                enemyGenerator.Interval = 3600;
                enemies.Interval = 18;
            }
            else if (State.Level == 3)
            {
                timer.Interval = 130;
                enemyGenerator.Interval = 3000;
                enemies.Interval = 16;
            }
            else if (State.Level == 4)
            {
                timer.Interval = 90;
                enemyGenerator.Interval = 2600;
                enemies.Interval = 12;
            }
            else if (State.Level == 5)
            {
                timer.Interval = 60;
                enemyGenerator.Interval = 2000;
                enemies.Interval = 10;
            }
            else if (State.Level == 6)
            {
                if (State.Boss == null)
                {
                    stopTimers();
                    State.Clear();
                    State.generateBoss();
                    //scoreLabel.Text = "Score: " + State.Score + " Level: " + State.Level;
                    bullets.Start();
                    changeDirection.Start();
                    moveBoss.Start();
                    bossShoot.Start();
                }
                
            }
        }
        public void stopTimers()
        {
            timer.Stop();
            bullets.Stop();
            enemies.Stop();
            enemyGenerator.Stop();
            moveBoss.Stop();
            changeDirection.Stop();
            bossShoot.Stop();

        }
        public void startTimers()
        {
           
            if (State.Boss != null)
            {
                moveBoss.Start();
                changeDirection.Start();
                bossShoot.Start();
                bullets.Start();
            }
            else
            {
                timer.Start();
                enemies.Start();
                enemyGenerator.Start();
                bullets.Start();
            }
        }
        private void View_Paint_1(object sender, PaintEventArgs e)
        {
            State.Draw(e.Graphics);
            updateTimers();
            e.Graphics.Clear(Color.MediumSeaGreen);
            scoreLabel.Text = "Score: " + State.Score + " Level: " + State.Level;
            State.checkBulletCollision();
            State.checkBulletsCollision();
            State.checkStrawberryCollision();
            if (State.checkObstacleCollision() || State.checkEnemyCollision() || State.checkIfHit() || State.checkBossHit())
            {
                stopTimers();
                State.Draw(e.Graphics);
                MessageBox.Show("Резултат: " + State.Score, "Крај",MessageBoxButtons.OK);
                this.Close();
            }
            State.Draw(e.Graphics);
            
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            stopTimers();
            if (FileName == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save game";
                sfd.Filter = "Mike's adventure file type (*.ma)|*.ma";
                if (sfd.ShowDialog() == DialogResult.OK)
                    FileName = sfd.FileName;
            }
            if (FileName != null)
            {
                IFormatter ift = new BinaryFormatter();
                FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                ift.Serialize(fs, State);
                fs.Close();
            }
            startTimers();
            Pause = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopTimers();
            if (FileName == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save game";
                sfd.Filter = "Mike's adventure file type (*.ma)|*.ma";
                if (sfd.ShowDialog() == DialogResult.OK)
                    FileName = sfd.FileName;
            }
            if (FileName != null)
            {
                IFormatter ift = new BinaryFormatter();
                FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                ift.Serialize(fs, State);
                fs.Close();
            }
            startTimers();
            Pause = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopTimers();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open game";
            ofd.Filter = "Mike's adventure file type (*.ma)|*.ma";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileName = ofd.FileName;
                IFormatter ift = new BinaryFormatter();
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                State = (State)ift.Deserialize(fs);
                fs.Close();
            }
            startTimers();
            Pause = false;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            stopTimers();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open game";
            ofd.Filter = "Mike's adventure file type (*.ma)|*.ma";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileName = ofd.FileName;
                IFormatter ift = new BinaryFormatter();
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                State = (State)ift.Deserialize(fs);
                fs.Close();
            }
            startTimers();
            Pause = false;
        }
    }
}
