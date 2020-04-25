﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    class Beer : ICreature
    {
        public Status Status { get; set; }

        public CreatureCommand Act(Level level, int x, int y)
        {
            return new CreatureCommand();
        }

        public Status GetStatus() => Status;

        public bool IsConflict(ICreature conflictedObject) => conflictedObject is Player;
    }
}