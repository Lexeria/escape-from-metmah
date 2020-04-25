using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromMetMah
{
    public class Dialogue
    {
        public readonly string Text;
        public List<string> Answers { get; private set; }
        private readonly string CorrectAnswer;

        public Dialogue(string text, List<string> answers, string correctAnswer)
        {
            Text = text;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }

        public Dialogue(string text, List<string> answers, int index)
        {
            Text = text;
            Answers = answers;
            CorrectAnswer = answers[index];
        }

        public bool IsTrueAnswer(string answer) => answer == CorrectAnswer;
        public bool IsTrueAnswer(int index)
        {
            if (index < 0 || index >= Answers.Count)
                throw new ArgumentException();
            return Answers[index] == CorrectAnswer;
        }
    }
}
