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

        public CreatureCommand Act(Level level, int x, int y)
        {
            // Тут должен быть супер мега крутой алгоритм поиска игрока и движения за ним
            return new CreatureCommand();
        }

        public bool IsConflict(ICreature conflictedObject) => conflictedObject is Player;
    }
}
