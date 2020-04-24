using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    class Player : ICreature
    {
        public readonly string Name;
        public Status Status { get; set; }
        public double ProcentBurnout { get; private set; }
        public Superpower Superpower { get; private set; }
        public Point Location { get; private set; }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public Point GetLocation() => Location;

        public string GetName() => Name;

        public Status GetStatus() => Status;
    }
}
