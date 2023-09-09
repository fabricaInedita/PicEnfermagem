namespace PicEnfermagem.Application.DTOs.Insert;

public class QuestionInsertRequest
{
    public ICollection<AlternativeInsertRequest> Alternatives { get; set; }
    public string statement { get; set; }
    public int CategoryId { get; set; }
}
