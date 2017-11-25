using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanWithMoving
{
    public partial class Form1 : Form
    {
        private Human One;
        private float Scale = 1;

        public Form1()
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

            ViewGraphic vg = new ViewGraphic();
            vg.ImagePath = Environment.CurrentDirectory + @"\..\..\Human.png";
            vg.Size = new PointF(32, 32);

            One = new Human(vg);
            One.Position = new PointF(400, 300);
            One.Target = new PointF(400, 300);

            Wall wl = new Wall(new PointF(100, 100), new PointF(400, 100));
            Door door = new Door(new PointF(200, 100), new PointF(300, 100));

            List<Wall> walls = new List<Wall>();
            List<Door> doors = new List<Door>();

            walls.Add(wl);
            doors.Add(door);

            Task.Run(() => {

                var screenWidth = pictureBox1.Width;
                var screenHeight = pictureBox1.Height;

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                    System.Drawing.Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                    One.Graphic.MainScale = Scale;

                    One.Calculate(walls, doors);

                    if ( wl.getDistance(One.NextPosition) < 10 && wl.getDistance(One.NextPosition) != door.getDistance(One.NextPosition))
                    {
                        var i = wl.getDistance(One.NextPosition);
                        One.NextPosition = One.Position;
                    }

                    One.Draw(gfx);
                    wl.Draw(gfx);
                    door.Draw(gfx);

                    if (sw.ElapsedMilliseconds > 50)
                    {
                        WriteStats(); // Very slow
                        sw.Restart();
                    }
                    ChangeScreen(buffer);//set the PictureBox's image to be the buffer
                }
            });
        }

        public void WriteStats()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(WriteStats), new object[] { });
                return;
            }

            label1.Text = "Position: " + One.Position.X.ToString() + "," + One.Position.Y.ToString();
            label2.Text = "Target: " + One.Target.X.ToString() + "," + One.Target.Y.ToString();
            label3.Text = "Moving: " + One.Moving.X.ToString() + "," + One.Moving.Y.ToString();
            label4.Text = "Angle: " + One.Angle.ToString();
            label5.Text = "Scale: " + Scale.ToString();
        }

        public void ChangeScreen(Bitmap image)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Bitmap>(ChangeScreen), new object[] { image });
                return;
            }
            pictureBox1.Image = image;
        }

        private Graphics graphics;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            One.Target = new PointF(me.X * (1/Scale), me.Y * (1 / Scale));
        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Q:
                    if(Scale > .3f)
                        Scale -= .2f;
                    break;
                case Keys.E:
                    Scale += .2f;
                    break;
            }
        }
    }
}
