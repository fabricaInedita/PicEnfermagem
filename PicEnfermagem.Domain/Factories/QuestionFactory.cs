using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class QuestionFactory
{
    public static Question Create(string statement, ICollection<Alternative> alternatives, Category category)
    {
        return new Question(statement, alternatives, category);
    }
}
