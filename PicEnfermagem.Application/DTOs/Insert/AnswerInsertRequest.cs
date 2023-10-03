namespace PicEnfermagem.Application.DTOs.Insert;

public sealed class AnswerInsertRequest
{
    public int QuestionId { get; set; }
    public bool IsCorrectAnswer { get; set; }
    public int SecondsAnswer { get; set; }
}
