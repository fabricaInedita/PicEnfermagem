namespace PicEnfermagem.Application.DTOs.User;

public sealed class ConfirmEmail
{
    public string Token { get; set; }
    public string UserId { get; set; }
}
