﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    public interface ICreature
    {
        string GetName();
        Point GetLocation();
        Status GetStatus();
        bool IsConflict(ICreature conflictedObject);
        CreatureCommand Act(Level level, int x, int y);
    }
}
