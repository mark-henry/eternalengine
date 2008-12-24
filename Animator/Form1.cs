﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EternalEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace Aeternal
{
    public partial class Form1 : Form
    {
        Camera cam = new Camera(new PointF(0, 0));
        enum AnimatorModes { Animator, Modeler }
        AnimatorModes Mode;
        AeternalEntity currentfile = new AeternalEntity();
        enum ModelerTools { MoveVertex, NewVertex, LinkVerticesWithLine, DeleteVertex }
        ModelerTools CurrentTool;
        int linkstep = 1;
        int firstlink;
        int secondlink;
        bool success;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            currentfile.Animation = new Animation();
            currentfile.FillColor = Brushes.Olive;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (trackBar1.Value < trackBar1.Maximum)
            {
                trackBar1.Value++;
            }
            else { trackBar1.Value = 0; }
        }

        //Save
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svdlg = new SaveFileDialog();
                svdlg.DefaultExt = "xml";
                svdlg.Filter = "XML files (*.xml)|*.xml";
                svdlg.ShowDialog();
                Stream file = svdlg.OpenFile();
                SoapFormatter sf = new SoapFormatter();
                sf.Serialize(file, currentfile);
                file.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Fail!!"); }
        }

        //Open
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opendlg = new OpenFileDialog();
                opendlg.DefaultExt = "xml";
                opendlg.Filter = "XML files (*.xml)|*.xml";
                opendlg.Multiselect = false;
                opendlg.ShowDialog();
                Stream file = opendlg.OpenFile();
                SoapFormatter sf = new SoapFormatter();
                currentfile = (AeternalEntity)sf.Deserialize(file);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        //Play/Pause
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Play")
            {
                button2.Text = "Pause";
                timer1.Enabled = true;
            }
            else
            {
                button2.Text = "Play";
                timer1.Enabled = false;
            }
        }

        //Speed
        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox2.Text != "" && Int32.Parse(maskedTextBox2.Text) != 0)
            {
                timer1.Interval = (int)(100 / (float)Int32.Parse(maskedTextBox2.Text) * 16);
            }
        }

        //Stop
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            button2.Text = "Play";
            trackBar1.Value = 0;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = "Frame " + trackBar1.Value.ToString() + "/" + trackBar1.Maximum.ToString();
            currentfile.Animation.CurrentFrame = trackBar1.Value;
            currentfile.Animation.GoTo(currentfile.Vertices, trackBar1.Value);
            Invalidate();
        }

        //Create Keyframe
        private void button1_Click(object sender, EventArgs e)
        {
            if (currentfile.Animation.Keyframes.ContainsKey(trackBar1.Value))
            {
                currentfile.Animation.Keyframes[trackBar1.Value] = new Keyframe(currentfile.Vertices);
            }
            else
            {
                currentfile.Animation.Keyframes.Add(trackBar1.Value, new Keyframe(currentfile.Vertices));
            }
        }

        //Mode
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Mode = AnimatorModes.Animator;
                    BuildTitleText();
                    comboBox2.SelectedIndex = 0;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    numericUpDown1.Enabled = true;
                    numericUpDown2.Enabled = true;
                    numericUpDown3.Enabled = true;
                    maskedTextBox2.Enabled = true;
                    comboBox2.Enabled = false;
                    label1.Enabled = true;
                    label2.Enabled = true;
                    label3.Enabled = true;
                    label5.Enabled = false;
                    label6.Enabled = true;
                    label7.Enabled = true;
                    trackBar1.Enabled = true;
                    break;
                case 1:
                    Mode = AnimatorModes.Modeler;
                    BuildTitleText();
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    numericUpDown1.Enabled = false;
                    numericUpDown2.Enabled = false;
                    numericUpDown3.Enabled = false;
                    maskedTextBox2.Enabled = false;
                    comboBox2.Enabled = true;
                    label1.Enabled = false;
                    label2.Enabled = false;
                    label3.Enabled = false;
                    label5.Enabled = true;
                    label6.Enabled = false;
                    label7.Enabled = false;
                    trackBar1.Enabled = false;
                    break;
            }
        }

        private void BuildTitleText()
        {
            if (currentfile.ModelName != null)
            {
                this.Text = "Aeternal - " + Mode.ToString() + " Mode - " + currentfile.ModelName;
            }
            else
            {
                this.Text = "Aeternal - " + Mode.ToString() + " Mode";
            }
        }

        //Tool
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    CurrentTool = ModelerTools.MoveVertex;
                    break;
                case 1:
                    CurrentTool = ModelerTools.NewVertex;
                    break;
                case 2:
                    CurrentTool = ModelerTools.LinkVerticesWithLine;
                    break;
                case 3:
                    CurrentTool = ModelerTools.DeleteVertex;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (Line l in currentfile.Lines)
            {
                g.DrawLine(new Pen(l.Color, l.Width), WorldtoScreen(currentfile.Vertices[l.Index1].Location), WorldtoScreen(currentfile.Vertices[l.Index2].Location));
            }
            foreach (Vertex v in currentfile.Vertices)
            {
                g.DrawEllipse(new Pen(Color.Black, 2), WorldtoScreen(v.Location).X - .5f, WorldtoScreen(v.Location).Y - .5f, 1, 1);
            }
            if (currentfile.SelectedVertex != -1)
            {
                PointF sv = WorldtoScreen(currentfile.Vertices[currentfile.SelectedVertex].Location);
                g.DrawEllipse(new Pen(Color.Red), new RectangleF(sv.X - 1.5f, sv.Y - 1.5f, 3, 3));
                g.DrawString(currentfile.Vertices[currentfile.SelectedVertex].Location.ToString(),
                    new Font("Agency FB", 9), Brushes.Black, sv.X + 12, sv.Y + 10);
            }
            //if (currentfile.Animation.NextKey == null)
            //{
            //    g.DrawString("nextkey null", new Font("Agency FB", 9),
            //        Brushes.Black, 0, 20);
            //}
            //else
            //{
            //    g.DrawString(currentfile.Animation.NextKey.ToString(), new Font("Agency FB", 9),
            //        Brushes.Black, 0, 20);
            //}
            //if (currentfile.Animation.PreviousKey == null)
            //{
            //    g.DrawString("prevkey null", new Font("Agency FB", 9),
            //        Brushes.Black, 0, 0);
            //}
            //else
            //{
            //    g.DrawString(currentfile.Animation.PreviousKey.ToString(), new Font("Agency FB", 9),
            //        Brushes.Black, 0, 0);
            //}
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Maximum = (int)numericUpDown3.Value;
            label1.Text = "Frame " + trackBar1.Value + "/" + trackBar1.Maximum;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Mode == AnimatorModes.Modeler)
                {
                    switch (CurrentTool)
                    {
                        case ModelerTools.LinkVerticesWithLine:
                            if (success && linkstep == 1)
                            {
                                linkstep = 2;
                                firstlink = currentfile.SelectedVertex;
                            }
                            else if (success && linkstep == 2)
                            {
                                secondlink = currentfile.SelectedVertex;
                                currentfile.Lines.Add(new Line(firstlink, secondlink, Color.Green, 2));
                                linkstep = 1;
                            }
                            Invalidate();
                            break;
                        case ModelerTools.NewVertex:
                            Vertex newv = new Vertex(ScreenToWorld((PointF)e.Location));
                            currentfile.Vertices.Add(newv);
                            Invalidate();
                            break;
                        case ModelerTools.DeleteVertex:
                            if (success)
                            {
                                currentfile.Vertices.RemoveAt(currentfile.SelectedVertex);
                                Line[] lines = new Line[currentfile.Lines.Count];
                                currentfile.Lines.CopyTo(lines);
                                foreach (Line l in lines)
                                {
                                    if (l.Index1 == currentfile.SelectedVertex)
                                        currentfile.Lines.Remove(l);
                                    if (l.Index2 == currentfile.SelectedVertex)
                                        currentfile.Lines.Remove(l);
                                }
                                currentfile.SelectedVertex = -1;
                                Invalidate();
                            }
                            break;
                    }
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentTool == ModelerTools.MoveVertex && e.Button == MouseButtons.Left && success)
            {
                currentfile.Vertices[currentfile.SelectedVertex].Location = ScreenToWorld((PointF)e.Location);
                Invalidate();
            }

            RectangleF hit = new RectangleF(ScreenToWorld((PointF)e.Location).X - 1.5f, ScreenToWorld((PointF)e.Location).Y - 1.5f, 3, 3);
            int ii = 0;
            success = false;
            foreach (Vertex v in currentfile.Vertices)
            {
                if (hit.Contains((int)v.LocationX, (int)v.LocationY))
                {
                    success = true;
                    currentfile.SelectedVertex = ii;
                    Invalidate();
                }
                ii++;
            }
            if (!success) { currentfile.SelectedVertex = -1; Invalidate(); }
        }

        public PointF ScreenToWorld(PointF p)
        {
            PointF retp = new PointF(p.X, p.Y);
            retp.X = p.X + cam.Location.X - (this.Width / 2);
            retp.Y = p.Y + cam.Location.Y - ((this.Height - 75) / 2);
            return retp;
        }

        public PointF WorldtoScreen(PointF p)
        {
            PointF retp = new PointF(p.X, p.Y);
            retp.X = p.X - cam.Location.X + (this.Width / 2);
            retp.Y = p.Y - cam.Location.Y + ((this.Height - 75) / 2);
            return retp;
        }
    }
}
