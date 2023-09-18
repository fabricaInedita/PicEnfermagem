namespace PicEnfermagem.Application.DTOs.Response;

public sealed class UserRegisterResponse
{
    public bool Success => Errors.Message.Count == 0 ? true : false;

    public Errors Errors { get; set; } = new Errors();
    public string SucessMessage { get; set; }


    public UserRegisterResponse(bool success)
    {
    }

}

