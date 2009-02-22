using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace EternalEngine
{
    public class GUI
    {
        public GUI(Size clientsize)
        {
            m_inv = new Region();
            m_inv.MakeEmpty();
            Stamina = 1;
            ClientSize = clientsize;

            TopLevelMenuItem = new MenuItem("Paused");
            new MenuItem(TopLevelMenuItem, "Save");
            new MenuItem(TopLevelMenuItem, "Load");
            new MenuItem(TopLevelMenuItem, "Options");
            new MenuItem(TopLevelMenuItem, "Exit");
            CurrentMenuItem = TopLevelMenuItem;
            m_selectedMenuIndex = 0;

            ftitle = new Font("Lucida Console", 36, FontStyle.Regular);
            fsmall = new Font("Lucida Console", 20, FontStyle.Regular);

        }

        private Font ftitle;
        private Font fsmall;

        private int m_selectedMenuIndex;

        public Size ClientSize { get; set; }

        public void Draw(Graphics g)
        {
            if (IsPaused)
            {
                g.FillRegion(Brushes.Black, new Region());

                g.DrawString(CurrentMenuItem.Text, ftitle, Brushes.White, MenuItem.LeftMargin, MenuItem.TopMargin);
                for (int i = 0; i <= CurrentMenuItem.Children.Count; i++)
                {
                    if (i == CurrentMenuItem.Children.Count)
                    {
                        g.DrawString("Back", fsmall, Brushes.White, MenuItem.LeftMargin + MenuItem.TabStop,
                            i * MenuItem.VerticalInterval + MenuItem.TopMargin + MenuItem.TitleOffset);
                        break;
                    }
                    g.DrawString(CurrentMenuItem.Children[i].Text, fsmall, Brushes.White, MenuItem.LeftMargin + MenuItem.TabStop,
                        i * MenuItem.VerticalInterval + MenuItem.TopMargin + MenuItem.TitleOffset);
                }
                g.DrawString(CurrentMenuItem.Children[m_selectedMenuIndex].Text, fsmall, Brushes.Yellow,
                    MenuItem.LeftMargin + MenuItem.TabStop, MenuItem.TopMargin + MenuItem.TitleOffset + MenuItem.VerticalInterval * m_selectedMenuIndex);
            }
            else
            {
                //Stamina bar
                string s = "Stamina";
                Font f = new Font(FontFamily.GenericSerif, 10, FontStyle.Bold);
                float w = g.MeasureString(s, f).Width;
                g.DrawLine(new Pen(Brushes.Red, 5f), w, 7, Stamina * (ClientSize.Width - w), 7);
                g.DrawString(s, f, Brushes.Red, 0f, 0f);
            }
        }

        public void OnClick(object sender, Point click, int mousebutton)
        {
            if (m_ispaused)
            {
                switch (mousebutton)
                {
                    case 1048576: //Left-click
                        CurrentMenuItem.Children[m_selectedMenuIndex].OnClicked(this);
                        break;
                    case 2097152: //Right-click
                        break;
                }

            }
        }

        public void MouseMove(Point loc)
        {
            Rectangle r = new Rectangle(MenuItem.LeftMargin + MenuItem.TabStop, MenuItem.TopMargin + MenuItem.TitleOffset,
                ClientSize.Width, MenuItem.VerticalInterval);
            foreach (MenuItem mi in CurrentMenuItem.Children)
            {
                r.Offset(0, MenuItem.VerticalInterval);
                if (r.Contains(loc)) { m_inv.Union(r); m_selectedMenuIndex = CurrentMenuItem.Children.IndexOf(mi); }
            }
        }

        private Region m_inv;
        /// <summary>
        /// Gets and then clears the accumulated invalidated area.
        /// </summary>
        public Region GetInvalidatedRegion()
        {
            Region ret = m_inv.Clone();
            m_inv.MakeEmpty();
            return ret;
        }

        public MenuItem TopLevelMenuItem { get; set; }
        public MenuItem CurrentMenuItem { get; set; }

        private float m_stamina;
        public float Stamina
        {
            get { return m_stamina; }
            set
            {
                m_stamina = value;
                m_inv.Union(new Rectangle(0, 0, ClientSize.Width, 15));
            }
        }

        private bool m_ispaused;
        public bool IsPaused { get { return m_ispaused; } }

        public bool PauseToggle()
        {
            CurrentMenuItem = TopLevelMenuItem;
            if (m_ispaused)
            {
                m_inv = new Region(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
                m_inv.Union(new Rectangle(0, ClientSize.Height - 15, ClientSize.Width, 15));
                UnPause(this, new EventArgs());
            }
            else { m_inv = new Region(); }
            return m_ispaused = !m_ispaused;
        }
        public event EventHandler UnPause;
    }

    public class MenuItem
    {
        public const int LeftMargin = 50;
        public const int TabStop = 25;
        public const int TopMargin = 50;
        public const int VerticalInterval = 75;
        public const int TitleOffset = 30;

        public MenuItem(MenuItem parentmenu, string text)
        {
            Parent = parentmenu;
            Children = new List<MenuItem>();
            if (!parentmenu.Children.Contains(this))
            {
                parentmenu.Children.Add(this);
            }
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

        public bool IsOrphan { get { if (Parent == null) { return true; } else { return false; } } }

        public void OnClicked(object sender) { Clicked(sender, new EventArgs()); }
        public event EventHandler Clicked;
    }
}
