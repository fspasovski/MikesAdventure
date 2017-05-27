using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MikesAdventure
{
    [Serializable]
    public class State
    {
        public int COUNTER = 0;
        public int WINDOW_WIDTH = 1026;
        public int WINDOW_HEIGHT = 519;
        public int BLOCK_HEIGHT = 30; // visina na sekoj blok
        public static int MIN_SIZE = 10; // minimalna golemina na radius na karakterot
        public static int MAX_SIZE = 100; // maksimalna golemina na radius na karakterot
        public Mike Mike { get; set; }
        public Boss Boss { get; set; }
        public HPbar HPbar { get; set; }
        public List<Obstacle> Obstacles { get; set; }
        public List<Bullet> Bullets { get; set; }
        public List<Bullet> BossBullets { get; set; }
        public Strawberry Strawberry { get; set; }
        public List<Enemy> Enemies { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public string Direction { get; set; }
        public int pointID { get; set; }

        public State(Color HeadColor, Color EyesColor)
        {
            Mike = new Mike(new Point(WINDOW_WIDTH / 2, WINDOW_HEIGHT - 100), HeadColor, 20, EyesColor);
            Obstacles = new List<Obstacle>();
            Bullets = new List<Bullet>();
            BossBullets = new List<Bullet>();
            Enemies = new List<Enemy>();
            Score = 0;
            pointID = 0;
            UpdateLevel();
        }

        public void AddObstacle(Obstacle o)
        {
            Obstacles.Add(o);
        }
        public void AddBullet(Bullet b)
        {
            Bullets.Add(b);
        }
        public void AddBossBullet(Bullet b)
        {
            BossBullets.Add(b);
        }
        public void AddEnemy(Enemy e)
        {
            Enemies.Add(e);
        }

        public void Draw(Graphics g)
        {
            Mike.Draw(g);
            if(Strawberry!=null)
                Strawberry.Draw(g);
            if (Boss != null)
            {
                Boss.Draw(g);
                HPbar.Draw(g);
            }
            foreach (Obstacle o in Obstacles)
            {
                o.Draw(g);
            }
            foreach (Enemy e in Enemies)
            {
                e.Draw(g);
            }
            foreach (Bullet b in Bullets)
            {
                b.Draw(g);
            }
            foreach (Bullet b in BossBullets)
                b.Draw(g);
        }
        public void generateBlocks()
        {

            Random random = new Random();

            int r = random.Next(0, 2);

            if (r == 0)
            {
                int w1 = random.Next(20, WINDOW_WIDTH - 150);
                int gap = random.Next(2 * MIN_SIZE + 10, 2 * MAX_SIZE);
                int w2 = WINDOW_WIDTH - w1 - gap;


                AddObstacle(new Obstacle(new Point(w1 / 2, 30), "Obstacle", Color.SaddleBrown, w1, BLOCK_HEIGHT));
                AddObstacle(new Obstacle(new Point(w1 + gap / 2, 10), "Point" + pointID, Color.Transparent, gap, BLOCK_HEIGHT / 3));
                AddObstacle(new Obstacle(new Point(w1 + gap + w2 / 2, 30), "Obstacle", Color.SaddleBrown, w2, BLOCK_HEIGHT));
            }
            else
            {

                int gap1 = random.Next(2 * MIN_SIZE + 10, 2 * MAX_SIZE);
                int gap2 = random.Next(2 * MIN_SIZE + 10, 2 * MAX_SIZE);
                int w1 = random.Next(20, (WINDOW_WIDTH - gap1 - gap2) * 60 / 100);
                int w2 = random.Next(20, (WINDOW_WIDTH - w1 - gap2 - gap1) * 80 / 100);
                int w3 = WINDOW_WIDTH - w1 - w2 - gap1 - gap2;

                AddObstacle(new Obstacle(new Point(w1 / 2, 30), "Obstacle", Color.SaddleBrown, w1, BLOCK_HEIGHT));
                AddObstacle(new Obstacle(new Point(w1 + gap1 / 2, 10), "Point" + pointID, Color.Transparent, gap1, BLOCK_HEIGHT / 3));
                AddObstacle(new Obstacle(new Point(w1 + gap1 + w2 / 2, 30), "Obstacle", Color.SaddleBrown, w2, BLOCK_HEIGHT));
                AddObstacle(new Obstacle(new Point(w1 + gap1 + w2 + gap2 / 2, 10), "Point" + pointID, Color.Transparent, gap2, BLOCK_HEIGHT / 3));
                AddObstacle(new Obstacle(new Point(w1 + gap1 + w2 + gap2 + w3 / 2, 30), "Obstacle", Color.SaddleBrown, w3, BLOCK_HEIGHT));
            }
            pointID++;
        }

        public void UpdateScore(int i)
        {
            Score += i;
            this.UpdateLevel();
            
        }
        public void UpdateLevel()
        {
            if (Score <= 500 && Level!=6)
            {
                Level = 1;
            }
            else if (Score > 500 && Score <= 1000 && Level != 6)
            {
                Level = 2;
            }
            else if (Score > 1000 && Score <= 1500 && Level != 6)
            {
                Level = 3;
            }
            else if (Score > 1500 && Score <= 2000 && Level != 6)
            {
                Level = 4;
            }
            else if (Score > 2000 && Score <= 2500 && Level != 6)
            {
                Level = 5;
            }
            else if (Score > 2500)
            {
                Level = 6;
            }
        }
        public void generateStrawberry()
        {
            Random random = new Random();

            if (random.Next(0, 2) == 0)
            {
                int x = random.Next(50, WINDOW_WIDTH - 50);
                int y = random.Next(50, WINDOW_HEIGHT - 30);
                if(pointDistance(new Point(x,y),Mike.Point) > 4 + Mike.Radius)
                    Strawberry  = new Strawberry(new Point(x,y));
                
            }
        }

        public void generateBoss()
        {
            Random r = new Random();
            Boss = new Boss(new Point(r.Next(80, WINDOW_WIDTH - 80), 100), 35);
            HPbar = new HPbar();
        }
        public void generateEnemy()
        {
            Random random = new Random();
            AddEnemy(new Enemy(new Point(random.Next(50,WINDOW_WIDTH - 50), 30), 15));
            
        }
        public bool checkEnemyCollision()
        {
            foreach (Enemy e in Enemies)
            {
                if (overlapEnemy(e))
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkObstacleCollision()
        {
            foreach (Obstacle o in Obstacles)
            {
                if (overlapObstacle(o))
                {
                    if (o.Type == "Obstacle")
                    {
                        return true;
                    }
                    else if (o.Worth)
                    {
                        if (Mike.lastPoint == null || (Mike.lastPoint!=null && Mike.lastPoint!=o.Type))
                        {
                            Mike.lastPoint = o.Type;
                            this.UpdateScore(Mike.Radius * 200 / o.A);
                        }
                        o.Worth = false;
                    }
                }
            }
            return false;
        }

        public void checkBulletCollision()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                for (int j = 0; j < Enemies.Count; j++)
                {
                    if (overlapBulletEnemy(Bullets.ElementAt(i), Enemies.ElementAt(j)))
                    {
                        this.UpdateScore(50);
                        Bullets.RemoveAt(i);
                        Enemies.RemoveAt(j);
                        i--;
                        j--;
                    }
                }
            }
            for (int i = 0; i < Bullets.Count; i++)
            {
                foreach (Obstacle o in Obstacles)
                {
                    if (overlapBulletObstacle(Bullets.ElementAt(i),o) && o.Type == "Obstacle")
                    {
                        Bullets.RemoveAt(i);
                        i--;
                        break;
                        
                    }
                }
            }
        }

        public void checkStrawberryCollision()
        {
            
                if (Strawberry!=null && overlapStrawberry())
                {
                    Strawberry = null;
                    Random random = new Random();
                    if (random.Next(0,2) == 0)
                    {
                        Mike.Enlarge(20);
                    }
                    else
                    {
                        this.UpdateScore(100);
                    }
                }
            
        }

        public bool checkBossHit()
        {
            if (Boss != null)
            {
                for (int i = 0; i < Bullets.Count; i++)
                {
                    if (pointDistance(Bullets.ElementAt(i).Point, Boss.Point) <= Boss.Radius + Bullets.ElementAt(i).Radius)
                    {
                        Bullets.RemoveAt(i);
                        i--;
                        Boss.HP -= 1;
                        HPbar.Decrease();
                    }
                }
                if (Boss.HP <= 0)
                    Score += 3000;
                return Boss.HP <= 0;
            }
            return false;
        }

        public void checkBulletsCollision()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                for (int j = 0; j < BossBullets.Count; j++)
                {
                    if (pointDistance(Bullets.ElementAt(i).Point, BossBullets.ElementAt(j).Point) <= Bullets.ElementAt(i).Radius * 2)
                    {
                        Bullets.RemoveAt(i);
                        BossBullets.RemoveAt(j);
                        i--;
                        j--;
                        break;
                    }
                }
            }
        }

        public bool checkIfHit()
        {
            foreach (Bullet b in BossBullets)
            {
                if (pointDistance(b.Point,Mike.Point) <= b.Radius + Mike.Radius)
                    return true;
            }
            if (Boss != null)
                return pointDistance(Mike.Point, Boss.Point) <= Mike.Radius + Boss.Radius;
            else return false;
        }

        public bool overlapBulletEnemy(Bullet b, Enemy e)
        {
            return pointDistance(b.Point, e.Point) <= b.Radius + e.Radius;
        }
        public bool overlapStrawberry()
        {
            return pointDistance(Strawberry.Point, Mike.Point) <= Strawberry.Radius + Mike.Radius;
        }

        public bool overlapEnemy(Enemy e)
        {
            return pointDistance(e.Point, Mike.Point) <= e.Radius + Mike.Radius;
        }


        public double pointDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        
        public bool overlapObstacle(Obstacle o)
        {
            float circleDistancex = Math.Abs(Mike.Point.X - o.Point.X);
            float circleDistancey = Math.Abs(Mike.Point.Y - o.Point.Y);

            if (circleDistancex > (o.A / 2 + Mike.Radius)) { return false; }
            else if (circleDistancey > (o.B / 2 + Mike.Radius)) { return false; }

            else if (circleDistancex <= (o.A / 2)) { return true; }
            else if (circleDistancey <= (o.B / 2)) { return true; }

            double cornerDistancesq = (circleDistancex - o.A / 2) * (circleDistancex - o.A / 2) +
                                 (circleDistancey - o.B / 2) * (circleDistancey - o.B / 2);

            return (cornerDistancesq <= (Mike.Radius * Mike.Radius));
        }
        public bool overlapBulletObstacle(Bullet b, Obstacle o)
        {
            float circleDistancex = Math.Abs(b.Point.X - o.Point.X);
            float circleDistancey = Math.Abs(b.Point.Y - o.Point.Y);

            if (circleDistancex > (o.A / 2 + b.Radius)) { return false; }
            else if (circleDistancey > (o.B / 2 + b.Radius)) { return false; }

            else if (circleDistancex <= (o.A / 2)) { return true; }
            else if (circleDistancey <= (o.B / 2)) { return true; }

            double cornerDistancesq = (circleDistancex - o.A / 2) * (circleDistancex - o.A / 2) +
                                 (circleDistancey - o.B / 2) * (circleDistancey - o.B / 2);

            return (cornerDistancesq <= (b.Radius * b.Radius));
        }

        public void MoveEnemies()
        {
            foreach (Enemy e in Enemies)
                e.MoveDown(3);
        }
        public void MoveBullets()
        {
            foreach (Bullet b in Bullets)
                b.MoveUp(5);
            foreach (Bullet b in BossBullets)
                b.MoveDown(5);
        }
        public void MoveObstacles()
        {
            foreach (Obstacle o in Obstacles)
            {
                o.MoveDown(6);
            }
        }

        public void MoveBoss()
        {
            Random r = new Random();
            if (Direction == "Left")
                Boss.MoveLeft(8);
            else
                Boss.MoveRight(8);
            if (r.Next(0, 2) == 0)
            {
                Boss.Enlarge(5);
            }
            else
            {
                Boss.Shrink(5);
            }
            
        }
        public void ChangeDirection()
        {
            Random r = new Random();
            if (r.Next(0, 2) == 0)
            {
                Direction = "Left";
            }
            else
            {
                Direction = "Right";
            }
        }
        public void Clear()
        {
            Obstacles = new List<Obstacle>();
            Bullets = new List<Bullet>();
            Strawberry = null;
            Enemies = new List<Enemy>();
        }
    }
}
