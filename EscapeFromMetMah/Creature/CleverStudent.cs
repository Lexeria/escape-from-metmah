using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EscapeFromMetMah
{
    class CleverStudent : ICreature
    {
        public Status Status { get; private set; }
        public Dialogue Dialogue { get; private set; }
        private bool IsRest;

        public CleverStudent(Dialogue dialogue)
        {
            Dialogue = dialogue;
            Status = Status.Active;
        }

        public Status GetStatus() => Status;

        private Point? FindPlayer(Level level)
        {
            Point? coordinatePlayer = null;
            for (int x = 0; x < level.Width; x++)
                for (int y = 0; y < level.Height; y++)
                    if (level.Map[x, y].Any(x => x is Player))
                        coordinatePlayer = new Point(x, y);
            return coordinatePlayer;
        }

        public CreatureCommand Act(Level level, int x, int y)
        {
            if (Status == Status.Inactive)
                return new CreatureCommand();
            if (IsRest)
            {
                IsRest = !IsRest;
                return new CreatureCommand();
            }
            var player = FindPlayer(level);
            if (player is null)
                return new CreatureCommand();
            var path = Bfs.FindPaths(level.Map, new Point(x, y), new Point(player.Value.X, player.Value.Y));
            if (path is null)
                return new CreatureCommand();
            IsRest = !IsRest;
            return new CreatureCommand() { deltaX = path.Previous.Value.X - x, deltaY = path.Previous.Value.Y - y };
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
