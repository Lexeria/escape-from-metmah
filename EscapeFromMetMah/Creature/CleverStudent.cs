using System.Drawing;
using System.Linq;

namespace EscapeFromMetMah
{
    class CleverStudent : ICreature
    {
        public Status Status { get; private set; }
        public readonly Dialogue Dialogue;

        public CleverStudent(Dialogue dialogue)
        {
            Dialogue = dialogue;
            Status = Status.Active;
        }

        public Status GetStatus() => Status;

        private static Point? FindPlayer(Level level)
        {
            Point? coordinatePlayer = null;
            for (int x = 0; x < level.Width; x++)
                for (int y = 0; y < level.Height; y++)
                    if (level.Map[x, y].Any(creature => creature is Player))
                        coordinatePlayer = new Point(x, y);
            return coordinatePlayer;
        }

        public Move Act(Level level, int x, int y)
        {
            if (Status == Status.Inactive)
                return new Move();
            var player = FindPlayer(level);
            if (player is null)
                return new Move();
            var path = Bfs.FindPaths(level.Map, new Point(x, y), new Point(player.Value.X, player.Value.Y));
            if (path is null)
                return new Move();
            return new Move() { DeltaX = path.Previous.Value.X - x, DeltaY = path.Previous.Value.Y - y };
        }

        public bool IsConflict(ICreature conflictedObject)
        {
            if (Status == Status.Active && (conflictedObject is Player || conflictedObject is Python))
            {
                Status = Status.Inactive;
                return true;
            }

            return false;
        }
    }
}
