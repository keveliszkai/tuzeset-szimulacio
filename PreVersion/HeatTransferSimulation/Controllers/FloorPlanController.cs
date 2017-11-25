using HeatTransferSimulation.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatTransferSimulation.Controllers
{
    class FloorPlanController
    {
        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<Door> Doors { get; set; } = new List<Door>();
        public List<Wall> Walls { get; set; } = new List<Wall>();

        public List<EmergencyCheckpoint> EmergencyRoute { get; set; } = new List<EmergencyCheckpoint>();


        public void Boot()
        {
            Rooms = new List<Room>();

            Room mainRoom = new Room(new PointF(0, 0), new PointF(750, 450));
            mainRoom.isMainRoom = true;
            mainRoom.AddDoor(Room.DoorPosition.Right, 50);

            Room room1 = new Room(new PointF(0, 0), new PointF(150, 150));
            room1.AddDoor(Room.DoorPosition.Down, 50);

            Room room2 = new Room(new PointF(150, 0), new PointF(300, 150));
            room2.AddDoor(Room.DoorPosition.Down, 50);

            Room room3 = new Room(new PointF(500, 0), new PointF(750, 150));
            room3.AddDoor(Room.DoorPosition.Down, 50);

            Room room4 = new Room(new PointF(0, 300), new PointF(150, 450));
            room4.AddDoor(Room.DoorPosition.Right, 50);

            Room room5 = new Room(new PointF(300, 300), new PointF(550, 450));
            room5.AddDoor(Room.DoorPosition.Left, 50);

            Room room6 = new Room(new PointF(550, 300), new PointF(750, 450));
            room6.AddDoor(Room.DoorPosition.Up, 50);

            Rooms.Add(mainRoom);
            Rooms.Add(room1);
            Rooms.Add(room2);
            Rooms.Add(room3);
            Rooms.Add(room4);
            Rooms.Add(room5);
            Rooms.Add(room6);

            EmergencyCheckpoint ec6 = new EmergencyCheckpoint(new PointF(750, 225), null);
            EmergencyCheckpoint ec5 = new EmergencyCheckpoint(new PointF(640, 225), ec6);
            EmergencyCheckpoint ec4 = new EmergencyCheckpoint(new PointF(400, 215), ec5);
            EmergencyCheckpoint ec3 = new EmergencyCheckpoint(new PointF(225, 225), ec4);
            EmergencyCheckpoint ec2 = new EmergencyCheckpoint(new PointF(75, 225), ec3);
            EmergencyCheckpoint ec1 = new EmergencyCheckpoint(new PointF(225, 350), ec3);

            EmergencyRoute.Add(ec6);
            EmergencyRoute.Add(ec5);
            EmergencyRoute.Add(ec4);
            EmergencyRoute.Add(ec3);
            EmergencyRoute.Add(ec2);
            EmergencyRoute.Add(ec1);

            Rooms.ForEach(room => {
                Doors.AddRange(room.Doors);
                Walls.AddRange(room.Walls);
            });
        }

        public void Draw(Graphics g)
        {
            Rooms.ForEach(room => room.Draw(g));
        }
    }
}
