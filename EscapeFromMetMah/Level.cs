using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EscapeFromMetMah
{
    public class Level
    {
        public List<ICreature>[,] Map; // Лист, потому что в одной клетке могут находиться несколько сущностей.
        public bool IsOver => CountBeer < 1;
        public int CountBeer { get; set; }
        public int Width => Map.GetLength(0);
        public int Height => Map.GetLength(1);
        public Keys keyPressed;

        public Level(List<ICreature>[,] Map, int countPivo)
        {
            CountBeer = countPivo;
            this.Map = Map;
            // Генерация карты?
        }
    }
}
