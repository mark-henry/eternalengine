using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace WTF
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
         mouse = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
      }

      private MouseEventArgs mouse;
      float theta;

      private void Form1_MouseMove(object sender, MouseEventArgs e)
      {
         mouse = e;
         //Debug.WriteLine("1:" + Math.Atan2(-50, 0));
         Debug.WriteLine("2:" + Math.Atan2(e.Y - 150, e.X - 150));
         theta = (float)(Math.Atan2(50, 0) - Math.Atan2(e.Y - 150, e.X - 150)); //angle between push (straight up) and lever arm
         Invalidate();
      }

      private void Form1_Paint(object sender, PaintEventArgs e)
      {
         Graphics g = e.Graphics;

         g.DrawLine(new Pen(Brushes.Black), new Point(150, 150), mouse.Location);
         g.DrawLine(new Pen(Brushes.Blue), mouse.Location, new Point(mouse.X, mouse.Y - 50));

         //g.DrawString((mouse.X - 150).ToString(), new Font(FontFamily.GenericMonospace, 12f), Brushes.Black, new PointF(0, 0));
         //g.DrawString((mouse.Y - 150).ToString(), new Font(FontFamily.GenericMonospace, 12f), Brushes.Black, new PointF(0, 10));
         g.DrawString(Math.Atan2(mouse.Y - 150, mouse.X - 150).ToString(), new Font(FontFamily.GenericMonospace, 12f), Brushes.Black, new PointF(0, 20));
         g.DrawString("theta: " + theta, new Font(FontFamily.GenericMonospace, 12f), Brushes.Black, new PointF(0, 30));
         g.DrawString("cos theta: " + Math.Cos(theta), new Font(FontFamily.GenericMonospace, 12f), Brushes.Black, new PointF(0, 40));
      }
   }
}
