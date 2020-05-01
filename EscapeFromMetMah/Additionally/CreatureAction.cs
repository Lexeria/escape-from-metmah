using System.Drawing;

namespace EscapeFromMetMah
{
    public class CreatureAction
    {
        public Move Command;
        public ICreature Creature;
        public Point Location;
        public Point TargetLogicalLocation;
    }
}
