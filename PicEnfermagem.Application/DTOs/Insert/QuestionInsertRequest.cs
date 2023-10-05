namespace PicEnfermagem.Application.DTOs.Insert;

public class QuestionInsertRequest
{
    public ICollection<AlternativeInsertRequest> Alternatives { get; set; }
    public string statement { get; set; }
    public int CategoryId { get; set; }
    public int MaxPunctuation { get; set; }
    public int MinPunctuation { get; set; }
    public string Difficulty { get; set; }
}
