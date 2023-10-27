using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class Answer : EntityCore
{
    public double AnswerTime { get; private set; } 
    public int QuestionId { get; private set; }
    public double Punctuation {  get; private set; }
    public ApplicationUser? User { get; private set; }
    public string? UserId { get; set; }

    internal Answer(int questionId, double punctuation, double answerTime)
    {
        QuestionId = questionId;
        Punctuation = punctuation;
        AnswerTime = answerTime;
    }
    private Answer() { }
}
