namespace PicEnfermagem.Application.DTOs.User;

public sealed class PlayerInsertRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Period { get; set; }
    public string Phone { get; set; }
    public string Course { get; set; }
}