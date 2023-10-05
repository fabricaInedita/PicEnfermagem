using System.ComponentModel.DataAnnotations;

namespace PicEnfermagem.Application.DTOs.Insert;

public sealed class LoginRequest
{
    [Required(ErrorMessage = "O username deve ser informado.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "A senha deve ser informada.")]
    public string Password { get; set; }
}
