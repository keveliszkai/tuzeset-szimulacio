using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatTransferSimulation.Models
{
    class EmergencyCheckpoint
    {
        public PointF Position { get; set; }
        public EmergencyCheckpoint NextPoint { get; set; }

        public EmergencyCheckpoint(PointF position, EmergencyCheckpoint nextPoint)
        {
            Position = position;
            NextPoint = nextPoint;
        }

        public float getDistance(PointF from)
        {
            return (float)Vector.GetDistanceBetweenPoints(Position, from);
        }
    }
}
