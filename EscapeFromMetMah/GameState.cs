using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace EscapeFromMetMah
{
    class GameState
    {
        public Level CurrentLevel { get; private set; }
        private int IndexCurrentLevel;
        private readonly List<Level> Levels;
        public List<CreatureAnimation> Animations { get; private set; }
        public bool IsGameOver { get; private set; }

        public GameState(List<Level> levels)
        {
            Levels = levels;
            CurrentLevel = levels[0];
            Animations = new List<CreatureAnimation>();
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
                        if (creatures[i] == null) continue;
                        var command = creatures[i].Act(CurrentLevel, x, y);

                        if (x + command.deltaX < 0 || x + command.deltaX >= CurrentLevel.Width || y + command.deltaY < 0 ||
                            y + command.deltaY >= CurrentLevel.Height)
                            throw new Exception($"The object {creatures[i].GetType()} falls out of the game field");

                        Animations.Add(
                            new CreatureAnimation
                            {
                                Command = command,
                                Creature = creatures[i],
                                Location = new Point(x, y),
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

            if (CurrentLevel.IsOver)
            {
                if (IndexCurrentLevel + 1 < Levels.Count)
                {
                    IndexCurrentLevel += 1;
                    CurrentLevel = Levels[IndexCurrentLevel];
                }
                else
                    IsGameOver = true;
            }

            if (IsGameOver)
                return;
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
                        ResolvingConflict(aliveCandidates, candidate, rival);
                    }

            return aliveCandidates;
        }

        private void ResolvingConflict(List<ICreature> aliveCandidates, ICreature candidate, ICreature rival)
        {
            if (candidate is Beer && rival is Player)
            {
                aliveCandidates.Remove(candidate);
                CurrentLevel.CountBeer -= 1;
            }

            if (candidate is Student && rival is Player)
            {
                // Придумать, как выкидывать диалог со студентом.
            }
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
