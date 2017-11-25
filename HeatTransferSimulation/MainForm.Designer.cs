namespace HeatTransferSimulation
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSimulateTask = new System.Windows.Forms.Button();
            this.buttonStopSimulation = new System.Windows.Forms.Button();
            this.buttonSimulateWithThread = new System.Windows.Forms.Button();
            this.buttonSimulateWithThreadPool = new System.Windows.Forms.Button();
            this.buttonManualEmergency = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownHumans = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownParallelHorizontal = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownParallelVertical = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownSimulationLength = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownMultiplier = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHumans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownParallelHorizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownParallelVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSimulationLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMultiplier)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(13, 13);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(760, 460);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Simulation Stats";
            // 
            // buttonSimulateTask
            // 
            this.buttonSimulateTask.Location = new System.Drawing.Point(3, 6);
            this.buttonSimulateTask.Name = "buttonSimulateTask";
            this.buttonSimulateTask.Size = new System.Drawing.Size(201, 23);
            this.buttonSimulateTask.TabIndex = 2;
            this.buttonSimulateTask.Text = "Simulate with Task";
            this.buttonSimulateTask.UseVisualStyleBackColor = true;
            this.buttonSimulateTask.Click += new System.EventHandler(this.buttonSimulateTask_Click);
            // 
            // buttonStopSimulation
            // 
            this.buttonStopSimulation.Location = new System.Drawing.Point(6, 48);
            this.buttonStopSimulation.Name = "buttonStopSimulation";
            this.buttonStopSimulation.Size = new System.Drawing.Size(202, 23);
            this.buttonStopSimulation.TabIndex = 3;
            this.buttonStopSimulation.Text = "Stop simulation";
            this.buttonStopSimulation.UseVisualStyleBackColor = true;
            this.buttonStopSimulation.Click += new System.EventHandler(this.buttonStopSimulation_Click);
            // 
            // buttonSimulateWithThread
            // 
            this.buttonSimulateWithThread.Location = new System.Drawing.Point(3, 6);
            this.buttonSimulateWithThread.Name = "buttonSimulateWithThread";
            this.buttonSimulateWithThread.Size = new System.Drawing.Size(201, 23);
            this.buttonSimulateWithThread.TabIndex = 4;
            this.buttonSimulateWithThread.Text = "Simulate with Thread";
            this.buttonSimulateWithThread.UseVisualStyleBackColor = true;
            this.buttonSimulateWithThread.Click += new System.EventHandler(this.buttonSimulateWithThread_Click);
            // 
            // buttonSimulateWithThreadPool
            // 
            this.buttonSimulateWithThreadPool.Location = new System.Drawing.Point(3, 3);
            this.buttonSimulateWithThreadPool.Name = "buttonSimulateWithThreadPool";
            this.buttonSimulateWithThreadPool.Size = new System.Drawing.Size(204, 23);
            this.buttonSimulateWithThreadPool.TabIndex = 5;
            this.buttonSimulateWithThreadPool.Text = "Simulate with ThreadPool";
            this.buttonSimulateWithThreadPool.UseVisualStyleBackColor = true;
            this.buttonSimulateWithThreadPool.Click += new System.EventHandler(this.buttonSimulateWithThreadPool_Click);
            // 
            // buttonManualEmergency
            // 
            this.buttonManualEmergency.Location = new System.Drawing.Point(6, 19);
            this.buttonManualEmergency.Name = "buttonManualEmergency";
            this.buttonManualEmergency.Size = new System.Drawing.Size(202, 23);
            this.buttonManualEmergency.TabIndex = 6;
            this.buttonManualEmergency.Text = "Manual Emergency";
            this.buttonManualEmergency.UseVisualStyleBackColor = true;
            this.buttonManualEmergency.Click += new System.EventHandler(this.buttonManualEmergency_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(779, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stats";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonManualEmergency);
            this.groupBox2.Controls.Add(this.buttonStopSimulation);
            this.groupBox2.Location = new System.Drawing.Point(783, 389);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 84);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(779, 119);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(218, 64);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonSimulateWithThread);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(210, 38);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thread";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonSimulateTask);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(210, 238);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Task";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonSimulateWithThreadPool);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(210, 238);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ThreadPool";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numericUpDownMultiplier);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.numericUpDownSimulationLength);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numericUpDownParallelVertical);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.numericUpDownParallelHorizontal);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.numericUpDownHumans);
            this.groupBox3.Location = new System.Drawing.Point(783, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 193);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parameters";
            // 
            // numericUpDownHumans
            // 
            this.numericUpDownHumans.Location = new System.Drawing.Point(157, 19);
            this.numericUpDownHumans.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHumans.Name = "numericUpDownHumans";
            this.numericUpDownHumans.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownHumans.TabIndex = 0;
            this.numericUpDownHumans.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of humans";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Parallelization (Horizontal)";
            // 
            // numericUpDownParallelHorizontal
            // 
            this.numericUpDownParallelHorizontal.Location = new System.Drawing.Point(157, 70);
            this.numericUpDownParallelHorizontal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownParallelHorizontal.Name = "numericUpDownParallelHorizontal";
            this.numericUpDownParallelHorizontal.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownParallelHorizontal.TabIndex = 2;
            this.numericUpDownParallelHorizontal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Parallelization (Vertical)";
            // 
            // numericUpDownParallelVertical
            // 
            this.numericUpDownParallelVertical.Location = new System.Drawing.Point(157, 96);
            this.numericUpDownParallelVertical.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownParallelVertical.Name = "numericUpDownParallelVertical";
            this.numericUpDownParallelVertical.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownParallelVertical.TabIndex = 4;
            this.numericUpDownParallelVertical.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Length of the simulation (sec)";
            // 
            // numericUpDownSimulationLength
            // 
            this.numericUpDownSimulationLength.Location = new System.Drawing.Point(157, 45);
            this.numericUpDownSimulationLength.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownSimulationLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSimulationLength.Name = "numericUpDownSimulationLength";
            this.numericUpDownSimulationLength.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownSimulationLength.TabIndex = 6;
            this.numericUpDownSimulationLength.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Number of Blocks (multiplier)";
            // 
            // numericUpDownMultiplier
            // 
            this.numericUpDownMultiplier.Location = new System.Drawing.Point(157, 122);
            this.numericUpDownMultiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMultiplier.Name = "numericUpDownMultiplier";
            this.numericUpDownMultiplier.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownMultiplier.TabIndex = 8;
            this.numericUpDownMultiplier.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 486);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1025, 525);
            this.MinimumSize = new System.Drawing.Size(1025, 525);
            this.Name = "MainForm";
            this.Text = "Heat Transfer Simulation";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHumans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownParallelHorizontal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownParallelVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSimulationLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMultiplier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSimulateTask;
        private System.Windows.Forms.Button buttonStopSimulation;
        private System.Windows.Forms.Button buttonSimulateWithThread;
        private System.Windows.Forms.Button buttonSimulateWithThreadPool;
        private System.Windows.Forms.Button buttonManualEmergency;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownSimulationLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownParallelVertical;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownParallelHorizontal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownHumans;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownMultiplier;
    }
}

