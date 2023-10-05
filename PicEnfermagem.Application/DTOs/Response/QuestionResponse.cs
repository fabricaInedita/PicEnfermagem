namespace PicEnfermagem.Application.DTOs.Response;

public sealed class QuestionResponse
{
    public int Id { get; set; }
    public string Statement { get; set; }
    public int MaxPunctuation { get; set; }
    public int MinPunctuation { get; set; }
    public string Difficulty { get; set; }
    public CategoryResponse Category { get; set; }
    public ICollection<AlternativeResponse> Alternatives { get; set; }
}
public sealed class QuestionResponseList
{
    public int Punctuation { get; set; }
    public IEnumerable<QuestionResponse> QuestionResponses { get; set; }
}