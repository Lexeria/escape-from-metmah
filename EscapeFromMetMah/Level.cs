using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EscapeFromMetMah
{
    public class Level
    {
        public readonly List<ICreature>[,] Map; // Лист, потому что в одной клетке могут находиться несколько сущностей.
        public bool IsOver => CountBeer < 1;
        public int CountBeer { get; set; }
        public readonly int Width;
        public readonly int Height;
        public Keys KeyPressed; // Для управления игроком на уровне.
        public Dialogue CurrentDialogue { get; set; }

        public Level(List<ICreature>[,] map, int countBeer)
        {
            CountBeer = countBeer;
            Map = map;
            Width = Map.GetLength(0);
            Height = Map.GetLength(1);
        }

        public Level(string map, int countBeer)
        {
            CountBeer = countBeer;
            Map = MapCreator.CreateMap(map);
            Width = Map.GetLength(0);
            Height = Map.GetLength(1);
        }
    }
}
