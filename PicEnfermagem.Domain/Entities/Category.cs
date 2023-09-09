using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class Category : EntityCore
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    internal Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    private Category() { }
}
