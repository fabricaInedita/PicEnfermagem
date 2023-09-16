using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Application.DTOs.Response;

public sealed class QuestionResponse
{
    public string Statement { get; set; }
    public CategoryResponse Category { get; set; }
    public ICollection<AlternativeResponse> Alternatives { get; set; }
}
