using HeatTransferSimulation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatTransferSimulation.Controllers
{
    class HumanController
    {
        private Random rnd = new Random();
        public List<Human> Humans { get; set; } = new List<Human>();

        private Stopwatch CalculationStopper = new Stopwatch();
        public long CalculationTime = 0;

        private int RandomCounter = 0;

        public void Boot(List<Wall> walls, List<Door> doors, int numberOfHumans) {
            ViewGraphic vg = new ViewGraphic();
            vg.ImagePath = Environment.CurrentDirectory + @"\..\..\Human.png";
            vg.Size = new PointF(24, 24);

            for (int i = 0; i < numberOfHumans; i++)
            {
                Human One = new Human(vg);

                do
                {
                    One.Name = string.Format("Human - {0}", i);
                    One.Position = new PointF(rnd.Next(0, 750), rnd.Next(0, 450));
                    One.Angle = rnd.Next(-180, 180);
                    One.NextPosition = One.Position;
                    One.Moving = One.Position;
                } while (One.Collision(walls, doors));

                One.SetTarget(One.Position);

                Humans.Add(One);
            }
        }

        public void Draw(Graphics g)
        {
            Humans.ForEach(human => human.Draw(g));
        }

        public void Calculate(List<Wall> walls, List<Door> doors, List<Room> rooms)
        {
            CalculationStopper.Restart();

            Humans.ForEach(human => human.Calculate(walls, doors));
            Humans.ForEach(human =>
            human.Room = rooms
            .Where(room => !room.isMainRoom)
            .Where(room => room.InRoom(human.Position))
            .FirstOrDefault()
            );

            CalculationStopper.Stop();
            CalculationTime = CalculationStopper.ElapsedTicks;
        }

        public void RandomEvent()
        {
            if (RandomCounter == 0)
            {
                RandomCounter = rnd.Next(0, 30);

                Human human = Humans[rnd.Next(0, Humans.Count - 1)];

                if (!human.Emergency)
                    human.SetTarget(new PointF(human.Position.X + rnd.Next(-100, 100), human.Position.Y + rnd.Next(-100, 100)));
            }
            else
            {
                RandomCounter--;
            }
        }
    }
}
