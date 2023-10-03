namespace PicEnfermagem.Application.DTOs.Response;

public record AlternativeResponse
{
    public string Option { get; set; }
    public string Description { get; set; }
    public bool IsCorrect { get; set; }
}
