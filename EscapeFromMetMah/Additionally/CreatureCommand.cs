using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromMetMah
{
    // Действие объекта/что может измениться
    // Местоположение на дельту, может выскачить диалог,
    // Может поменяться статус
    public class CreatureCommand
    {
        public int deltaX;
        public int deltaY;
    }
}
