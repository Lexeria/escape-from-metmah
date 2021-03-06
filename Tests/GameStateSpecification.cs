﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using EscapeFromMetMah;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    class GameStateSpecification
    {
        [TestCase(new[] { Keys.Right }, 1, 0)]
        [TestCase(new[] { Keys.Left }, 0, 0)]
        [TestCase(new[] { Keys.Down }, 0, 0)]
        [TestCase(new[] { Keys.Up }, 0, 0)]
        [TestCase(new[] { Keys.None }, 0, 0)]
        [TestCase(new[] { Keys.Right, Keys.Right }, 2, 0)]
        [TestCase(new[] { Keys.Right, Keys.Right, Keys.Down }, 2, 1)]
        [TestCase(new[] { Keys.Right, Keys.Right, Keys.Down, Keys.Down }, 2, 2)]
        [TestCase(new[] { Keys.Right, Keys.Right, Keys.Down, Keys.Down, Keys.Left }, 1, 2)]
        [TestCase(new[] { Keys.Right, Keys.Right, Keys.Down, Keys.Down, Keys.Left, Keys.Left }, 0, 2)]
        public void GameState_PlayerShouldMoveCorrect(Keys[] keys, int finishX, int finishY)
        {
            var levelOne = new Level("P  \r\nTTL\r\nB L\r\nTTT");
            var levelTwo = new Level("P B  \r\nTTTTL\r\n    L\r\nC S L\r\nTTTTT");
            var levels = new List<Level> { levelOne, levelTwo };
            var gameState = new GameState(levels);

            foreach(var key in keys)
            {
                gameState.SetKeyPressed(key);
                gameState.BeginAct();
                gameState.EndAct();
            }

            var location = gameState.Actions.Where(x => x.Creature is Player).First().TargetLogicalLocation;
            location.X.Should().Be(finishX);
            location.Y.Should().Be(finishY);
        }

        [Test]
        public void GameState_ShouldGoNextLevel_AfterTakeLastBeer()
        {
            var levelOne = new Level("PB \r\nTTT");
            var levelTwo = new Level("P  \r\nTTT");
            var levels = new List<Level> { levelOne, levelTwo };
            var gameState = new GameState(levels);
            gameState.IndexCurrentLevel.Should().Be(0);
            gameState.SetKeyPressed(Keys.Right);
            gameState.BeginAct();
            gameState.EndAct();
            gameState.IndexCurrentLevel.Should().Be(1);
        }

        [Test]
        public void GameState_CleverStudentShouldReachPlayer()
        {
            var levels = new List<Level> { new Level("P B \r\nTTTL\r\n  CL\r\nTTTT") };
            var gameState = new GameState(levels);
            for (int i = 0; i < 6; i++)
            {
                gameState.BeginAct();
                gameState.EndAct();
            }
            gameState.CurrentDialogue.Should().NotBeNull();
            var creatures = gameState.CurrentLevel.GetCreatures(0, 0).ToList();
            creatures.Any(z => z is Player).Should().BeTrue();
            creatures.Any(z => z is CleverStudent).Should().BeTrue();
        }

        [Test]
        public void GameState_ShouldFinishGame_WhenLevelsExpire()
        {
            var levelOne = new Level("PB \r\nTTT");
            var levelTwo = new Level("PB \r\nTTT");
            var levels = new List<Level> { levelOne, levelTwo };
            var gameState = new GameState(levels);
            for (int i = 0; i < 2; i++)
            {
                gameState.SetKeyPressed(Keys.Right);
                gameState.BeginAct();
                gameState.EndAct();
            }
            gameState.IsGameOver.Should().BeTrue();
        }

        [Test]
        public void GameState_PlayerShouldPutPythonInCell()
        {
            var levels = new List<Level> { new Level("P B\r\nTTT")};
            var gameState = new GameState(levels);
            gameState.SetKeyPressed(Keys.R);
            gameState.BeginAct();
            gameState.EndAct();
            gameState.CurrentLevel.GetCreatures(0, 0).Any(x => x is Python).Should().BeTrue();
        }

        [Test]
        public void GameState_ShouldActivateDialogue_WhenConflictPlayerAndStudent()
        {
            var levels = new List<Level> { new Level("PCB\r\nTTT") };
            var gameState = new GameState(levels);
            gameState.BeginAct();
            gameState.EndAct();
            gameState.IsDialogueActivated.Should().BeTrue();
        }

        [Test]
        public void GameState_ShouldNotUpdateActions_WhenActivatedDialogue()
        {
            var levels = new List<Level> { new Level("PCB\r\nTTT") };
            var gameState = new GameState(levels);
            gameState.BeginAct();
            gameState.EndAct();
            gameState.IsDialogueActivated.Should().BeTrue();
            var actions = gameState.Actions;
            for (int i = 0; i < 5; i++)
            {
                gameState.BeginAct();
                gameState.EndAct();
            }
            gameState.Actions.Should().BeEquivalentTo(actions);
        }

        [Test]
        public void GameState_AllCreaturesShouldMove_AfterDeactivatedDialogue()
        {
            var levels = new List<Level> { new Level("PCB \r\nTTTL\r\nS CL\r\nTTTT") };
            var gameState = new GameState(levels);
            gameState.BeginAct();
            gameState.EndAct();
            gameState.IsDialogueActivated.Should().BeTrue();
            var dialogue = gameState.CurrentDialogue;
            var answers = dialogue.GetAnswers().ToList();
            for (int i = 0; i < answers.Count; i++)
            {
                if (dialogue.IsCorrectAnswer(i))
                {
                    gameState.SetKeyPressed((Keys)(i+49));
                    break;
                }
            }
            gameState.BeginAct();
            gameState.EndAct();
            gameState.IsDialogueActivated.Should().BeFalse();
        }

        [Test]
        public void GameState_GameShouldOver_WhenPatienceScaleLessZero()
        {
            var levels = new List<Level> { new Level("PCB \r\nTTTL\r\nS CL\r\nTTTT") };
            var gameState = new GameState(levels);
            for (int i = 0; i < gameState.HeightCurrentLevel * gameState.WidthCurrentLevel * 2; i++)
            {
                gameState.BeginAct();
                gameState.EndAct();
            }
            gameState.IsGameOver.Should().BeTrue();
        }
    }
}
