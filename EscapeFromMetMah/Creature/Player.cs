using System.Linq;
using System.Windows.Forms;

namespace EscapeFromMetMah
{
    class Player : ICreature
    {
        public Status Status { get; private set; }

        public Move Act(Level level, int x, int y)
        {
            if (level.KeyPressed == Keys.R && !level.Map[x, y].Any(creature => creature is Python))
                level.Map[x, y].Add(new Python());
            if (y + 1 <= level.Height - 1 &&
                !level.Map[x, y + 1].Any(creature => creature is Terrain || creature is Stairs))
                return new Move { DeltaY = 1 };

            if (level.KeyPressed == Keys.Up && y - 1 >= 0 &&
                level.Map[x, y].Any(creature => creature is Stairs) &&
                !level.Map[x, y - 1].Any(creature => creature is Terrain))
                return new Move { DeltaY = -1 };

            if (level.KeyPressed == Keys.Down && y + 1 <= level.Height - 1 &&
                level.Map[x, y + 1].Any(creature => creature is Stairs))
                return new Move { DeltaY = 1 };

            if (level.KeyPressed == Keys.Left && x - 1 >= 0 &&
                !level.Map[x - 1, y].Any(creature => creature is Terrain))
                return new Move { DeltaX = -1 };

            if (level.KeyPressed == Keys.Right && x + 1 <= level.Width - 1 &&
                !level.Map[x + 1, y].Any(creature => creature is Terrain))
                return new Move { DeltaX = 1 };

            return new Move();
        }

        public Status GetStatus() => Status;

        public bool IsConflict(ICreature conflictedObject) =>
            conflictedObject is Student || conflictedObject is Beer;
    }
}