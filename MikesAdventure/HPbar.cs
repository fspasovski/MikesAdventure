using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MikesAdventure
{
    [Serializable]
    public class HPbar
    {
        public int WINDOW_WIDTH = 1026;
        public int WINDOW_HEIGHT = 519;
        public int Value { get; set; }
        public Color Color { get; set; }
        public HPbar() 
        {
            Value = 100;
            Color = Color.Green;
        }
        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color);
            Brush b1 = new SolidBrush(Color.Black);
            g.FillRectangle(b,WINDOW_WIDTH - 250, WINDOW_HEIGHT - 100, Value * 2, 20);
            g.DrawString("Maiev HP: " + Value + "%", new Font("Arial", 8), b1, new Point(WINDOW_WIDTH - 200, WINDOW_HEIGHT - 97));
            b.Dispose();

        }
        public void Decrease()
        {
            Value -= 1;
            if (Value == 70)
                Color = Color.Yellow;
            else if (Value == 30)
                Color = Color.Red;
        }
        
    }
}
