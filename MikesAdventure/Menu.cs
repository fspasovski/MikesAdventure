using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace MikesAdventure
{
    public partial class Menu : Form
    {
        public Color HeadColor { get; set; }
        public Color EyesColor { get; set; }
        public Menu()
        {
            HeadColor = Color.Red;
            EyesColor = Color.Black;
            InitializeComponent();
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = ".//.//.//Sounds////song.wav";
            sp.Load();
            sp.PlayLooping();
            ddlHeadColor.Items.Add(Color.Red);
            ddlHeadColor.Items.Add(Color.Blue);
            ddlHeadColor.Items.Add(Color.Yellow);
            ddlHeadColor.Items.Add(Color.Purple);

            ddlEyesColor.Items.Add(Color.Black);
            ddlEyesColor.Items.Add(Color.Blue);
            ddlEyesColor.Items.Add(Color.Green);
            ddlEyesColor.Items.Add(Color.Brown);

            ddlHeadColor.SelectedIndex = 0;
            ddlEyesColor.SelectedIndex = 0;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            View view = new View(this.HeadColor, this.EyesColor, checkBox1.Checked);
            view.ShowDialog();
        }

        private void ddlEyesColor_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ddlEyesColor.SelectedItem != null)
            {
                EyesColor = (Color)ddlEyesColor.SelectedItem;
            }
        }

        private void ddlHeadColor_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ddlHeadColor.SelectedItem != null)
            {
                HeadColor = (Color)ddlHeadColor.SelectedItem;
            }
        }


        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.PaleVioletRed;
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Silver;
            button1.ForeColor = Color.Black;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Instructions ins = new Instructions();
            ins.ShowDialog();
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.PaleVioletRed;
            button2.ForeColor = Color.White;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Silver;
            button2.ForeColor = Color.Black;
        }


    }
}
