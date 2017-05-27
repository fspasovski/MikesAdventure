using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MikesAdventure
{
    [Serializable]
    public class Obstacle
    {
        public int A { get; set; } // width
        public int B { get; set; } // height
        public bool Worth { get; set; } // is it worth points
        public Point Point {get; set; } // position
        public Color Color { get; set; } // color of the obstacle
        public string Type { get; set; } // type of the obstacle

        public Obstacle(Point p, string type, Color color, int a, int b) 
        {
            A = a;
            B = b;
            Worth = true;
            Point = p;
            Color = color;
            Type = type;
        }

        public void Draw(Graphics g)
        {
                Brush b = new SolidBrush(Color);
                g.FillRectangle(b, Point.X - (A / 2), Point.Y - (B / 2), A, B);
                b.Dispose();   
        }
        
        
        public void MoveDown(int d)
        {
            Point = new Point(Point.X, Point.Y + d);
        }
    }
}
