using System.Text.Json.Serialization;

namespace PicEnfermagem.Application.DTOs.User;

public sealed class UserResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Email { get; set; }
    public double Punctuation { get; set; }
    public string Name { get; set; }
}