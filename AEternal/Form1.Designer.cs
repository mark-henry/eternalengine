namespace AEternal
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
           this.components = new System.ComponentModel.Container();
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
           this.trackBar1 = new System.Windows.Forms.TrackBar();
           this.label1 = new System.Windows.Forms.Label();
           this.label2 = new System.Windows.Forms.Label();
           this.panel1 = new System.Windows.Forms.Panel();
           this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
           this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
           this.label7 = new System.Windows.Forms.Label();
           this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
           this.label6 = new System.Windows.Forms.Label();
           this.comboBox2 = new System.Windows.Forms.ComboBox();
           this.comboBox1 = new System.Windows.Forms.ComboBox();
           this.label5 = new System.Windows.Forms.Label();
           this.label4 = new System.Windows.Forms.Label();
           this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
           this.button5 = new System.Windows.Forms.Button();
           this.button4 = new System.Windows.Forms.Button();
           this.button3 = new System.Windows.Forms.Button();
           this.label3 = new System.Windows.Forms.Label();
           this.button2 = new System.Windows.Forms.Button();
           this.button1 = new System.Windows.Forms.Button();
           this.timer1 = new System.Windows.Forms.Timer(this.components);
           ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
           this.panel1.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
           this.SuspendLayout();
           // 
           // trackBar1
           // 
           this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.trackBar1.Location = new System.Drawing.Point(9, 20);
           this.trackBar1.Maximum = 100;
           this.trackBar1.Name = "trackBar1";
           this.trackBar1.Size = new System.Drawing.Size(981, 45);
           this.trackBar1.TabIndex = 0;
           this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
           // 
           // label1
           // 
           this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(9, 52);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(68, 13);
           this.label1.TabIndex = 1;
           this.label1.Tag = "frame X/Y";
           this.label1.Text = "Frame 0/100";
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(12, 6);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(81, 13);
           this.label2.TabIndex = 2;
           this.label2.Tag = "# of Keyframes:";
           this.label2.Text = "# of Keyframes:";
           // 
           // panel1
           // 
           this.panel1.AutoSize = true;
           this.panel1.Controls.Add(this.numericUpDown3);
           this.panel1.Controls.Add(this.numericUpDown2);
           this.panel1.Controls.Add(this.label7);
           this.panel1.Controls.Add(this.numericUpDown1);
           this.panel1.Controls.Add(this.label6);
           this.panel1.Controls.Add(this.comboBox2);
           this.panel1.Controls.Add(this.comboBox1);
           this.panel1.Controls.Add(this.label5);
           this.panel1.Controls.Add(this.label4);
           this.panel1.Controls.Add(this.maskedTextBox2);
           this.panel1.Controls.Add(this.button5);
           this.panel1.Controls.Add(this.button4);
           this.panel1.Controls.Add(this.button3);
           this.panel1.Controls.Add(this.label3);
           this.panel1.Controls.Add(this.button2);
           this.panel1.Controls.Add(this.button1);
           this.panel1.Controls.Add(this.label2);
           this.panel1.Controls.Add(this.label1);
           this.panel1.Controls.Add(this.trackBar1);
           this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
           this.panel1.Location = new System.Drawing.Point(0, 498);
           this.panel1.Name = "panel1";
           this.panel1.Size = new System.Drawing.Size(1002, 68);
           this.panel1.TabIndex = 8;
           // 
           // numericUpDown3
           // 
           this.numericUpDown3.Location = new System.Drawing.Point(91, 4);
           this.numericUpDown3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
           this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
           this.numericUpDown3.Name = "numericUpDown3";
           this.numericUpDown3.Size = new System.Drawing.Size(40, 20);
           this.numericUpDown3.TabIndex = 23;
           this.numericUpDown3.Tag = "# keyframes";
           this.numericUpDown3.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
           this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
           // 
           // numericUpDown2
           // 
           this.numericUpDown2.DecimalPlaces = 1;
           this.numericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
           this.numericUpDown2.Location = new System.Drawing.Point(369, 4);
           this.numericUpDown2.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
           this.numericUpDown2.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
           this.numericUpDown2.Name = "numericUpDown2";
           this.numericUpDown2.Size = new System.Drawing.Size(36, 20);
           this.numericUpDown2.TabIndex = 22;
           this.numericUpDown2.Tag = "y-displacement";
           // 
           // label7
           // 
           this.label7.AutoSize = true;
           this.label7.Location = new System.Drawing.Point(354, 6);
           this.label7.Name = "label7";
           this.label7.Size = new System.Drawing.Size(17, 13);
           this.label7.TabIndex = 21;
           this.label7.Text = "Y:";
           // 
           // numericUpDown1
           // 
           this.numericUpDown1.DecimalPlaces = 1;
           this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
           this.numericUpDown1.Location = new System.Drawing.Point(317, 4);
           this.numericUpDown1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
           this.numericUpDown1.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
           this.numericUpDown1.Name = "numericUpDown1";
           this.numericUpDown1.Size = new System.Drawing.Size(36, 20);
           this.numericUpDown1.TabIndex = 20;
           this.numericUpDown1.Tag = "x-displacement";
           // 
           // label6
           // 
           this.label6.AutoSize = true;
           this.label6.Location = new System.Drawing.Point(233, 6);
           this.label6.Name = "label6";
           this.label6.Size = new System.Drawing.Size(87, 13);
           this.label6.TabIndex = 19;
           this.label6.Text = "Displacement: X:";
           // 
           // comboBox2
           // 
           this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.comboBox2.FormattingEnabled = true;
           this.comboBox2.Items.AddRange(new object[] {
            "Move Vertex",
            "New Vertex",
            "Link Vertices with Line",
            "Delete Vertex"});
           this.comboBox2.Location = new System.Drawing.Point(856, 3);
           this.comboBox2.Name = "comboBox2";
           this.comboBox2.Size = new System.Drawing.Size(134, 21);
           this.comboBox2.TabIndex = 18;
           this.comboBox2.Tag = "tool";
           this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
           // 
           // comboBox1
           // 
           this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.comboBox1.FormattingEnabled = true;
           this.comboBox1.Items.AddRange(new object[] {
            "Animator",
            "Modeler"});
           this.comboBox1.Location = new System.Drawing.Point(742, 3);
           this.comboBox1.MaxDropDownItems = 2;
           this.comboBox1.Name = "comboBox1";
           this.comboBox1.Size = new System.Drawing.Size(80, 21);
           this.comboBox1.TabIndex = 15;
           this.comboBox1.Tag = "mode";
           this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
           // 
           // label5
           // 
           this.label5.AutoSize = true;
           this.label5.Location = new System.Drawing.Point(828, 6);
           this.label5.Name = "label5";
           this.label5.Size = new System.Drawing.Size(31, 13);
           this.label5.TabIndex = 17;
           this.label5.Text = "Tool:";
           // 
           // label4
           // 
           this.label4.AutoSize = true;
           this.label4.Location = new System.Drawing.Point(708, 6);
           this.label4.Name = "label4";
           this.label4.Size = new System.Drawing.Size(37, 13);
           this.label4.TabIndex = 16;
           this.label4.Text = "Mode:";
           // 
           // maskedTextBox2
           // 
           this.maskedTextBox2.Location = new System.Drawing.Point(523, 3);
           this.maskedTextBox2.Mask = "0000";
           this.maskedTextBox2.Name = "maskedTextBox2";
           this.maskedTextBox2.PromptChar = ' ';
           this.maskedTextBox2.Size = new System.Drawing.Size(25, 20);
           this.maskedTextBox2.TabIndex = 14;
           this.maskedTextBox2.Tag = "play speed";
           this.maskedTextBox2.Text = "100";
           this.maskedTextBox2.TextChanged += new System.EventHandler(this.maskedTextBox2_TextChanged);
           // 
           // button5
           // 
           this.button5.Location = new System.Drawing.Point(597, 1);
           this.button5.Name = "button5";
           this.button5.Size = new System.Drawing.Size(50, 23);
           this.button5.TabIndex = 11;
           this.button5.Tag = "open...";
           this.button5.Text = "&Open...";
           this.button5.UseVisualStyleBackColor = true;
           this.button5.Click += new System.EventHandler(this.button5_Click);
           // 
           // button4
           // 
           this.button4.Location = new System.Drawing.Point(653, 1);
           this.button4.Name = "button4";
           this.button4.Size = new System.Drawing.Size(49, 23);
           this.button4.TabIndex = 10;
           this.button4.Tag = "save...";
           this.button4.Text = "&Save...";
           this.button4.UseVisualStyleBackColor = true;
           this.button4.Click += new System.EventHandler(this.button4_Click);
           // 
           // button3
           // 
           this.button3.Location = new System.Drawing.Point(554, 1);
           this.button3.Name = "button3";
           this.button3.Size = new System.Drawing.Size(37, 23);
           this.button3.TabIndex = 9;
           this.button3.Tag = "stop";
           this.button3.Text = "&Stop";
           this.button3.UseVisualStyleBackColor = true;
           this.button3.Click += new System.EventHandler(this.button3_Click);
           // 
           // label3
           // 
           this.label3.AutoSize = true;
           this.label3.Location = new System.Drawing.Point(462, 6);
           this.label3.Name = "label3";
           this.label3.Size = new System.Drawing.Size(64, 13);
           this.label3.TabIndex = 13;
           this.label3.Text = "Play Speed:";
           // 
           // button2
           // 
           this.button2.Location = new System.Drawing.Point(411, 1);
           this.button2.Name = "button2";
           this.button2.Size = new System.Drawing.Size(45, 23);
           this.button2.TabIndex = 8;
           this.button2.Tag = "play";
           this.button2.Text = "&Play";
           this.button2.UseVisualStyleBackColor = true;
           this.button2.Click += new System.EventHandler(this.button2_Click);
           // 
           // button1
           // 
           this.button1.Enabled = false;
           this.button1.Location = new System.Drawing.Point(137, 1);
           this.button1.Name = "button1";
           this.button1.Size = new System.Drawing.Size(93, 23);
           this.button1.TabIndex = 7;
           this.button1.Tag = "create keyframe";
           this.button1.Text = "Create &Keyframe";
           this.button1.UseVisualStyleBackColor = true;
           this.button1.Click += new System.EventHandler(this.button1_Click);
           // 
           // timer1
           // 
           this.timer1.Interval = 16;
           this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
           // 
           // Form1
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size(1002, 566);
           this.Controls.Add(this.panel1);
           this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
           this.Name = "Form1";
           this.Load += new System.EventHandler(this.Form1_Load);
           this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
           this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
           this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
           this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
           ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
           this.panel1.ResumeLayout(false);
           this.panel1.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
           this.ResumeLayout(false);
           this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
    }
}

