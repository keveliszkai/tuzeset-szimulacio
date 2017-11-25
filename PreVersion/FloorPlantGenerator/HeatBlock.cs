using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlantGenerator
{
    class HeatBlock
    {
        public PointF Size { get; set; }
        public PointF Position { get; set; }
        public Color Color { get; set; }
        public float Temperature { get; set; } = 20;
        public float NewTemperature { get; set; }
        public float Alpha = 0.25F;

        public void Draw(Graphics g)
        {
            // uncomment for higher quality output
            g.InterpolationMode = InterpolationMode.High;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle r = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)Size.X,
                (int)Size.Y
                );

            //g.DrawRectangle(pen, r);
            g.FillRectangle(new SolidBrush(Color), r);
        }

        public void CalculateColor(float leftTemp, float rightTemp, float topTemp, float bottomTemp)
        {
            float verticalTemp = leftTemp - 2 * Temperature + rightTemp;
            float horizontalTemp = topTemp - 2 * Temperature + bottomTemp;
            NewTemperature = Temperature + Alpha * (verticalTemp + horizontalTemp);

            float maxTemperature = 255;
            float relTemp = NewTemperature / maxTemperature;

            int alpha = 100;

            if (relTemp < 0.2)
                Color = Color.FromArgb(alpha, 0, (int)(5 * relTemp * 255), 255);

            if (0.2 <= relTemp && relTemp < 0.4)
                Color = Color.FromArgb(alpha, 0, 255, (int)(5 * (relTemp - 0.2) * 255));

            if (0.4 <= relTemp && relTemp < 0.6)
                Color = Color.FromArgb(alpha, (int)(5 * (relTemp - 0.4) * 255), 255, 0);

            if (0.6 <= relTemp && relTemp < 0.8)
                Color = Color.FromArgb(alpha, 255, 255 - (int)(5 * (relTemp - 0.6) * 255), 0);

            if (0.8 <= relTemp && relTemp <= 1)
                Color = Color.FromArgb(alpha, 255, (int)(5 * (relTemp - 0.8) * 255), (int)(5 * (relTemp - 0.8) * 255));
        }

        public void Sync()
        {
            Temperature = NewTemperature;
        }
    }
}
