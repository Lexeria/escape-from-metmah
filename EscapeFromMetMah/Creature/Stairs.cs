using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    class Stairs : ICreature
    {
        public Point Location { get; set; }
        public Status Status { get; set; }

        public CreatureCommand Act(Level level, int x, int y)
        {
            return new CreatureCommand();
        }

        public Point GetLocation() => Location;

        public string GetName() => "Stairs";

        public Status GetStatus() => Status;

        public bool IsConflict(ICreature conflictedObject) => false;
    }
}
