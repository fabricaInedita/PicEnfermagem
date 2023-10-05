using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class QuestionFactory
{
    public static Question Create(string statement, ICollection<Alternative> alternatives, Category category, int maxPunctuation, int minPunctuation, string difficulty)
    {
        return new Question(statement, alternatives, category, maxPunctuation, minPunctuation, difficulty);
    }
}
