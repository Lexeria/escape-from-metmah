using System.Linq;
using System.Windows.Forms;

namespace EscapeFromMetMah
{
    class Player : ICreature
    {
        public Status Status { get; private set; }

        public CreatureCommand Act(Level level, int x, int y)
        {
            if (level.KeyPressed == Keys.R && !level.Map[x, y].Any(x => x is Python))
                level.Map[x, y].Add(new Python());
            if (y + 1 <= level.Height - 1 &&
                !level.Map[x, y + 1].Any(x => x is Terrain || x is Stairs))
                return new CreatureCommand { DeltaY = 1 };

            if (level.KeyPressed == Keys.Up && y - 1 >= 0 &&
                level.Map[x, y].Any(x => x is Stairs) &&
                !level.Map[x, y - 1].Any(x => x is Terrain))
                return new CreatureCommand { DeltaY = -1 };

            if (level.KeyPressed == Keys.Down && y + 1 <= level.Height - 1 &&
                level.Map[x, y + 1].Any(x => x is Stairs))
                return new CreatureCommand { DeltaY = 1 };

            if (level.KeyPressed == Keys.Left && x - 1 >= 0 &&
                !level.Map[x - 1, y].Any(x => x is Terrain))
                return new CreatureCommand { DeltaX = -1 };

            if (level.KeyPressed == Keys.Right && x + 1 <= level.Width - 1 &&
                !level.Map[x + 1, y].Any(x => x is Terrain))
                return new CreatureCommand { DeltaX = 1 };

            return new CreatureCommand();
        }

        public Status GetStatus() => Status;

        public bool IsConflict(ICreature conflictedObject) =>
            conflictedObject is Student || conflictedObject is Beer;
    }
}