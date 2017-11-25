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

            CreateLog("Program started.");
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
            HeatTransferController.Boot(FloorPlanController.Walls, (int)numericUpDownMultiplier.Value);
            HumanController.Boot(FloorPlanController.Walls, FloorPlanController.Doors, (int)numericUpDownHumans.Value);
            HumanController.Humans.ForEach(i => i.SetEmergencyRoute(FloorPlanController.EmergencyRoute));
            SimulationLength = (int)numericUpDownSimulationLength.Value;
            ParallelHorizontal = (int)numericUpDownParallelHorizontal.Value;
            ParallelVertical = (int)numericUpDownParallelVertical.Value;
        }

        /** SIMULATIONS **/

        private void SimulateWithTask()
        {

            Simulate = true;
            CreateLog("Task Simulation Starting");

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
                        }
                    }
                }
            });
        }

        private void SimulateWithThread()
        {
            Simulate = true;
            CreateLog("Thread Simulation Starting");

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
            ResetControllers();
            BootControllers();

            Simulate = true;

            ThreadPool.QueueUserWorkItem(new WaitCallback(Body));
        }

        private void Body(Object threadContext)
        {
            var screenWidth = pictureBox.Width;
            var screenHeight = pictureBox.Height;

            Random rnd = new Random();
            int randomCounter = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Stopwatch swFps = new Stopwatch();
            swFps.Start();

            int fpsTmp = 0;

            ChangeText(string.Format("FPS: {0}\nCalculationTime: {1}", Fps, HeatTransferController.CalculationTime + HumanController.CalculationTime));

            while (Simulate)
            {
                Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                System.Drawing.Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                // Calculations
                HumanController.Calculate(
                    FloorPlanController.Walls,
                    FloorPlanController.Doors,
                    FloorPlanController.Rooms
                );

                HumanController.Humans.ForEach(h =>
                    h.Temperature = HeatTransferController.GetNearestBlock(h.Position).Temperature
                );

                HeatTransferController.Calculate();

                // Draws
                HumanController.Draw(gfx);
                FloorPlanController.Draw(gfx);
                HeatTransferController.Draw(gfx);

                if (sw.ElapsedMilliseconds > REFRESH_SCREEN_MS)
                {
                    ChangeScreen(buffer);//set the PictureBox's image to be the buffer

                    if (randomCounter == 0)
                    {
                        randomCounter = rnd.Next(0, 30);
                        HumanController.RandomEvent();
                    }
                    else
                    {
                        randomCounter--;
                    }

                    sw.Restart();
                }

                fpsTmp++;

                if (swFps.ElapsedMilliseconds > 1000)
                {
                    Fps = fpsTmp;
                    ChangeText(string.Format(
                        "FPS: {0}\n" +
                        "CalculationTime (Heat): {1}\n" +
                        "CalculationTime (Human): {2}\n", 
                        Fps, HeatTransferController.CalculationTime, HumanController.CalculationTime));
                    fpsTmp = 0;
                    swFps.Restart();
                }
            }
        }

        /** HANDLERS **/

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
            CreateLog("Thread Simulation Ended");
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
            CreateLog("Manual Emergency Event");
            this.HumanController.Humans.ForEach(h => h.PathFind());
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
