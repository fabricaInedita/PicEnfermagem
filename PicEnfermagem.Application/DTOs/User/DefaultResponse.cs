namespace PicEnfermagem.Application.DTOs.User;

public sealed class DefaultResponse
{
    public bool Sucess { get; set; }
    public List<object> Data { get; set; } = new List<object>();
    public Errors Errors { get; set; } = new Errors();

}

public class Errors
{
    public List<object> Message { get; set; } = new List<object>();
    public void AddError(string message)
    {
        Message.Add(message);
    }
}