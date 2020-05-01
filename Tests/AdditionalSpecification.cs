using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using EscapeFromMetMah;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    class AdditionalSpecification
    {
        [Test]
        public void IsCorrectAnswer_ShouldReturnTrue_WhenAnswerCorrect()
        {
            var dialogue = new Dialogue("Do you love me?", new string[] { "No", "Yes" }, "Yes");
            dialogue.IsCorrectAnswer(1).Should().BeTrue();
            dialogue.IsCorrectAnswer("Yes").Should().BeTrue();
        }

        [Test]
        public void IsCorrectAnswer_ShouldReturnFalse_WhenAnswerIncorrect()
        {
            var dialogue = new Dialogue("Do you love me?", new string[] { "Yes", "No" }, 0);
            dialogue.IsCorrectAnswer(1).Should().BeFalse();
            dialogue.IsCorrectAnswer("No").Should().BeFalse();
        }
    }
}
