using HeatTransferSimulation.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatTransferSimulation.Controllers
{
    class HeatTransferController
    {
        public int ArrayWidth { get; set; } = 15;
        public int ArrayHeight { get; set; } = 9;

        public int BlockWidth { get; set; } = 50;
        public int BlockHeight { get; set; } = 50;

        public HeatBlock[,] HeatBlocks;

        public void Boot(List<Wall> walls)
        {
            HeatBlocks = new HeatBlock[ArrayWidth,ArrayHeight];

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
                            hb.Alpha = 0.1F;
                        }
                    });

                    HeatBlocks[i, j] = hb;
                }
            }

            /*HeatBlocks[0, 0].FixedTemperature = true;
            HeatBlocks[0, 0].Temperature = 250;*/
        }

        public void Calculate()
        {
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
            HeatBlock minimum = HeatBlocks[0,0];

            for (int i = 0; i < ArrayWidth; i++)
                for (int j = 0; j < ArrayHeight; j++)
                    if (Vector.GetDistanceBetweenPoints(HeatBlocks[i, j].Center, point) < 
                        Vector.GetDistanceBetweenPoints(minimum.Center, point))
                        minimum = HeatBlocks[i, j];

            return minimum;
        }
    }
}
