using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace MikesAdventure
{
    [Serializable]
    public class Enemy
    {
        public static int WINDOW_HEIGHT = 519;
        public static int WINDOW_WIDTH = 1026;
        public static int MIN_SIZE = 10;
        public static int MAX_SIZE = 100;
        public int Radius { get; set; }
        public Point Point { get; set; }

        public Enemy(Point p, int radius)
        {
            Radius = radius;
            Point = p;
        }

        public void Draw(Graphics g)
        {
            Brush b1 = new SolidBrush(Color.Black); // glava
            Brush b2 = new SolidBrush(Color.White); // oci
            Brush b3 = new SolidBrush(Color.Black); // zenici
            Brush b4 = new SolidBrush(Color.Lavender); // usta
            Pen p1 = new Pen(Color.Black, 3); // kontura
            
            g.DrawEllipse(p1, Point.X - Radius, Point.Y - Radius, 2 * Radius, 2 * Radius);
            g.FillEllipse(b1, Point.X - Radius, Point.Y - Radius, 2 * Radius, 2 * Radius);
            g.FillEllipse(b2, Point.X - Radius / 2, Point.Y - Radius / 2, Radius / 2, Radius / 2);
            g.FillEllipse(b2, Point.X, Point.Y - Radius / 2, Radius / 2, Radius / 2);
            g.FillEllipse(b3, Point.X + Radius / 6, Point.Y - Radius / 4, Radius / 4, Radius / 4);
            g.FillEllipse(b3, Point.X - Radius / 3, Point.Y - Radius / 4, Radius / 4, Radius / 4);
            g.FillEllipse(b4, Point.X - Radius / 2, Point.Y + Radius / 3, Radius, Radius / 2);
            b1.Dispose();
            b2.Dispose();
            b3.Dispose();
            b4.Dispose();
            p1.Dispose();
        }
        public void MoveDown(int d)
        {
            if (Point.Y + d < 470)
                Point = new Point(Point.X, Point.Y + d);
            
            else Point = new Point(Point.X, Point.Y + d);
        }

        
    }
}
