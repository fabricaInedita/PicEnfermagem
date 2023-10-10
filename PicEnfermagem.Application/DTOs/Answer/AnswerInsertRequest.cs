namespace PicEnfermagem.Application.DTOs.Answer;

public sealed class AnswerInsertRequest
{
    public int QuestionId { get; set; }
    public int AlternativeId { get; set; }
    public double Punctuation { get; set; }
}
