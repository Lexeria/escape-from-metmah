using System;
using System.Collections.Generic;
using System.Linq;

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
                {
                    var creature = CreateCreatureBySymbol(rows[y][x]);
                    result[x, y] = new List<ICreature>();
                    if (creature != null)
                        result[x, y].Add(creature);
                }
            return result;
        }

        private static ICreature CreateCreatureBySymbol(char symbol)
        {
            return symbol switch
            {
                'P' => new Player(),
                'T' => new Terrain(),
                'L' => new Stairs(),
                'S' => new Student(new Dialogue("Do you love me?",
                                                new string[] { "Yes", "No" },
                                                "Yes")),
                'C' => new CleverStudent(new Dialogue("Do you love me?",
                                                new string[] { "Yes", "No" },
                                                "Yes")),
                'B' => new Beer(),
                ' ' => null,
                _ => throw new Exception($"wrong character for ICreature {symbol}"),
            };
        }
    }
}
