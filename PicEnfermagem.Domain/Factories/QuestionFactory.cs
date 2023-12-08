using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class QuestionFactory
{
    public static Question Create(string statement, string explanation, ICollection<Alternative> alternatives, Category category, int maxPunctuation, int minPunctuation, string difficulty)
    {
        return new Question(statement, explanation, alternatives, category, maxPunctuation, minPunctuation, difficulty);
    }
}
