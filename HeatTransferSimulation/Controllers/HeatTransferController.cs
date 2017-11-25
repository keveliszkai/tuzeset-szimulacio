using HeatTransferSimulation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeatTransferSimulation.Controllers
{
    class HeatTransferController
    {
        public int ArrayWidth { get; set; } = 15;
        public int ArrayHeight { get; set; } = 9;

        public int BlockWidth { get; set; } = 50;
        public int BlockHeight { get; set; } = 50;

        private Stopwatch CalculationStopper = new Stopwatch();
        public long CalculationTime = 0;

        public HeatBlock[,] HeatBlocks;

        public void Boot(List<Wall> walls, int multiplier)
        {
            if (BlockHeight % multiplier != 0)
                multiplier = 1;

            ArrayHeight = ArrayHeight * multiplier;
            ArrayWidth = ArrayWidth * multiplier;
            BlockHeight = BlockHeight / multiplier;
            BlockWidth = BlockWidth / multiplier;

            HeatBlocks = new HeatBlock[ArrayWidth, ArrayHeight];

            for (int i = 0; i < ArrayWidth; i++)
            {
                for (int j = 0; j < ArrayHeight; j++)
                {
                    HeatBlock hb = new HeatBlock();
                    hb.Size = new PointF(BlockWidth, BlockHeight);
                    hb.Position = new PointF(i * hb.Size.X, j * hb.Size.Y);

                    walls.ForEach(w =>
                    {
                        if (w.getDistance(hb.Position) < 1)
                        {
                            hb.Alpha = 0.01F;
                        }
                    });

                    HeatBlocks[i, j] = hb;
                }
            }
        }

        public void Calculate()
        {
            CalculationStopper.Restart();

            for (int i = 0; i < ArrayWidth; i++)
            {
                for (int j = 0; j < ArrayHeight; j++)
                {
                    HeatBlock hb = HeatBlocks[i, j];

                    float left = i == 0 ? hb.Temperature : HeatBlocks[i - 1, j].Temperature;
                    float right = i == ArrayWidth - 1 ? hb.Temperature : HeatBlocks[i + 1, j].Temperature;
                    float top = j == ArrayHeight - 1 ? hb.Temperature : HeatBlocks[i, j + 1].Temperature;
                    float botton = j == 0 ? hb.Temperature : HeatBlocks[i, j - 1].Temperature;

                    hb.CalculateColor(left, right, top, botton);
                }

            }

            CalculationStopper.Stop();
            CalculationTime = CalculationStopper.ElapsedTicks;
        }

        public void CalculateThread(int verticalDiv, int horizontalDiv)
        {
            CalculationStopper.Restart();

            List<Thread> threads = new List<Thread>();

            for (int vertical = 0; vertical < verticalDiv; vertical++)
            {
                for (int horizontal = 0; horizontal < horizontalDiv; horizontal++)
                {
                    int vertical_copy = vertical;
                    int horizontal_copy = horizontal;

                    threads.Add(new Thread(() =>
                    {
                        int _widthBase = (ArrayWidth / horizontalDiv);
                        int _heightBase = (ArrayHeight / verticalDiv);
                        int borderWidthLower = _widthBase * horizontal_copy;
                        int borderWidthUpper = _widthBase * (horizontal_copy + 1);
                        int borderHeightLower = _heightBase * vertical_copy;
                        int borderHeightUpper = _heightBase * (vertical_copy + 1);

                        for (int i = borderWidthLower; i < borderWidthUpper; i++)
                        {
                            for (int j = borderHeightLower; j < borderHeightUpper; j++)
                            {
                                HeatBlock hb = HeatBlocks[i, j];

                                float left = i == 0 ? hb.Temperature : HeatBlocks[i - 1, j].Temperature;
                                float right = i == ArrayWidth - 1 ? hb.Temperature : HeatBlocks[i + 1, j].Temperature;
                                float top = j == ArrayHeight - 1 ? hb.Temperature : HeatBlocks[i, j + 1].Temperature;
                                float botton = j == 0 ? hb.Temperature : HeatBlocks[i, j - 1].Temperature;

                                hb.CalculateColor(left, right, top, botton);
                            }

                        }
                    }));
                }
            }

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());

            CalculationStopper.Stop();
            CalculationTime = CalculationStopper.ElapsedTicks;
        }

        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);
            
            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        private Object _lockObject = new Object();

        public void DrawThread(Bitmap buffer, Graphics g, int verticalDiv, int horizontalDiv)
        {
            int verticalMax = 450;
            int horizontalMax = 750;

            int verticalSize = verticalMax / verticalDiv;
            int horizontalSize = horizontalMax / horizontalDiv;

            List<Thread> threads = new List<Thread>();
            for (int vertical = 0; vertical < verticalDiv; vertical++)
            {
                for (int horizontal = 0; horizontal < horizontalDiv; horizontal++)
                {
                    int horizontal_copy = horizontal;
                    int vertical_copy = vertical;
                    
                    Rectangle section = new Rectangle(new Point(0, 0), new Size(750, 450));
                    Bitmap CroppedImage = CropImage(buffer, section);
                    System.Drawing.Graphics g_copy = Graphics.FromImage(CroppedImage);
                    threads.Add(new Thread(() =>
                    {
                        int _widthBase = (ArrayWidth / horizontalDiv);
                        int _heightBase = (ArrayHeight / verticalDiv);
                        int borderWidthLower = _widthBase * horizontal_copy;
                        int borderWidthUpper = _widthBase * (horizontal_copy + 1);
                        int borderHeightLower = _heightBase * vertical_copy;
                        int borderHeightUpper = _heightBase * (vertical_copy + 1);

                        for (int i = borderWidthLower; i < borderWidthUpper; i++)
                            for (int j = borderHeightLower; j < borderHeightUpper; j++)
                                HeatBlocks[i, j].Draw(g_copy);

                        lock (_lockObject)
                            g.DrawImage(CroppedImage, horizontal_copy * horizontalSize, vertical_copy * verticalSize,
                                new Rectangle(new Point(horizontal_copy * horizontalSize, vertical_copy * verticalSize),
                                new Size(horizontalSize, verticalSize)), GraphicsUnit.Pixel);
                    }));
                }
            }

            threads.ForEach(t => t.Start());
                threads.ForEach(t => t.Join());

                for (int i = 0; i < ArrayWidth; i++)
                    for (int j = 0; j < ArrayHeight; j++)
                        HeatBlocks[i, j].Sync();
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < ArrayWidth; i++)
                for (int j = 0; j < ArrayHeight; j++)
                    HeatBlocks[i, j].Draw(g);

            for (int i = 0; i < ArrayWidth; i++)
                for (int j = 0; j < ArrayHeight; j++)
                    HeatBlocks[i, j].Sync();
        }

        public HeatBlock GetNearestBlock(PointF point)
        {
            HeatBlock minimum = HeatBlocks[0, 0];

            for (int i = 0; i < ArrayWidth; i++)
                for (int j = 0; j < ArrayHeight; j++)
                    if (Vector.GetDistanceBetweenPoints(HeatBlocks[i, j].Center, point) <
                        Vector.GetDistanceBetweenPoints(minimum.Center, point))
                        minimum = HeatBlocks[i, j];

            return minimum;
        }
    }
}
