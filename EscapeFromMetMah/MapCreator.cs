using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EscapeFromMetMah
{
    public static class MapCreator
    {
        public static List<ICreature>[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Select(z => z.Length).Distinct().Count() != 1)
                throw new Exception($"Wrong map '{map}'");
            var result = new List<ICreature>[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
                for (var y = 0; y < rows.Length; y++)
                    result[x, y] = new List<ICreature> { CreateCreatureBySymbol(rows[y][x], x, y) };
            return result;
        }

        private static ICreature CreateCreatureBySymbol(char symbol, int x, int y)
        {
            switch (symbol)
            {
                case 'P':
                    return new Player();
                case 'T':
                    return new Terrain();
                case 'L':
                    return new Stairs();
                case 'S':
                    // Здесь должно быть рандомное присвоение диалога
                    return new Student(new Dialogue("Do you love me?",
                                                new List<string> { "Yes", "No" },
                                                "Yes"),
                                       new Point(x, y));
                case 'B':
                    return new Beer();
                case ' ':
                    return null;
                default:
                    throw new Exception($"wrong character for ICreature {symbol}");
            }
        }
    }
}
