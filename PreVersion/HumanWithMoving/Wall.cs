using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanWithMoving
{
    class Wall
    {
        public PointF StartPoint { get; set; }
        public PointF EndPoint { get; set; }

        public Wall(PointF startPoint, PointF endPoint)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }

        public static float DistanceTo(PointF from, PointF to)
        {
            return (float)Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
        }

        public float getDistance(PointF position)
        {
            double tI = ((EndPoint.X - StartPoint.X) * (position.X - StartPoint.X) + (EndPoint.Y - StartPoint.Y) * (position.Y - StartPoint.Y)) / Math.Pow(DistanceTo(StartPoint, EndPoint), 2);
            double dP = ((EndPoint.X - StartPoint.X) * (position.Y - StartPoint.Y) - (EndPoint.Y - StartPoint.Y) * (position.X - StartPoint.X)) / DistanceTo(StartPoint, EndPoint);

            if (tI >= 0d && tI <= 1d)
                return (float)Math.Abs(dP);
            else
                return Math.Min(DistanceTo(position, StartPoint), DistanceTo(position, EndPoint));
        }

        public void Draw(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            p.Width = 5;
            g.DrawLine(p, StartPoint, EndPoint);
        }
    }
}
