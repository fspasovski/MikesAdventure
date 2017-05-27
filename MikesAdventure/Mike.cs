using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace MikesAdventure
{
    [Serializable]
    public class Mike
    {
        public static int WINDOW_HEIGHT = 519;
        public static int WINDOW_WIDTH = 1026;
        public static int MIN_SIZE = 10;
        public static int MAX_SIZE = 100;
        public String Type { get; set; }
        public Color EyeColor { get; set; }
        public Color HeadColor { get; set; }
        public int Radius { get; set; }
        public Point Point { get; set; }
        public string lastPoint { get; set; }

        public Mike(Point p, Color color, int radius, Color ec)
        {
            Radius = radius;
            EyeColor = ec;
            HeadColor = color;
            Point = p;
        }

        public void Draw(Graphics g)
        {
            Brush b1 = new SolidBrush(HeadColor); // glava
            Brush b2 = new SolidBrush(Color.White); // oci
            Brush b3 = new SolidBrush(EyeColor); // zenici
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
        public void MoveUp(int d)
        {
            if (Point.Y - 2 * Radius - d - 25 >=0)
            {
                    Point = new Point(Point.X, Point.Y - d);
            }
            
        }

        public void MoveDown(int d)
        {
            if (Point.Y + d < WINDOW_HEIGHT - 2*Radius - 35)
                Point = new Point(Point.X, Point.Y + d);
            
        }

        public void MoveLeft(int d)
        {
            if (Point.X - Radius - d > 0)
                Point = new Point(Point.X - d, Point.Y);
        }

        public void MoveRight(int d)
        {
            if (Point.X + Radius * 2 + d > WINDOW_WIDTH)
                Point = new Point(WINDOW_WIDTH - 2 * Radius, Point.Y);
            else
                Point = new Point(Point.X + d, Point.Y);
        }

        public void Enlarge(int d)
        {
            Radius += d;
            MoveRight(10);
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
