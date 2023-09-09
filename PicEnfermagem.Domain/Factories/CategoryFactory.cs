using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class CategoryFactory
{
    public static Category Create(string name, string description)
    {
        return new Category(name, description);
    }
}
