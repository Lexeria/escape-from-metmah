using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromMetMah
{
    public class Snake : ICreature
    {
        public Status Status { get; private set; }
        public CreatureCommand Act(Level level, int x, int y) => new CreatureCommand();

        public Status GetStatus() => Status;

        public bool IsConflict(ICreature conflictedObject) => false;
    }
}
