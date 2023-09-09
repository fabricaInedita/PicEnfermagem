using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class AlternativeFactory
{
    public static Alternative Create(string option, bool isCorrect)
    {
        return new Alternative(option, isCorrect);
    }
}
