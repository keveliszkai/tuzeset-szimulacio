using HeatTransferSimulation.Controllers;
using HeatTransferSimulation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeatTransferSimulation
{
    public partial class MainForm : Form
    {
        private const int REFRESH_SCREEN_MS = 20;
        private const int ONE_SEC = 1000;

        private bool Simulate = false;
        private int Fps = 0;

        private int SimulationLength = 60;
        private int ParallelHorizontal = 1;
        private int ParallelVertical = 1;

        private FloorPlanController FloorPlanController = new FloorPlanController();
        private HeatTransferController HeatTransferController = new HeatTransferController();
        private HumanController HumanController = new HumanController();

        private List<int> FpsNumbers = new List<int>();

        public MainForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);

            comboBoxMultipliers.SelectedIndex = 1;

            MessageBox.Show("Welcome! After starting the simulation, you can click on the screen, and the heat will come.", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CreateLogInvoke("Program started.");
        }

        private void ResetControllers()
        {
            FpsNumbers.Clear();
            HumanController = new HumanController();
            FloorPlanController = new FloorPlanController();
            HeatTransferController = new HeatTransferController();
        }

        private void BootControllers()
        {
            FloorPlanController.Boot();
            HeatTransferController.Boot(FloorPlanController.Walls, Int32.Parse(comboBoxMultipliers.SelectedItem.ToString()));
            HumanController.Boot(FloorPlanController.Walls, FloorPlanController.Doors, (int)numericUpDownHumans.Value);
            HumanController.Humans.ForEach(i => i.SetEmergencyRoute(FloorPlanController.EmergencyRoute));
            SimulationLength = (int)numericUpDownSimulationLength.Value;
            ParallelHorizontal = (int)numericUpDownParallelHorizontal.Value;
            ParallelVertical = (int)numericUpDownParallelVertical.Value;
        }

        /** SIMULATIONS **/

        private void SimulateWithoutAny()
        {
            Simulate = true;
            CreateLogInvoke("Simple Simulation Starting");

            ResetControllers();
            BootControllers();

            Task.Run(() => {

                Fps = 0;
                var screenWidth = pictureBox.Width;
                var screenHeight = pictureBox.Height;

                Stopwatch stopwatchFps = new Stopwatch();
                Stopwatch stopwatchDrawTime = new Stopwatch();

                stopwatchFps.Start();

                WriteStats(0, 0);

                while (Simulate)
                {
                    Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                    Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                    // Calculations
                    HumanController.Calculate(
                        FloorPlanController.Walls,
                        FloorPlanController.Doors,
                        FloorPlanController.Rooms
                    );

                    // Human --> HeatBlock
                    HumanController.Humans.ForEach(h =>
                        h.Temperature = HeatTransferController.GetNearestBlock(h.Position).Temperature
                    );

                    // Heat Temperature Calculation
                    HeatTransferController.Calculate();

                    // Drawing section
                    stopwatchDrawTime.Restart();
                    HumanController.Draw(gfx);
                    FloorPlanController.Draw(gfx);
                    HeatTransferController.Draw(gfx);
                    stopwatchDrawTime.Stop();

                    // Screen Change
                    ChangeScreen(buffer);
                    HumanController.RandomEvent();
                    Fps++;

                    // Check Logs
                    CheckLogger();

                    if (stopwatchFps.ElapsedMilliseconds > ONE_SEC)
                    {
                        FpsNumbers.Add(Fps);

                        float fpsAverage = FpsNumbers.Sum() / FpsNumbers.Count;

                        WriteStats(stopwatchDrawTime.ElapsedMilliseconds, fpsAverage);

                        Fps = 0;

                        stopwatchFps.Restart();

                        if (FpsNumbers.Count > SimulationLength)
                        {
                            Simulate = false;
                            CreateLogInvoke("Simple Simulation Ended");
                            stopwatchFps.Stop();
                            return;
                        }
                    }
                }
            });
        }

        private void SimulateWithTask()
        {

            Simulate = true;
            CreateLogInvoke("Task Simulation Starting");

            ResetControllers();
            BootControllers();

            Task.Run(() => {

                Fps = 0;
                var screenWidth = pictureBox.Width;
                var screenHeight = pictureBox.Height;

                Stopwatch stopwatchFps = new Stopwatch();
                Stopwatch stopwatchDrawTime = new Stopwatch();

                stopwatchFps.Start();

                WriteStats(0, 0);

                while (Simulate)
                {
                    Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                    Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                    // Calculations
                    HumanController.Calculate(
                        FloorPlanController.Walls,
                        FloorPlanController.Doors,
                        FloorPlanController.Rooms
                    );

                    // Human --> HeatBlock
                    HumanController.Humans.ForEach(h =>
                        h.Temperature = HeatTransferController.GetNearestBlock(h.Position).Temperature
                    );

                    // Heat Temperature Calculation
                    HeatTransferController.CalculateTask(ParallelVertical, ParallelHorizontal);

                    // Drawing section
                    stopwatchDrawTime.Restart();
                    HumanController.Draw(gfx);
                    FloorPlanController.Draw(gfx);
                    HeatTransferController.DrawTask(buffer, gfx, ParallelVertical, ParallelHorizontal);
                    stopwatchDrawTime.Stop();

                    // Screen Change
                    ChangeScreen(buffer);
                    HumanController.RandomEvent();
                    Fps++;

                    // Check Logs
                    CheckLogger();

                    if (stopwatchFps.ElapsedMilliseconds > ONE_SEC)
                    {
                        FpsNumbers.Add(Fps);

                        float fpsAverage = FpsNumbers.Sum() / FpsNumbers.Count;

                        WriteStats(stopwatchDrawTime.ElapsedMilliseconds, fpsAverage);

                        Fps = 0;

                        stopwatchFps.Restart();

                        if (FpsNumbers.Count > SimulationLength)
                        {
                            Simulate = false;
                            CreateLogInvoke("Task Simulation Ended");
                            stopwatchFps.Stop();
                            return;
                        }
                    }
                }
            });
        }

        private void SimulateWithThread()
        {
            Simulate = true;
            CreateLogInvoke("Thread Simulation Starting");

            ResetControllers();
            BootControllers();

            new Thread(() => {

                Fps = 0;
                var screenWidth = pictureBox.Width;
                var screenHeight = pictureBox.Height;
                
                Stopwatch stopwatchFps = new Stopwatch();
                Stopwatch stopwatchDrawTime = new Stopwatch();

                stopwatchFps.Start();

                WriteStats(0, 0);

                while (Simulate)
                {
                    Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                    Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                    // Calculations
                    HumanController.Calculate(
                        FloorPlanController.Walls,
                        FloorPlanController.Doors,
                        FloorPlanController.Rooms
                    );

                    // Human --> HeatBlock
                    HumanController.Humans.ForEach(h =>
                        h.Temperature = HeatTransferController.GetNearestBlock(h.Position).Temperature
                    );

                    // Heat Temperature Calculation
                    HeatTransferController.CalculateThread(ParallelVertical, ParallelHorizontal);

                    // Drawing section
                    stopwatchDrawTime.Restart();
                    HumanController.Draw(gfx);
                    FloorPlanController.Draw(gfx);
                    HeatTransferController.DrawThread(buffer, gfx, ParallelVertical, ParallelHorizontal);
                    stopwatchDrawTime.Stop();

                    // Screen Change
                    ChangeScreen(buffer);
                    HumanController.RandomEvent();
                    Fps++;

                    // Check Logs
                    CheckLogger();

                    if (stopwatchFps.ElapsedMilliseconds > ONE_SEC)
                    {
                        FpsNumbers.Add(Fps);

                        float fpsAverage = FpsNumbers.Sum() / FpsNumbers.Count;

                        WriteStats(stopwatchDrawTime.ElapsedMilliseconds, fpsAverage);

                        Fps = 0;

                        stopwatchFps.Restart();

                        if (FpsNumbers.Count > SimulationLength)
                        {
                            Simulate = false;
                            CreateLogInvoke("Thread Simulation Ended");
                            stopwatchFps.Stop();
                            return;
                        }
                    }
                }
            }).Start();
        }

        private void WriteStats(long drawTimeTicks, float fpsAverage)
        {
            ChangeText(string.Format(
                            "FPS: {0}\n" +
                            "FPS Average: {4}\n" +
                            "Calculation Time (Heat): {1} Ticks\n" +
                            "Calculation Time (Human): {2} Ticks\n" +
                            "DrawTime: {3} ms\n" +
                            "Time Elapsed: {5} sec",
                            Fps,
                            HeatTransferController.CalculationTime,
                            HumanController.CalculationTime,
                            drawTimeTicks,
                            fpsAverage,
                            FpsNumbers.Count)
            );
        }

        private void SimulateWithThreadPool()
        {
            Simulate = true;
            CreateLogInvoke("ThreadPool Simulation Starting");

            ResetControllers();
            BootControllers();

            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolBody));
        }

        private void ThreadPoolBody(Object threadContext)
        {
            Fps = 0;
            var screenWidth = pictureBox.Width;
            var screenHeight = pictureBox.Height;

            Stopwatch stopwatchFps = new Stopwatch();
            Stopwatch stopwatchDrawTime = new Stopwatch();

            stopwatchFps.Start();

            WriteStats(0, 0);

            while (Simulate)
            {
                Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                // Calculations
                HumanController.Calculate(
                    FloorPlanController.Walls,
                    FloorPlanController.Doors,
                    FloorPlanController.Rooms
                );

                // Human --> HeatBlock
                HumanController.Humans.ForEach(h =>
                    h.Temperature = HeatTransferController.GetNearestBlock(h.Position).Temperature
                );

                // Heat Temperature Calculation
                HeatTransferController.CalculateThread(ParallelVertical, ParallelHorizontal);

                // Drawing section
                stopwatchDrawTime.Restart();
                HumanController.Draw(gfx);
                FloorPlanController.Draw(gfx);
                HeatTransferController.DrawThread(buffer, gfx, ParallelVertical, ParallelHorizontal);
                stopwatchDrawTime.Stop();

                // Screen Change
                ChangeScreen(buffer);
                HumanController.RandomEvent();
                Fps++;

                // Check Logs
                CheckLogger();

                if (stopwatchFps.ElapsedMilliseconds > ONE_SEC)
                {
                    FpsNumbers.Add(Fps);

                    float fpsAverage = FpsNumbers.Sum() / FpsNumbers.Count;

                    WriteStats(stopwatchDrawTime.ElapsedMilliseconds, fpsAverage);

                    Fps = 0;

                    stopwatchFps.Restart();

                    if (FpsNumbers.Count > SimulationLength)
                    {
                        Simulate = false;
                        CreateLogInvoke("Thread Simulation Ended");
                        stopwatchFps.Stop();
                        return;
                    }
                }
            }
        }

        private void FullBenchmark()
        {
            DialogResult dialogResult = MessageBox.Show(string.Format("This benchmark will take {0} seconds. Are You Sure?", (numericUpDownSimulationLength.Value * 4)), "Are You Sure", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                    this.tabControl.Enabled = false;
                    this.groupBoxActions.Enabled = false;
                    this.groupBoxParameters.Enabled = false;

                Task.Run(() =>
                {

                    List<float> Results = new List<float>();

                    SimulateWithoutAny();
                    HeatTransferController.HeatBlocks[10, 10].FixedTemperature = true;
                    HeatTransferController.HeatBlocks[10, 10].Temperature = 250;
                    while (Simulate) { };
                    Results.Add(FpsNumbers.Sum() / FpsNumbers.Count);


                    SimulateWithThread();
                    HeatTransferController.HeatBlocks[10, 10].FixedTemperature = true;
                    HeatTransferController.HeatBlocks[10, 10].Temperature = 250;
                    while (Simulate) { };
                    Results.Add(FpsNumbers.Sum() / FpsNumbers.Count);


                    SimulateWithTask();
                    HeatTransferController.HeatBlocks[10, 10].FixedTemperature = true;
                    HeatTransferController.HeatBlocks[10, 10].Temperature = 250;
                    while (Simulate) { };
                    Results.Add(FpsNumbers.Sum() / FpsNumbers.Count);

                    SimulateWithThreadPool();
                    HeatTransferController.HeatBlocks[10, 10].FixedTemperature = true;
                    HeatTransferController.HeatBlocks[10, 10].Temperature = 250;
                    while (Simulate) { };
                    Results.Add(FpsNumbers.Sum() / FpsNumbers.Count);

                    string text = string.Format("Restults:{0}{1}{0}", Environment.NewLine, "----------------");

                    text += string.Format("Simple Average Fps: {0}{1}", Results[0], Environment.NewLine);
                    text += string.Format("Thread Average Fps: {0}{1}", Results[1], Environment.NewLine);
                    text += string.Format("Task Average Fps: {0}{1}", Results[2], Environment.NewLine);
                    text += string.Format("ThreadPool Average Fps: {0}{1}", Results[3], Environment.NewLine);

                    CreateLogInvoke(text);
                }).ContinueWith(res => {
                    MessageBox.Show("The results can be found in the log.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.EnableControls();
                });
            }
        }

        /** HANDLERS **/

        private void EnableControls()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(EnableControls));
                return;
            }
            this.groupBoxParameters.Enabled = true;
            this.groupBoxActions.Enabled = true;
            this.tabControl.Enabled = true;
        }

        private void CheckLogger()
        {
            HumanController.Humans.ForEach(human => {
                while(human.Logs.Count > 0)
                {
                    CreateLogInvoke(string.Format("{0} : {1}", human.Name, human.Logs.First()));
                    human.Logs.RemoveAt(0);
                }
            });
        }

        private void CreateLogInvoke(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(CreateLogInvoke), new object[] { text });
                return;
            }
            CreateLog(text);
        }

        private void CreateLog(string text)
        {
            textBoxLogs.AppendText(string.Format("[{0}] - {1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), text, Environment.NewLine));
        }

        public void ChangeScreen(Bitmap image)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Bitmap>(ChangeScreen), new object[] { image });
                return;
            }
            pictureBox.Image = image;
        }

        public void ChangeText(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(ChangeText), new object[] { text });
                return;
            }
            label1.Text = text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.ClickHandler((MouseEventArgs)e);
        }

        private void ClickHandler(MouseEventArgs me)
        {
            if (Simulate)
            {
                PointF clicked = new PointF(me.X, me.Y);
                HeatBlock selected = HeatTransferController.GetNearestBlock(clicked);

                selected.FixedTemperature = true;
                selected.Temperature = 250;

                CreateLogInvoke(string.Format("Fire started at X:{0}-Y:{1}", me.X, me.Y));
            }
        }

        /** BUTTONS **/

        private void buttonStopSimulation_Click(object sender, EventArgs e)
        {
            Simulate = false;
            CreateLogInvoke("Thread Simulation Ended");
        }

        private void buttonSimulateTask_Click(object sender, EventArgs e)
        {
            SimulateWithTask();
        }

        private void buttonSimulateWithThread_Click(object sender, EventArgs e)
        {
            SimulateWithThread();
        }

        private void buttonSimulateWithThreadPool_Click(object sender, EventArgs e)
        {
            SimulateWithThreadPool();
        }

        private void buttonManualEmergency_Click(object sender, EventArgs e)
        {
            CreateLogInvoke("Manual Emergency Event");
            this.HumanController.Humans.ForEach(h => h.PathFind());
        }

        private void buttonWithoutAny_Click(object sender, EventArgs e)
        {
            SimulateWithoutAny();
        }

        private void btnFullBenchmark_Click(object sender, EventArgs e)
        {
            FullBenchmark();
        }

        /** OTHERS **/

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
