namespace PicEnfermagem.Application.DTOs.Response;

public sealed class AnswerResponse
{
    public int QuestionId { get; set; }
    public bool IsCorrectAnswer { get; set; }
    public int SecondsAnswer { get; set; }
}
