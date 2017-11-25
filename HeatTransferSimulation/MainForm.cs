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
        }

        private void ResetControllers()
        {
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
            ResetControllers();
            BootControllers();

            Simulate = true;

            Task.Run(() => {

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
            });
        }

        private void SimulateWithThread()
        {
            ResetControllers();
            BootControllers();

            Simulate = true;

            new Thread(() => {

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

                Task Draw = null;
                Bitmap DrawBuffer;

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

                    HeatTransferController.CalculateThread(1, 1);

                    Stopwatch drawTime = new Stopwatch();
                    drawTime.Start();

                    HumanController.Draw(gfx);
                    FloorPlanController.Draw(gfx);
                    HeatTransferController.DrawThread(buffer, gfx, 2, 1);
                    //HeatTransferController.Draw(gfx);

                    drawTime.Stop();

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
                        FpsNumbers.Add(Fps);

                        if (FpsNumbers.Count > SimulationLength)
                            Simulate = false;

                        float fpsAverage = FpsNumbers.Sum() / FpsNumbers.Count;

                        ChangeText(string.Format(
                            "FPS: {0}\n" +
                            "CalculationTime (Heat): {1}\n" +
                            "CalculationTime (Human): {2}\n" +
                            "DrawTime: {3}\n" +
                            "FPS Average: {4}",
                            Fps, 
                            HeatTransferController.CalculationTime, 
                            HumanController.CalculationTime,
                            drawTime.ElapsedMilliseconds,
                            fpsAverage));
                        fpsTmp = 0;
                        swFps.Restart();
                    }
                }
            }).Start();
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
            PointF clicked = new PointF(me.X, me.Y);
            HeatBlock selected = HeatTransferController.GetNearestBlock(clicked);

            selected.FixedTemperature = true;
            selected.Temperature = 250;
        }

        /** BUTTONS **/

        private void buttonStopSimulation_Click(object sender, EventArgs e)
        {
            Simulate = false;
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
            this.HumanController.Humans.ForEach(h => h.PathFind());
        }

        /** OTHERS **/

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
