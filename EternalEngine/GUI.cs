using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EternalEngine
{
    public class GUI
    {
        public GUI()
        {
            Stamina = 100;
        }

        public void Refresh(Graphics g)
        {
            //Stamina bar
            g.DrawLine(new Pen(Brushes.Red, 5f), 5, 5, Stamina + 5, 5);
        }

        public float Stamina { get; set; }
        public bool Paused { get; set; }
    }
}
