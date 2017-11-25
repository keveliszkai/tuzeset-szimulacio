using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanWithMoving
{
    class Vector
    {
        public static double GetDistanceBetweenPoints(PointF p, PointF q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }

        public static PointF Add(PointF p, PointF q)
        {
            float a = p.X + q.X;
            float b = p.Y + q.Y;
            return new PointF(a, b);
        }

        public static float GetAngle(PointF p)
        {
            return (float)Math.Atan2(p.Y, p.X) * (180 / (float)Math.PI);
        }

        public static PointF Subtract(PointF p, PointF q)
        {
            float a = p.X - q.X;
            float b = p.Y - q.Y;
            return new PointF(a, b);
        }

        public static PointF Multiply(PointF p, float f)
        {
            float a = p.X * f;
            float b = p.Y * f;
            return new PointF(a, b);
        }
    }
}
