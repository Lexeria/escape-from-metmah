using System;
using System.Collections.Generic;

namespace EscapeFromMetMah
{
    public class Dialogue
    {
        public readonly string Text;
        private readonly string[] answers;
        private readonly string CorrectAnswer;

        public Dialogue(string text, string[] answers, string correctAnswer)
        {
            Text = text;
            this.answers = answers;
            CorrectAnswer = correctAnswer;
        }

        public Dialogue(string text, string[] answers, int index)
        {
            Text = text;
            this.answers = answers;
            CorrectAnswer = answers[index];
        }

        public IEnumerable<string> GetAnswers()
        {
            foreach (var e in answers)
                yield return e;
        }

        public bool IsTrueAnswer(string answer) => answer == CorrectAnswer;
        public bool IsTrueAnswer(int index)
        {
            if (index < 0 || index >= answers.Length)
                throw new ArgumentException();
            return answers[index] == CorrectAnswer;
        }
    }
}
