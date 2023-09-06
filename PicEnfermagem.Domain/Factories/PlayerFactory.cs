using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Domain.Factories;

public static class PlayerFactory
{
    public static Player Create(string name, int age, int period, string phone, string course, string email)
    {
        var newPlayer = new Player(name, age, period, phone, course, email);

        return newPlayer;
    }
}
