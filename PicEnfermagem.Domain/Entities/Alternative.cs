using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class Alternative : EntityCore
{
    public string Option { get; private set; }
    public bool IsCorrect { get; private set; }
    public Question Question { get; private set; }
    public int QuestionId { get; private set; }
    internal Alternative(string option, bool isCorrect)
    {
        Option = option;
        IsCorrect = isCorrect;
    }
    private Alternative() { }
}
