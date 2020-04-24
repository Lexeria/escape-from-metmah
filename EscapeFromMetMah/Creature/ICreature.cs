using System;
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
        CreatureCommand Act(int x, int y);
    }
}
