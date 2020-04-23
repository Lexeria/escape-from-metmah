using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EscapeFromMetMah
{
    class Bot
    {
        public Status Status { get; set; }
        public readonly Dialogue Dialogue;
        public Vector Location { get; private set; }
        
        public Bot(Dialogue dialogue, Vector location)
        {
            Dialogue = dialogue;
            Location = location;
        }

        // Возможны методы на передвижение
    }
}
