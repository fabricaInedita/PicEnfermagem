using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class AlternativeFactory
{
    public static Alternative Create(string option, string description, bool isCorrect)
    {
        return new Alternative(option, description, isCorrect);
    }
}
