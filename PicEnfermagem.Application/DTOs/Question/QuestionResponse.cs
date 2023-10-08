using PicEnfermagem.Application.DTOs.Alternative;
using PicEnfermagem.Application.DTOs.Category;

namespace PicEnfermagem.Application.DTOs.Question;

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
    public double Punctuation { get; set; }
    public IEnumerable<QuestionResponse> QuestionResponses { get; set; }
}