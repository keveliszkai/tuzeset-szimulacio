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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeatTransferSimulation
{
    public partial class MainForm : Form
    {
        private FloorPlanController FloorPlanController = new FloorPlanController();
        private HeatTransferController HeatTransferController = new HeatTransferController();
        private HumanController HumanController = new HumanController();

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

            BootControllers();

            Task.Run(() => {

                var screenWidth = pictureBox.Width;
                var screenHeight = pictureBox.Height;

                Random rnd = new Random();
                int randomCounter = 0;

                Stopwatch sw = new Stopwatch();
                sw.Start();

                while (true)
                {
                    Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                    System.Drawing.Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                    HumanController.Calculate(
                        FloorPlanController.Walls,
                        FloorPlanController.Doors,
                        FloorPlanController.Rooms
                    );

                    HumanController.Humans.ForEach(h =>
                        h.Temperature = HeatTransferController.GetNearestBlock(h.Position).Temperature
                    );

                    HeatTransferController.Calculate();

                    HumanController.Draw(gfx);
                    FloorPlanController.Draw(gfx);
                    HeatTransferController.Draw(gfx);
                    if (sw.ElapsedMilliseconds > 20)
                    {

                        ChangeScreen(buffer);//set the PictureBox's image to be the buffer

                        if(randomCounter == 0)
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
                }
            });
        }

        private void BootControllers()
        {
            FloorPlanController.Boot();
            HeatTransferController.ArrayHeight = 18;
            HeatTransferController.ArrayWidth = 30;
            HeatTransferController.BlockHeight = 25;
            HeatTransferController.BlockWidth = 25;

            HeatTransferController.Boot(FloorPlanController.Walls);
            HumanController.Boot(FloorPlanController.Walls, FloorPlanController.Doors, 5);
            HumanController.Humans.ForEach(i => i.SetEmergencyRoute(FloorPlanController.EmergencyRoute));
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

            label1.Text = clicked.X.ToString() + "-" + clicked.Y.ToString();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.E)
            {
                this.HumanController.Humans.ForEach(h => h.PathFind());
            }
        }
    }
}
