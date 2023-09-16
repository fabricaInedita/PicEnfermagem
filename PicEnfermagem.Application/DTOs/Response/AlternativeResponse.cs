namespace PicEnfermagem.Application.DTOs.Response;

public record AlternativeResponse
{
    public string Option { get; set; }
    public bool IsCorrect { get; set; }
}
