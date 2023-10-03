using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class AnswerFactory
{
    public static Answer Create(int questionId, bool isCorrectAnswer, int secondsAnswer)
    {
        return new Answer(questionId, isCorrectAnswer, secondsAnswer);  
    }
}
