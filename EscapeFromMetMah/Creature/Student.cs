using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EscapeFromMetMah
{
    class Student : ICreature
    {
        public Status Status { get; private set; }
        public readonly Dialogue Dialogue;
        
        public Student(Dialogue dialogue)
        {
            Dialogue = dialogue;
            Status = Status.Active;
        }

        public Status GetStatus() => Status;

        public CreatureCommand Act(Level level, int x, int y)
        {
            if (Status == Status.Inactive)
                return new CreatureCommand();
            var random = new Random();
            var next = random.Next(-1, 2);
            if (x + next >= 0 && x + next <= level.Width - 1 &&
                !level.Map[x + next, y].Any(x => x is Terrain) &&
                level.Map[x + next, y + 1].Any(x => x is Terrain || x is Stairs))
                return new CreatureCommand { deltaX = next };
            return new CreatureCommand();
        }

        public bool IsConflict(ICreature conflictedObject)
        {
            if (Status == Status.Active && (conflictedObject is Player || conflictedObject is Snake))
            {
                Status = Status.Inactive;
                return true;
            }

            return false;
        }
    }
}
