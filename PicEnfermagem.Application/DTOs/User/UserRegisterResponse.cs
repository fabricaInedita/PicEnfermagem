namespace PicEnfermagem.Application.DTOs.User;

public sealed class UserRegisterResponse
{
    public bool Success => Errors.Message.Count == 0 ? true : false;

    public Errors Errors { get; set; } = new Errors();
    public string SucessMessage { get; set; }

}

