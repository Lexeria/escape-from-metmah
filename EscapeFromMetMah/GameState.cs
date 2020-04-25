using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EscapeFromMetMah
{
    class GameState
    {
        private Level CurrentLevel;
        private List<Level> Levels;
        public List<CreatureAnimation> Animations = new List<CreatureAnimation>();

        public GameState(List<Level> levels)
        {
            Levels = levels;
        }

        public void BeginAct()
        {
            Animations.Clear();
            for (var x = 0; x < CurrentLevel.Width; x++)
                for (var y = 0; y < CurrentLevel.Height; y++)
                {
                    var creatures = CurrentLevel.Map[x, y];
                    if (creatures == null) continue;

                    for (int i = 0; i < creatures.Count; i++)
                    {
                        var command = creatures[i].Act(CurrentLevel, x, y);

                        if (x + command.deltaX < 0 || x + command.deltaX >= CurrentLevel.Width || y + command.deltaY < 0 ||
                            y + command.deltaY >= CurrentLevel.Height)
                            throw new Exception($"The object {creatures[i].GetType()} falls out of the game field");

                        Animations.Add(
                            new CreatureAnimation
                            {
                                Command = command,
                                Creature = creatures[i],
                                TargetLogicalLocation = new Point(x + command.deltaX, y + command.deltaY)
                            });
                    }
                }

            // Потом в визуализации нужно продумать приоритет отрисовки и отсортировать Animations
        }

        public void EndAct()
        {
            var creaturesPerLocation = GetCandidatesPerLocation();
            for (var x = 0; x < CurrentLevel.Width; x++)
                for (var y = 0; y < CurrentLevel.Height; y++)
                    CurrentLevel.Map[x, y] = SelectWinnerCandidatePerLocation(creaturesPerLocation, x, y);
        }

        private List<ICreature> SelectWinnerCandidatePerLocation(List<ICreature>[,] creatures, int x, int y)
        {
            var candidates = creatures[x, y];
            var aliveCandidates = candidates.ToList();
            foreach (var candidate in candidates)
                foreach (var rival in candidates)
                    if (rival != candidate && candidate.IsConflict(rival))
                    {
                        // Решение всех возможных конфликтов
                        if (candidate is Pivo && rival is Player)
                        {
                            aliveCandidates.Remove(candidate);
                            CurrentLevel.countPivo -= 1;
                        }

                        if (candidate is Player && rival is Bot)
                        {
                            // Придумать, как выкидывать диалог со студентом.
                        }
                    }

            return aliveCandidates;
        }

        private List<ICreature>[,] GetCandidatesPerLocation()
        {
            // Кандидаты на нахождение в этой клетке
            var creatures = new List<ICreature>[CurrentLevel.Width, CurrentLevel.Height];
            for (var x = 0; x < CurrentLevel.Width; x++)
                for (var y = 0; y < CurrentLevel.Height; y++)
                    creatures[x, y] = new List<ICreature>();
            foreach (var e in Animations)
            {
                var x = e.TargetLogicalLocation.X;
                var y = e.TargetLogicalLocation.Y;
                creatures[x, y].Add(e.Creature);
            }

            return creatures;
        }
    }
}
