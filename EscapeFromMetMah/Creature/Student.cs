using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EscapeFromMetMah
{
    class Student : ICreature
    {
        public Status Status { get; set; }
        public readonly Dialogue Dialogue;
        public Point Location { get; set; }
        
        public Student(Dialogue dialogue, Point location)
        {
            Dialogue = dialogue;
            Status = Status.Active;
            Location = location;
        }

        public Status GetStatus() => Status;

        public CreatureCommand Act(Level level, int x, int y)
        {
            var random = new Random();
            var next = random.Next(-1, 2);
            if (x + next >= 0 && x + next <= level.Width - 1 &&
                !level.Map[x - 1, y].Any(x => x is Terrain) &&
                level.Map[x + next, y + 1].Any(x => x is Terrain || x is Stairs))
                return new CreatureCommand { deltaX = next };
            // Тут должен быть супер мега крутой алгоритм поиска игрока и движения за ним
            return new CreatureCommand();
        }

        public bool IsConflict(ICreature conflictedObject) => conflictedObject is Player;
    }
}
