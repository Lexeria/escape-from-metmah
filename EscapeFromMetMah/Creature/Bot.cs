using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    class Bot : ICreature
    {
        private readonly string Name;
        public Status Status { get; set; }
        public readonly Dialogue Dialogue;
        public Point Location { get; private set; }
        
        public Bot(string name, Dialogue dialogue, Point location)
        {
            Name = name;
            Dialogue = dialogue;
            Location = location;
        }

        public string GetName() => Name;

        public Point GetLocation() => Location;

        public Status GetStatus() => Status;

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }
    }
}
