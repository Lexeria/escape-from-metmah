using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EscapeFromMetMah
{
    class Player : ICreature
    {
        public readonly string Name;
        public Status Status { get; set; }
        public double ProcentBurnout { get; private set; }
        public Superpower Superpower { get; private set; }
        public Point Location { get; private set; }

        public CreatureCommand Act(Level level, int x, int y)
        {
            if (y + 1 <= level.Height - 1 && !level.Map[x, y + 1].Any())
                return new CreatureCommand { deltaY = 1 };

            if (level.keyPressed == Keys.Up && y - 1 >= 0 && level.Map[x, y].Any(x => x is Stairs))
                return new CreatureCommand { deltaY = -1 };

            if (level.keyPressed == Keys.Down && y + 1 <= level.Height - 1 && level.Map[x, y + 1].Any(x => x is Stairs))
                return new CreatureCommand { deltaY = 1 };

            if (level.keyPressed == Keys.Left && x - 1 >= 0 && !level.Map[x - 1, y].Any(x => x is Terrain))
                return new CreatureCommand { deltaX = -1 };

            if (level.keyPressed == Keys.Right && x + 1 <= level.Width - 1 && !level.Map[x + 1, y].Any(x => x is Terrain))
                return new CreatureCommand { deltaX = 1 };

            return new CreatureCommand();
        }

        public Point GetLocation() => Location;

        public string GetName() => Name;

        public Status GetStatus() => Status;

        public bool IsConflict(ICreature conflictedObject) =>
            conflictedObject is Bot || conflictedObject is Pivo;
    }
}
