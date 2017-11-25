using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanWithMoving
{
    class ZoombaleObject
    {
        public string ImagePath { get; set; }
        public Bitmap Bitmap { get; set; }

        public PointF Size { get; set; }
        public PointF Position { get; set; }
        public float MainScale { get; set; }

        public Rectangle SizedRectangle(int x, int y, int width, int height)
        {
            return new Rectangle(
                (int)((x - width / 2) * MainScale),
                (int)((y - height / 2) * MainScale),
                (int)(MainScale * width),
                (int)(MainScale * height));
        }
    }
}
