using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class Question : EntityCore
{
    public string Statement { get; private set; }
    public int MaxPunctuation { get; private set; }
    public int MinPunctuation { get; private set; }
    public string Difficulty { get; private set; }
    public Category Category { get; private set; }
    public int CategoryId { get; private set; }
    public ICollection<Alternative> Alternatives { get; private set; }
    internal Question(string statement, ICollection<Alternative> alternatives, Category category, int maxPunctuation, int minPunctuation, string difficulty)
    {
        Statement = statement;
        Alternatives = alternatives;
        Category = category;
        MaxPunctuation = maxPunctuation;
        MinPunctuation = minPunctuation;
        Difficulty = difficulty;
    }
    private Question() { }
}
