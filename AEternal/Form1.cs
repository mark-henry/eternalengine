﻿using System;
using System.Drawing;
using System.Windows.Forms;
using EternalEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Diagnostics;

namespace AEternal
{
   public partial class Form1 : Form
   {
      Camera cam;
      enum AnimatorModes { Animator, Modeler }
      AnimatorModes Mode;
      ActorEntity currentfile = new ActorEntity();
      enum ModelerTools { MoveVertex, NewVertex, LinkVerticesWithLine, DeleteVertex }
      ModelerTools CurrentTool;
      int linkstep = 1;
      int firstlink;
      int secondlink;
      int selectedvertex = -1;

      public Form1()
      {
         InitializeComponent();
         cam = new Camera(new PointF(0, 0), this.ClientSize);
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         currentfile.Animation = new Animation();
         currentfile.Material = Material.Steel;
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

      private void button1_Click(object sender, EventArgs e) //Create Keyframe 
      {
         if (currentfile.Animation.Keyframes.ContainsKey(trackBar1.Value))
         {
            currentfile.Animation.Keyframes.Remove(trackBar1.Value);
            button1.Text = "Create Keyframe";
         }
         else
         {
            currentfile.Animation.Keyframes.Add(trackBar1.Value, new Keyframe(currentfile.Vertices));
            button1.Text = "Delete &Keyframe";
         }
         Invalidate(new Rectangle(0, 0, 75, this.Height));
      }

      private void button2_Click(object sender, EventArgs e) //Play/Pause 
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

      private void button3_Click(object sender, EventArgs e) //Stop 
      {
         timer1.Enabled = false;
         button2.Text = "Play";
         trackBar1.Value = 0;
      }

      private void button4_Click(object sender, EventArgs e) //Save 
      {
         try
         {
            SaveFileDialog svdlg = new SaveFileDialog();
            svdlg.DefaultExt = "eem";
            svdlg.Filter = "Eternal Engine Model (*.eem)|*.eem";
            if (svdlg.ShowDialog() != DialogResult.Cancel)
            {
               Stream file = svdlg.OpenFile();
               BinaryFormatter bf = new BinaryFormatter();
               bf.Serialize(file, currentfile);
               file.Close();
            }
         }
         catch (Exception ex) { MessageBox.Show(ex.Message, "Fail!!"); }
      }

      private void button5_Click(object sender, EventArgs e) //Open 
      {            
         OpenFileDialog opendlg = new OpenFileDialog();
         try
         {
            opendlg.DefaultExt = "eem";
            opendlg.Filter = "Eternal Engine Model (*.eem)|*.eem";
            opendlg.Multiselect = false;
            if (opendlg.ShowDialog() != DialogResult.Cancel)
            {
               Stream file = opendlg.OpenFile();
               BinaryFormatter bf = new BinaryFormatter();
               currentfile = (ActorEntity)bf.Deserialize(file);
               file.Close();
               numericUpDown3.Value = currentfile.Animation.NumberofFrames;
               trackBar1.Value = 0;
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Fail!!");
         }
         Invalidate(new Rectangle(0, 0, 75, this.Height));
      }

      private void maskedTextBox2_TextChanged(object sender, EventArgs e) //Speed 
      {
         if (maskedTextBox2.Text != "" && Int32.Parse(maskedTextBox2.Text) != 0)
         {
            timer1.Interval = (int)(100 / (float)Int32.Parse(maskedTextBox2.Text) * 16);
         }
      }

      private void trackBar1_ValueChanged(object sender, EventArgs e) //Trackbar 
      {
         label1.Text = "Frame " + trackBar1.Value.ToString() + "/" + trackBar1.Maximum.ToString();
         if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift) //Shift supresses calculation
         {
            currentfile.Animation.GoTo(currentfile.Vertices, trackBar1.Value);
            Invalidate(new Rectangle(75, 0, this.Width - 75, this.Height));
         }
         if (currentfile.Animation.Keyframes.ContainsKey(trackBar1.Value))
         {
            button1.Text = "Delete Keyframe";
         }
         else { button1.Text = "Create Keyframe"; }
      }

      private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Mode 
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
               currentfile.Animation.GoTo(currentfile.Vertices, 0);
               break;
         }
      }
      private void BuildTitleText()
      {
         if (currentfile.ModelName != null)
         {
            this.Text = "AEternal - " + Mode.ToString() + " Mode - " + currentfile.ModelName;
         }
         else
         {
            this.Text = "AEternal - " + Mode.ToString() + " Mode";
         }
      }

      private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) //Tool 
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

      private void Form1_Paint(object sender, PaintEventArgs e) //Paint 
      {
         Graphics g = e.Graphics;
         g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
         foreach (Line l in currentfile.Lines) //Lines
         {
            g.DrawLine(new Pen(l.Color, l.Width), WorldtoScreen(currentfile.Vertices[l.Index1].Location), WorldtoScreen(currentfile.Vertices[l.Index2].Location));
         }
         foreach (Vertex v in currentfile.Vertices) //Vertices
         {
            g.DrawEllipse(new Pen(Color.Black, 2), WorldtoScreen(v.Location).X - .5f, WorldtoScreen(v.Location).Y - .5f, 1, 1);
         }
         if (selectedvertex != -1) //Selected gets a red circle
         {
            PointF sv = WorldtoScreen(currentfile.Vertices[selectedvertex].Location);
            g.DrawEllipse(new Pen(Color.Red), new RectangleF(sv.X - 1.5f, sv.Y - 1.5f, 3, 3));
            g.DrawString(currentfile.Vertices[selectedvertex].Location.ToString(),
                new Font("Agency FB", 9), Brushes.Black, sv.X + 12, sv.Y + 10);
         }
         string keylist = "Keyframes:\n";        //Keyframes list
         foreach (KeyValuePair<int, Keyframe> key in currentfile.Animation.Keyframes)
         {
            keylist += "Frame " + key.Key + "\n";
         }
         g.DrawString(keylist, new Font(FontFamily.GenericMonospace, 9), Brushes.Blue, 0, 0);

         g.DrawEllipse(new Pen(Color.Coral, 2), WorldtoScreen(currentfile.CenterofMass).X - .5f, WorldtoScreen(currentfile.CenterofMass).Y - .5f, 1, 1);
      }

      private void numericUpDown3_ValueChanged(object sender, EventArgs e) // # of Frames
      {
         trackBar1.Maximum = (int)numericUpDown3.Value;
         label1.Text = "Frame " + trackBar1.Value + "/" + trackBar1.Maximum;
      }

      private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left)
         {
            Drag(e);

            if (Mode == AnimatorModes.Modeler)
            {
               switch (CurrentTool)
               {
                  case ModelerTools.LinkVerticesWithLine:
                     if (SelectVertex(e))
                     {
                        if (linkstep == 1)
                        {
                           linkstep = 2;
                           firstlink = selectedvertex;
                        }
                        else
                        {
                           secondlink = selectedvertex;
                           if (firstlink != secondlink)
                           {
                              currentfile.Lines.Add(new Line(firstlink, secondlink, Color.Green, 2));
                              linkstep = 1;
                           }
                           else
                           {
                              Debug.WriteLine("Info: Selected same Vertex twice in linking with Line");
                           }
                        }

                     }
                     break;
                  case ModelerTools.NewVertex:
                     Vertex newv = new Vertex(ScreenToWorld((PointF)e.Location));
                     currentfile.Vertices.Add(newv);
                     break;
                  case ModelerTools.DeleteVertex:
                     if (IsDragging)
                     {
                        currentfile.Vertices.RemoveAt(selectedvertex);
                        Line[] lines = new Line[currentfile.Lines.Count];
                        currentfile.Lines.CopyTo(lines);
                        foreach (Line l in lines)
                        {
                           if (l.Index1 == selectedvertex)
                              currentfile.Lines.Remove(l);
                           if (l.Index2 == selectedvertex)
                              currentfile.Lines.Remove(l);
                        }
                        selectedvertex = -1;
                     }
                     break;
               }
            }
            Invalidate();
         }
      }

      private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         SelectVertex(e);
         if (selectedvertex != -1 && IsDragging)
         {
            currentfile.Vertices[selectedvertex].Location = ScreenToWorld((PointF)e.Location);
            Invalidate();
         }
      }

      private void Form1_MouseUp(object sender, MouseEventArgs e)
      {
         IsDragging = false;
         selectedvertex = -1;
         Invalidate();
      }

      private bool Drag(MouseEventArgs e)
      {
         RectangleF hit = new RectangleF(ScreenToWorld((PointF)e.Location).X - 1.5f, ScreenToWorld((PointF)e.Location).Y - 1.5f, 3, 3);
         int ii = 0;
         foreach (Vertex v in currentfile.Vertices)
         {
            if (hit.Contains((int)v.LocationX, (int)v.LocationY))
            {
               selectedvertex = ii;
               IsDragging = true;
               return true;
            }
            ii++;
         }
         selectedvertex = -1;
         return false;
      }

      private bool IsDragging { get; set; }

      private bool SelectVertex(MouseEventArgs e)
      {
         RectangleF hit = new RectangleF(ScreenToWorld((PointF)e.Location).X - 1.5f, ScreenToWorld((PointF)e.Location).Y - 1.5f, 3, 3);
         int ii = 0;
         foreach (Vertex v in currentfile.Vertices)
         {
            if (hit.Contains((int)v.LocationX, (int)v.LocationY))
            {
               selectedvertex = ii;
               return true;
            }
            ii++;
         }
         selectedvertex = -1;
         IsDragging = false;
         return false;
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

   /// <summary>
   /// Deprecated
   /// </summary>
   [Serializable]
   public class AEternalEntity : ActorEntity
   {
      public AEternalEntity()
      {
      }

      private int m_selectedvertex = -1;
      public int SelectedVertex
      {
         get { return m_selectedvertex; }
         set { if (value > this.Vertices.Count) { throw new ArgumentOutOfRangeException(); } m_selectedvertex = value; }
      }
   }

}