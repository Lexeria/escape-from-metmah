﻿using System;
using System.Linq;

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

        public Move Act(Level level, int x, int y)
        {
            if (Status == Status.Inactive)
                return new Move();
            var random = new Random();
            var next = random.Next(-1, 2);
            if (x + next >= 0 && x + next <= level.Width - 1 &&
                !level.Map[x + next, y].Any(creature => creature is Terrain) &&
                level.Map[x + next, y + 1].Any(creature => creature is Terrain || creature is Stairs))
                return new Move { DeltaX = next };
            return new Move();
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
