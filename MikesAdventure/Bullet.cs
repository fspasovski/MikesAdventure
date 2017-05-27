using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MikesAdventure
{
    [Serializable]
    public class Bullet
    {
        public Point Point { get; set; }
        public int Radius = 5;
        public Color Color { get; set; }

        public Bullet(Point p, Color c)
        {
            Point = p;
            Color = c;
        }

        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color);
            g.FillEllipse(b,Point.X - Radius, Point.Y - Radius, 2 * Radius, 2 * Radius);
            b.Dispose();
        }

        public void MoveUp(int d)
        {
            Point = new Point(Point.X, Point.Y - d);
        }
        public void MoveDown(int d)
        {
            Point = new Point(Point.X, Point.Y + d);
        }

    }
}
