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

namespace HeatTrasnfer
{
    public partial class Form1 : Form
    {
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

            Task.Run(() => {

                var screenWidth = pictureBox1.Width;
                var screenHeight = pictureBox1.Height;

                int width = 100;
                int height = 100;

                HeatBlock[,] hbs = new HeatBlock[width, height];

                for(int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        HeatBlock hb = new HeatBlock();
                        hb.Size = new PointF(15, 15);
                        hb.Position = new PointF(i * hb.Size.X, j * hb.Size.Y);
                        hbs[i, j] = hb;
                    }
                }

                hbs[0, 0].Temperature = 250;

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                    System.Drawing.Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                    hbs[0, 0].Temperature = 250;
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {

                            HeatBlock hb = hbs[i, j];

                            float left = i == 0 ? hb.Temperature : hbs[i - 1, j].Temperature;
                            float right = i == width - 1 ? hb.Temperature : hbs[i + 1, j].Temperature;
                            float top = j == height - 1 ? hb.Temperature : hbs[i, j + 1].Temperature;
                            float botton = j == 0 ? hb.Temperature : hbs[i, j - 1].Temperature;

                            hb.CalculateColor(left, right, top, botton);
                        }

                    }

                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                            hbs[i, j].Draw(gfx);

                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                            hbs[i, j].Sync();

                    if (sw.ElapsedMilliseconds > 1000)
                    {
                        //WriteStats(); // Very slow
                        sw.Restart();
                    }
                    ChangeScreen(buffer);//set the PictureBox's image to be the buffer
                }
            });
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
