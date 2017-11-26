using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HumanWithMoving
{
    class Human
    {
        public PointF Position { get; set; }
        public PointF NextPosition { get; set; }
        public PointF Target { get; set; }
        public float Speed { get; set; } = .01F;
        public float Angle { get; set; } = 0;

        public PointF Moving { get; set; }
        public ViewGraphic Graphic { get; set; }

        public Human(ViewGraphic vg)
        {
            Graphic = vg;
            Graphic.SetBitmap(vg.ImagePath);
        }

        public void Calculate(List<Wall> walls, List<Door> doors)
        {
            if (Vector.GetDistanceBetweenPoints(Target, Position) > 2)
            {
                Moving = Vector.Subtract(Target, Position);
                NextPosition = Vector.Add(Position, Vector.Multiply(Moving, Speed));
                Angle = Vector.GetAngle(Moving);

                if(Collision(walls, doors))
                {
                    NextPosition = Position;
                    Target = Position;
                }

            } else
            {
                NextPosition = Position;
            }
        }

        private bool Collision(List<Wall> walls, List<Door> doors)
        {
            return walls.Any(
                w => w.getDistance(NextPosition) < 10 && 
                doors.All(
                    d => d.getDistance(NextPosition) != w.getDistance(NextPosition)
            ));
        }

        public void Draw(Graphics g)
        {
            Position = NextPosition;
            Graphic.Draw(g, NextPosition, Angle, true);
        }
    }
}
