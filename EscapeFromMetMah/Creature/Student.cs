using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    class Student : ICreature
    {
        public Status Status { get; set; }
        public readonly Dialogue Dialogue;
        
        public Student(Dialogue dialogue)
        {
            Dialogue = dialogue;
        }

        public Status GetStatus() => Status;

        public CreatureCommand Act(Level level, int x, int y)
        {
            // Тут должен быть супер мега крутой алгоритм поиска игрока и движения за ним
            return new CreatureCommand();
        }

        public bool IsConflict(ICreature conflictedObject) => conflictedObject is Player;
    }
}
