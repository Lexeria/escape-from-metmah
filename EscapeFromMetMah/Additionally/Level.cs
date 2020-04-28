using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EscapeFromMetMah
{
    public class Level
    {
        public readonly List<ICreature>[,] Map; // Лист, потому что в одной клетке могут находиться несколько сущностей.
        public bool IsOver => CountBeer() == 0;
        public readonly int Width;
        public readonly int Height;
        public Keys KeyPressed; // Для управления игроком на уровне.

        public Level(List<ICreature>[,] map)
        {
            Map = map;
            Width = Map.GetLength(0);
            Height = Map.GetLength(1);
        }

        public Level(string map)
        {
            Map = MapCreator.CreateMap(map);
            Width = Map.GetLength(0);
            Height = Map.GetLength(1);
        }

        public int CountBeer()
        {
            var countBeer = 0;
            for (int x = 0; x < Width; x++)
                for (int y = 0; x < Height; y++)
                    if (Map[x, y].Any(x => x is Beer))
                        countBeer++;
            return countBeer;
        }
    }
}
