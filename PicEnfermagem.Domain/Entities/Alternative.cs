using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class Alternative : EntityCore
{
    public string Option { get; private set; }
    public string Description { get; private set; }
    public bool IsCorrect { get; private set; }
    public Question Question { get; private set; }
    public int QuestionId { get; private set; }
    internal Alternative(string option, string description, bool isCorrect)
    {
        Option = option;
        Description = description;
        IsCorrect = isCorrect;
    }
    private Alternative() { }
}
