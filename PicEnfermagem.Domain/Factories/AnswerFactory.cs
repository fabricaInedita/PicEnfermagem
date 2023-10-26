using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class AnswerFactory
{
    public static Answer Create(int questionId, double punctuation, int time)
    {
        return new Answer(questionId, punctuation, time);  
    }
}
