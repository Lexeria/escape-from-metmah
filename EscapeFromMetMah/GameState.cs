﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EscapeFromMetMah
{
    public class GameState
    {
        public Level CurrentLevel { get; private set; }
        public int IndexCurrentLevel { get; private set; }
        private readonly List<Level> Levels;
        public List<CreatureAction> Actions { get; }
        public bool IsGameOver { get; private set; }
        public Dialogue CurrentDialogue { get; private set; }
        public bool IsDialogueActivated => CurrentDialogue != null;
        public int PatienceScale { get; private set; }
        public int WidthCurrentLevel => CurrentLevel.Width;
        public int HeightCurrentLevel => CurrentLevel.Height;

        public GameState(IEnumerable<Level> levels)
        {
            Levels = levels.ToList();
            CurrentLevel = Levels[0];
            Actions = new List<CreatureAction>();
            PatienceScale = CurrentLevel.Height * CurrentLevel.Width * 2;
        }

        public void SetKeyPressed(Keys key) => CurrentLevel.KeyPressed = key;

        public void BeginAct()
        {
            if (IsDialogueActivated)
                return;
            Actions.Clear();
            for (var x = 0; x < CurrentLevel.Width; x++)
                for (var y = 0; y < CurrentLevel.Height; y++)
                {
                    var creatures = CurrentLevel.GetCreatures(x, y).ToList();
                    if (creatures == null) continue;

                    for (int i = 0; i < creatures.Count; i++)
                    {
                        if (creatures[i] == null) continue;
                        var command = creatures[i].Act(CurrentLevel, x, y);

                        if (x + command.DeltaX < 0 || x + command.DeltaX >= CurrentLevel.Width || y + command.DeltaY < 0 ||
                            y + command.DeltaY >= CurrentLevel.Height)
                            throw new Exception($"The object {creatures[i].GetType()} falls out of the game field");

                        Actions.Add(
                            new CreatureAction
                            {
                                Command = command,
                                Creature = creatures[i],
                                Location = new Point(x, y),
                                TargetLogicalLocation = new Point(x + command.DeltaX, y + command.DeltaY)
                            });
                        // Если в клетке появились новые существа
                        creatures = CurrentLevel.GetCreatures(x, y).ToList();
                    }
                }

            // Потом в визуализации нужно продумать приоритет отрисовки и отсортировать Animations
        }

        public void EndAct()
        {
            IsGameOver = PatienceScale-- < 0;
            if (IsDialogueActivated)
            {
                PatienceScale -= 2;
                var index = (int)CurrentLevel.KeyPressed - 49;
                if (index < 0 || index >= CurrentDialogue.CountAnswers)
                    return;
                if (CurrentDialogue.IsCorrectAnswer(index))
                    CurrentDialogue = null;
                return;
            }
            var creaturesPerLocation = GetCandidatesPerLocation();
            for (var x = 0; x < CurrentLevel.Width; x++)
                for (var y = 0; y < CurrentLevel.Height; y++)
                    CurrentLevel.SetCreatures(x, y, SelectWinnerCandidatePerLocation(creaturesPerLocation, x, y));

            if (CurrentLevel.IsOver)
            {
                if (IndexCurrentLevel + 1 < Levels.Count)
                {
                    IndexCurrentLevel += 1;
                    CurrentLevel = Levels[IndexCurrentLevel];
                    PatienceScale = CurrentLevel.Height * CurrentLevel.Width * 2;
                }
                else
                    IsGameOver = true;
            }
        }

        private List<ICreature> SelectWinnerCandidatePerLocation(List<ICreature>[,] creatures, int x, int y)
        {
            var candidates = creatures[x, y];
            var aliveCandidates = candidates.ToList();
            foreach (var candidate in candidates)
                foreach (var rival in candidates)
                    if (rival != candidate && candidate.IsConflict(rival))
                        ResolvingConflict(aliveCandidates, candidate, rival);

            return aliveCandidates;
        }

        private void ResolvingConflict(List<ICreature> aliveCandidates, ICreature candidate, ICreature rival)
        {
            if (candidate is Beer && rival is Player)
                aliveCandidates.Remove(candidate);

            if (candidate is Student && rival is Player)
                CurrentDialogue = (candidate as Student).Dialogue;

            if (candidate is CleverStudent && rival is Player)
                CurrentDialogue = (candidate as CleverStudent).Dialogue;
        }

        private List<ICreature>[,] GetCandidatesPerLocation()
        {
            // Кандидаты на нахождение в этой клетке
            var creatures = new List<ICreature>[CurrentLevel.Width, CurrentLevel.Height];
            for (var x = 0; x < CurrentLevel.Width; x++)
                for (var y = 0; y < CurrentLevel.Height; y++)
                    creatures[x, y] = new List<ICreature>();
            foreach (var e in Actions)
            {
                var x = e.TargetLogicalLocation.X;
                var y = e.TargetLogicalLocation.Y;
                creatures[x, y].Add(e.Creature);
            }

            return creatures;
        }
    }
}
