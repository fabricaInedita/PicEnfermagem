using System.Text.Json.Serialization;

namespace PicEnfermagem.Application.DTOs.Alternative;

public record AlternativeResponse
{
    public int Id { get; set; }
    public string Option { get; set; }
    public string Description { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool IsCorrect { get; set; }
}
