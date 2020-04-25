using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    class Stairs : ICreature
    {
        public Status Status { get; set; }

        public CreatureCommand Act(Level level, int x, int y)
        {
            return new CreatureCommand();
        }

        public Status GetStatus() => Status;

        public bool IsConflict(ICreature conflictedObject) => false;
    }
}
