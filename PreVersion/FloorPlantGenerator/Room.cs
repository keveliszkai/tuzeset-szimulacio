using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlantGenerator
{
    class Room
    {
        public List<Wall> Walls { get; set; }
        public List<Door> Doors { get; set; }

        private PointF Corner1 { get; set; }
        private PointF Corner2 { get; set; }

        public Room(PointF corner1, PointF corner2)
        {
            Corner1 = corner1;
            Corner2 = corner2;

            Walls = new List<Wall>();
            Doors = new List<Door>();

            Walls.Add(new Wall(corner1, new PointF(corner1.X, corner2.Y)));
            Walls.Add(new Wall(corner1, new PointF(corner2.X, corner1.Y)));

            Walls.Add(new Wall(corner2, new PointF(corner2.X, corner1.Y)));
            Walls.Add(new Wall(corner2, new PointF(corner1.X, corner2.Y)));
        }

        public enum DoorPosition {
            Left,
            Right,
            Up,
            Down
        }

        public void AddDoor(DoorPosition position, float width)
        {
            float horizontalWidth = Corner2.X - Corner1.X;
            float verticalWidth = Corner2.Y - Corner1.Y;

            switch (position)
            {
                case DoorPosition.Down:
                    this.Doors.Add(new Door(
                        new PointF(Corner1.X + horizontalWidth / 2 - width / 2, Corner2.Y),
                        new PointF(Corner1.X + horizontalWidth / 2 + width / 2, Corner2.Y)
                        ));
                    break;
                case DoorPosition.Up:
                    this.Doors.Add(new Door(
                        new PointF(Corner1.X + horizontalWidth / 2 - width / 2, Corner1.Y),
                        new PointF(Corner1.X + horizontalWidth / 2 + width / 2, Corner1.Y)
                        ));
                    break;
                case DoorPosition.Right:
                    this.Doors.Add(new Door(
                        new PointF(Corner2.X , Corner1.Y + verticalWidth / 2 - width / 2),
                        new PointF(Corner2.X, Corner1.Y + verticalWidth / 2 + width / 2)
                        ));
                    break;
                case DoorPosition.Left:
                    this.Doors.Add(new Door(
                        new PointF(Corner1.X, Corner1.Y + verticalWidth / 2 - width / 2),
                        new PointF(Corner1.X, Corner1.Y + verticalWidth / 2 + width / 2)
                        ));
                    break;
            }
        }

        public void Draw(Graphics g)
        {
            this.Walls.ForEach(i => i.Draw(g));
            this.Doors.ForEach(i => i.Draw(g));
        }
    }
}
