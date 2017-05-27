using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MikesAdventure
{
   
    [Serializable]
    public class Boss
    {
        public static int WINDOW_HEIGHT = 519;
        public static int WINDOW_WIDTH = 1026;
        public static int MIN_SIZE = 10;
        public static int MAX_SIZE = 100;
        public String Type { get; set; }
        public int Radius { get; set; }
        public Point Point { get; set; }
        public int HP { get; set; }

        public Boss(Point p,int radius)
        {
            Radius = radius;
            Point = p;
            HP = 100;
        }

        public void Draw(Graphics g)
        {
            Brush b1 = new SolidBrush(Color.Black); // glava
            Brush b2 = new SolidBrush(Color.Red); // oci
            Brush b4 = new SolidBrush(Color.Lavender); // usta
            g.FillEllipse(b1, Point.X - Radius, Point.Y - Radius, 2 * Radius, 2 * Radius);
            g.FillEllipse(b2, Point.X - Radius / 2, Point.Y - Radius / 2, Radius / 2, Radius / 2);
            g.FillEllipse(b2, Point.X, Point.Y - Radius / 2, Radius / 2, Radius / 2);
            g.FillEllipse(b4, Point.X - Radius / 2, Point.Y + Radius / 3, Radius, Radius / 2);
            b1.Dispose();
            b2.Dispose();
            b4.Dispose();
        }
        

        public void MoveLeft(int d)
        {
            if (Point.X -  2 * Radius - d > 0)
                Point = new Point(Point.X - d, Point.Y);
        }

        public void MoveRight(int d)
        {
            if (Point.X + Radius * 2 + d <= WINDOW_WIDTH)
                Point = new Point(Point.X + d, Point.Y);
        }

        public void Enlarge(int d)
        {
            Radius += d;
            if (Radius > MAX_SIZE)
                Radius = MAX_SIZE;
        }
        public void Shrink(int d)
        {
            Radius -= d;
            if (Radius < MIN_SIZE)
                Radius = MIN_SIZE;
                    
                
        }
    }
}
