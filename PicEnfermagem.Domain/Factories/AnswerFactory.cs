using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class AnswerFactory
{
    public static Answer Create(int questionId,int punctuation)
    {
        return new Answer(questionId, punctuation);  
    }
}
