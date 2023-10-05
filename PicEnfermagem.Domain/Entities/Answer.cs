using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class Answer : EntityCore
{
    public int QuestionId { get; private set; }
    public int Punctuation {  get; private set; }
    public ApplicationUser? User { get; private set; }
    public string? UserId { get; set; }
    internal Answer(int questionId, int punctuation)
    {
        QuestionId = questionId;
        Punctuation = punctuation;
    }
    private Answer() { }
}
