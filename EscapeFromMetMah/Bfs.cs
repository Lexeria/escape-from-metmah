using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EscapeFromMetMah
{
    public static class Bfs
    {
        public class SinglyLinkedList<T> : IEnumerable<T>
        {
            public readonly T Value;
            public readonly SinglyLinkedList<T> Previous;
            public readonly int Length;

            public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
            {
                Value = value;
                Previous = previous;
                Length = previous?.Length + 1 ?? 1;
            }

            public IEnumerator<T> GetEnumerator()
            {
                yield return Value;
                var pathItem = Previous;
                while (pathItem != null)
                {
                    yield return pathItem.Value;
                    pathItem = pathItem.Previous;
                }
            }

            public SinglyLinkedList<T> Reverse()
            {
                var answer = new SinglyLinkedList<T>(Value);
                var next = Previous;
                while (next != null)
                {
                    answer = new SinglyLinkedList<T>(next.Value, answer);
                    next = next.Previous;
                }
                return answer;
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public static SinglyLinkedList<Point> FindPaths(List<ICreature>[,] map, Point start, Point finish)
        {
            if (start == finish)
                return null;
            var queue = new Queue<SinglyLinkedList<Point>>();
            var visited = new HashSet<Point>();
            queue.Enqueue(new SinglyLinkedList<Point>(start));
            var width = map.GetLength(0);
            var height = map.GetLength(1);

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var point = path.Value;
                if (!visited.Contains(point) && point.X >= 0 && point.X < width && point.Y >= 0 && point.Y < height - 1 &&
                    !map[point.X, point.Y].Any(x => x is Terrain) &&
                    (map[point.X, point.Y + 1].Any(x => x is Terrain || x is Stairs) ||
                    map[point.X, point.Y].Any(x => x is Stairs)))
                {
                    if (finish == point)
                        return path.Reverse();
                    visited.Add(point);
                    AddPoint(queue, path);
                }
            }
            return null;
        }

        private static void AddPoint(Queue<SinglyLinkedList<Point>> queue, SinglyLinkedList<Point> cell)
        {
            var point = cell.Value;
            for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                {
                    if (dx != 0 && dy != 0)
                        continue;
                    var newPoint = new Point(point.X + dx, point.Y + dy);
                    queue.Enqueue(new SinglyLinkedList<Point>(newPoint, cell));
                }
        }
    }
}
