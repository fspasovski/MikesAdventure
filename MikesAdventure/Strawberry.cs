using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MikesAdventure
{
    [Serializable]
    public class Strawberry
    {
        public Point Point { get; set; }
        public int Radius = 4;

        public Strawberry(Point p)
        {
            Point = p;
        }

        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.MediumVioletRed);
            g.FillEllipse(b,Point.X - Radius, Point.Y - Radius, 2 * Radius, 2 * Radius);
            b.Dispose();
        }
    }
}
