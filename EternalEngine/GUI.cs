using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EternalEngine
{
    public class GUI
    {
        public GUI(Size clientsize)
        {
            Stamina = 1;
            ClientSize = clientsize;
            TopLevelMenuItem = new MenuItem("Paused");
            TopLevelMenuItem.Children.Add(new MenuItem(TopLevelMenuItem, "asdf"));
            TopLevelMenuItem.Children.Add(new MenuItem(TopLevelMenuItem, "asdf2"));
            TopLevelMenuItem.Children.Add(new MenuItem(TopLevelMenuItem, "asdf3"));
        }

        public Size ClientSize { get; set; }

        public void Draw(Graphics g)
        {
            if (IsPaused)
            {
                Font ftitle = new Font("Lucida Console", 36, FontStyle.Regular);
                Font fsmall = new Font("Lucida Console", 20, FontStyle.Regular);
                g.DrawString(CurrentMenuItem.Text, ftitle, Brushes.Black, 50, 50);
            }
            else
            {
                g.FillRegion(Brushes.LightGray, this.GetGUIBounds());
                //Stamina bar
                string s = "Stamina";
                Font f = new Font(FontFamily.GenericSerif, 10, FontStyle.Bold);
                float w = g.MeasureString(s, f).Width;
                g.DrawLine(new Pen(Brushes.Red, 5f), w, 7, Stamina * (ClientSize.Width - w), 7);
                g.DrawString(s, f, Brushes.Red, 0f, 0f);
            }
        }

        public void Click(Point click)
        {

        }

        public void MouseMove(Point loc)
        {
        }

        public Region GetGUIBounds()
        {
            if (IsPaused)
            {
                return new Region();
            }
            else
            {
                Region ret = new Region(new Rectangle(0, 0, ClientSize.Width, 15));
                ret.Union(new Rectangle(0, ClientSize.Height - 15, ClientSize.Width, 15));
                return ret;
            }
        }

        public MenuItem TopLevelMenuItem { get; set; }
        public MenuItem CurrentMenuItem { get; set; }

        public float Stamina { get; set; }
        private bool m_ispaused;
        public bool IsPaused { get { return m_ispaused; } }

        public void Pause()
        {
            m_ispaused = true;
            CurrentMenuItem = TopLevelMenuItem;
        }
        public event EventHandler UnPause;
    }

    public class MenuItem
    {
        public MenuItem(MenuItem parentmenu, string text)
        {
            Parent = parentmenu;
            Children = new List<MenuItem>();
            parentmenu.Children.Add(this);
            Text = text;
        }
        public MenuItem(string text)
        {
            Children = new List<MenuItem>();
            Text = text;
        }

        public MenuItem Parent { get; set; }
        public List<MenuItem> Children { get; set; }

        public string Text { get; set; }
    }
}
