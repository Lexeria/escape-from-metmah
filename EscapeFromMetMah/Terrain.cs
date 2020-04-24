using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    class Terrain : ICreature
    {
        public Point Location { get; set; }
        public Status Status { get; set; }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public Point GetLocation() => Location;

        public string GetName() => "Terratin";

        public Status GetStatus() => Status;
    }
}
