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

namespace FloorPlantGenerator
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

            List<Room> rooms = new List<Room>();

            Room mainRoom = new Room(new PointF(0, 0), new PointF(750, 450));
            mainRoom.AddDoor(Room.DoorPosition.Right, 50);

            Room room1 = new Room(new PointF(0, 0), new PointF(150, 150));
            room1.AddDoor(Room.DoorPosition.Down, 50);

            Room room2 = new Room(new PointF(150, 0), new PointF(300, 150));
            room2.AddDoor(Room.DoorPosition.Down, 50);

            Room room3 = new Room(new PointF(500, 0), new PointF(750, 150));
            room3.AddDoor(Room.DoorPosition.Down, 50);

            Room room4 = new Room(new PointF(0, 300), new PointF(150, 450));
            room4.AddDoor(Room.DoorPosition.Right, 50);

            Room room5 = new Room(new PointF(300, 300), new PointF(550, 450));
            room5.AddDoor(Room.DoorPosition.Left, 50);

            Room room6 = new Room(new PointF(550, 300), new PointF(750, 450));
            room6.AddDoor(Room.DoorPosition.Up, 50);

            rooms.Add(mainRoom);
            rooms.Add(room1);
            rooms.Add(room2);
            rooms.Add(room3);
            rooms.Add(room4);
            rooms.Add(room5);
            rooms.Add(room6);

            Task.Run(() => {

                var screenWidth = pictureBox1.Width;
                var screenHeight = pictureBox1.Height;

                int width = 15;
                int height = 9;

                HeatBlock[,] hbs = new HeatBlock[width, height];

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        HeatBlock hb = new HeatBlock();
                        hb.Size = new PointF(50, 50);
                        hb.Position = new PointF(i * hb.Size.X, j * hb.Size.Y);

                        rooms.ForEach(r => r.Walls.ForEach(w =>
                        {
                            if (w.getDistance(hb.Position) < 1)
                            {
                                hb.Alpha = 0.1F;
                            }
                        }));

                        hbs[i, j] = hb;
                    }
                }

                while (true)
                {
                    Bitmap buffer = new Bitmap(screenWidth, screenHeight);
                    System.Drawing.Graphics gfx = Graphics.FromImage(buffer);//set the graphics to draw on the image

                    rooms.ForEach(i => i.Draw(gfx));

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
