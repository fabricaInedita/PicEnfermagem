using System.Text.Json.Serialization;

namespace PicEnfermagem.Application.DTOs.Answer;

public sealed class AnswerResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int QuestionId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double Punctuation { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int AlternativeCorrectId { get; set; }
}
