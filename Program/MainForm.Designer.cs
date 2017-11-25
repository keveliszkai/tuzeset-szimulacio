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
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonWithoutAny = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.comboBoxMultipliers = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownSimulationLength = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownParallelVertical = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownParallelHorizontal = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownHumans = new System.Windows.Forms.NumericUpDown();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.Logs = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBoxParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSimulationLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownParallelVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownParallelHorizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHumans)).BeginInit();
            this.Logs.SuspendLayout();
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
            this.buttonSimulateWithThreadPool.Location = new System.Drawing.Point(3, 6);
            this.buttonSimulateWithThreadPool.Name = "buttonSimulateWithThreadPool";
            this.buttonSimulateWithThreadPool.Size = new System.Drawing.Size(201, 23);
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
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.buttonManualEmergency);
            this.groupBoxActions.Controls.Add(this.buttonStopSimulation);
            this.groupBoxActions.Location = new System.Drawing.Point(783, 389);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(214, 84);
            this.groupBoxActions.TabIndex = 8;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(779, 119);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(218, 65);
            this.tabControl.TabIndex = 10;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonWithoutAny);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(210, 39);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Simple";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // buttonWithoutAny
            // 
            this.buttonWithoutAny.Location = new System.Drawing.Point(3, 6);
            this.buttonWithoutAny.Name = "buttonWithoutAny";
            this.buttonWithoutAny.Size = new System.Drawing.Size(201, 23);
            this.buttonWithoutAny.TabIndex = 0;
            this.buttonWithoutAny.Text = "Without any Parallelization";
            this.buttonWithoutAny.UseVisualStyleBackColor = true;
            this.buttonWithoutAny.Click += new System.EventHandler(this.buttonWithoutAny_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonSimulateWithThread);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(210, 39);
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
            this.tabPage2.Size = new System.Drawing.Size(210, 39);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Task";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonSimulateWithThreadPool);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(210, 39);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ThreadPool";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Controls.Add(this.comboBoxMultipliers);
            this.groupBoxParameters.Controls.Add(this.button1);
            this.groupBoxParameters.Controls.Add(this.label6);
            this.groupBoxParameters.Controls.Add(this.label5);
            this.groupBoxParameters.Controls.Add(this.numericUpDownSimulationLength);
            this.groupBoxParameters.Controls.Add(this.label4);
            this.groupBoxParameters.Controls.Add(this.numericUpDownParallelVertical);
            this.groupBoxParameters.Controls.Add(this.label3);
            this.groupBoxParameters.Controls.Add(this.numericUpDownParallelHorizontal);
            this.groupBoxParameters.Controls.Add(this.label2);
            this.groupBoxParameters.Controls.Add(this.numericUpDownHumans);
            this.groupBoxParameters.Location = new System.Drawing.Point(783, 190);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Size = new System.Drawing.Size(214, 193);
            this.groupBoxParameters.TabIndex = 11;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Parameters";
            // 
            // comboBoxMultipliers
            // 
            this.comboBoxMultipliers.DisplayMember = "10";
            this.comboBoxMultipliers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMultipliers.FormattingEnabled = true;
            this.comboBoxMultipliers.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "25",
            "50"});
            this.comboBoxMultipliers.Location = new System.Drawing.Point(157, 121);
            this.comboBoxMultipliers.Name = "comboBoxMultipliers";
            this.comboBoxMultipliers.Size = new System.Drawing.Size(47, 21);
            this.comboBoxMultipliers.TabIndex = 2;
            this.comboBoxMultipliers.SelectedValueChanged += new System.EventHandler(this.comboBoxMultipliers_SelectedValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 39);
            this.button1.TabIndex = 10;
            this.button1.Text = "Full Benchmark";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnFullBenchmark_Click);
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
            3600,
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of humans";
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
            // textBoxLogs
            // 
            this.textBoxLogs.Enabled = false;
            this.textBoxLogs.Location = new System.Drawing.Point(6, 19);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogs.Size = new System.Drawing.Size(968, 170);
            this.textBoxLogs.TabIndex = 12;
            // 
            // Logs
            // 
            this.Logs.Controls.Add(this.textBoxLogs);
            this.Logs.Location = new System.Drawing.Point(13, 479);
            this.Logs.Name = "Logs";
            this.Logs.Size = new System.Drawing.Size(984, 195);
            this.Logs.TabIndex = 13;
            this.Logs.TabStop = false;
            this.Logs.Text = "Logs";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 686);
            this.Controls.Add(this.Logs);
            this.Controls.Add(this.groupBoxParameters);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1025, 725);
            this.MinimumSize = new System.Drawing.Size(1025, 725);
            this.Name = "MainForm";
            this.Text = "Heat Transfer Simulation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxActions.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSimulationLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownParallelVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownParallelHorizontal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHumans)).EndInit();
            this.Logs.ResumeLayout(false);
            this.Logs.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownSimulationLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownParallelVertical;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownParallelHorizontal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownHumans;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.GroupBox Logs;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button buttonWithoutAny;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxMultipliers;
    }
}

