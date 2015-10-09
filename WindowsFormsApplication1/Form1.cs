using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<Int32> xkor = new List<Int32>();
            List<Int32> ykor = new List<Int32>();
            try
            {
                xkor = textBox1.Text.Split(',').Select(n => Convert.ToInt32(n)).ToList();
                ykor = textBox2.Text.Split(',').Select(n => Convert.ToInt32(n)).ToList();
            } catch (FormatException fe) {

            MessageBox.Show("Unijeli ste vrijednost koja nije cio broj!", "GREŠKA!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;           
            }

            label3.Text = "Unijete tačke: ";
            label4.Text = "Tačke omotača: ";

            List<Point> allPoints = new List <Point>();

            for (int i = 0; i < xkor.Count(); i++)
            {
                try
                {
                allPoints.Add(new Point(xkor[i], ykor[i]));
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Unijeli ste različit broj tačaka za X i Y koordinate!", "GREŠKA!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                    
                }
                if (i != xkor.Count()-1)
                {
                    label3.Text = label3.Text + xkor[i] + " " + ykor[i] + ", ";
                }
                else
                {
                    label3.Text = label3.Text + xkor[i] + " " + ykor[i];
                }
			}

           List<Point> ch = ConvexHull.CH2(allPoints);

            String CHPoints = "";
				for (int i = 0; i < ch.Count; i++){
                    if (i != ch.Count)
                    {				
					CHPoints=CHPoints+ch[i].X+" "+ch[i].Y+", ";
                    }else {
                        CHPoints = CHPoints + ch[i].X + " " + ch[i].Y;
                    }
					
				}
                label4.Text =label4.Text + CHPoints;
            
        }

       
    }
}
