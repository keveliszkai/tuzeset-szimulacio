using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatTransferSimulation.Models
{
    class ViewGraphic : ZoombaleObject
    {
        //public PointF Size { get; set; }

        public void Draw(Graphics g, PointF position, float angle, bool debug)
        {
            // uncomment for higher quality output
            g.InterpolationMode = InterpolationMode.High;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int RedSize = 32;
            int HearSize = 75;
            int LookSize = 300;

            float scale = Math.Min(Size.X / Bitmap.Width, Size.Y / Bitmap.Height) * MainScale;
            var scaleWidth = (int)(Bitmap.Width * scale);
            var scaleHeight = (int)(Bitmap.Height * scale);

            Rectangle r = SizedRectangle(
                (int)position.X, 
                (int)position.Y,
                scaleWidth,
                scaleHeight
                );

            Rectangle s = SizedRectangle(
                (int)position.X, 
                (int)position.Y, 
                (int)LookSize,
                (int)LookSize);
            Rectangle t = SizedRectangle(
                (int)position.X, 
                (int)position.Y,
                (int)HearSize,
                (int)HearSize);
            Rectangle red = SizedRectangle(
                (int)position.X, 
                (int)position.Y,
                (int)RedSize,
                (int)RedSize);

            g.DrawImage(RotateImage(Bitmap, angle), r);

            Pen blackPen = new Pen(Color.Black);
            Pen redPen = new Pen(Color.Red);

            if (debug)
            {
                g.DrawEllipse(blackPen, t);
                g.DrawEllipse(redPen, red);
                g.DrawPie(blackPen, s, angle - 30, 60);
            }
        }

        public void SetBitmap(string path)
        {
            if(!string.IsNullOrEmpty(path))
            {
                ImagePath = path;
            }

            Bitmap = new Bitmap(ImagePath);
        }

        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Set the rotation point to the center in the matrix
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                // Rotate
                g.RotateTransform(angle);
                // Restore rotation point in the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                // Draw the image on the bitmap
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }
    }
}
