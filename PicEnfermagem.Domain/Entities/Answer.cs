using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class Answer : EntityCore
{
    public int QuestionId { get; private set; }
    public bool IsCorrectAnswer { get; private set; }
    public int SecondsAnswer {  get; private set; }
    public ApplicationUser? User { get; private set; }
    public string? UserId { get; set; }
    internal Answer(int questionId, bool isCorrectAnswer, int secondsAnswer)
    {
        QuestionId = questionId;
        IsCorrectAnswer = isCorrectAnswer;
        SecondsAnswer = secondsAnswer;
    }
    private Answer() { }
}
