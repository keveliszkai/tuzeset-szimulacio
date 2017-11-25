using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeatTransferSimulation.Models
{
    class Human
    {
        public PointF Position { get; set; }
        public PointF NextPosition { get; set; }
        public PointF Target { get; set; }
        public float Speed { get; set; } = .05F;
        public float Angle { get; set; } = 0;

        public float Temperature { get; set; }

        public bool Emergency = false;

        public float CollisionDistance { get; set; } = 20;

        public PointF Moving { get; set; }
        public ViewGraphic Graphic { get; set; }

        public Room Room { get; set; }

        private List<EmergencyCheckpoint> EmergencyRoute;
        private EmergencyCheckpoint EmergencyTarget;

        public Human(ViewGraphic vg)
        {
            Graphic = vg;
            Graphic.SetBitmap(vg.ImagePath);
        }

        public void SetEmergencyRoute(List<EmergencyCheckpoint> emergencyRoute)
        {
            this.EmergencyRoute = emergencyRoute;
        }

        public void SetTarget(PointF target)
        {
            Target = target;
            Moving = Vector.Subtract(Target, Position);
        }

        public void Calculate(List<Wall> walls, List<Door> doors)
        {
            if (Vector.GetDistanceBetweenPoints(Target, Position) > 2)
            {
                //Moving = Vector.Subtract(Target, Position);
                NextPosition = Vector.Add(Position, Vector.Multiply(Moving, Speed));
                Angle = Vector.GetAngle(Moving);

                if(Collision(walls, doors))
                {
                    NextPosition = Position;
                    Target = Position;
                }

                if(Temperature > 100 && !Emergency)
                {
                    PathFind();
                }

            }
            else if (Emergency)
            {
                if(EmergencyTarget == null)
                {
                    EmergencyTarget = EmergencyRoute
                        .OrderBy(i => i.getDistance(Position))
                        .FirstOrDefault();
                    
                    SetTarget(EmergencyTarget.Position);
                }
                else if(EmergencyTarget.NextPoint != null)
                {
                    EmergencyTarget = EmergencyTarget.NextPoint;
                    SetTarget(EmergencyTarget.Position);

                }
            }
            else
            {
                NextPosition = Position;
            }
        }

        public bool Collision(List<Wall> walls, List<Door> doors)
        {
            return walls.Any(
                w => w.getDistance(NextPosition) < CollisionDistance && 
                doors.All(
                    d => d.getDistance(NextPosition) != w.getDistance(NextPosition)
            ));
        }

        public void Draw(Graphics g)
        {
            Position = NextPosition;
            Graphic.Draw(g, NextPosition, Angle, false);
        }

        public void PathFind()
        {
            Emergency = true;

            if (Room != null && Vector.GetDistanceBetweenPoints(Room.Doors.FirstOrDefault().Center, Position) > CollisionDistance)
            {
                SetTarget(Room.Doors.FirstOrDefault().Center);
            }
            else
            {
                Room = null;

                EmergencyTarget = EmergencyRoute
                    .OrderBy(checkpoint => checkpoint.getDistance(Position))
                    .First();

                SetTarget(EmergencyTarget.Position);
            }
        }
    }
}
