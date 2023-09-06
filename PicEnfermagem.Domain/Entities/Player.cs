using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class Player : EntityCore
{
    public string Name { get; private set; }
    public int Age { get; private set; }
    public int Period { get; private set; }
    public string Phone { get;  private set; }
    public string Course { get; private set; }
    public string Email { get; private set; }

    internal Player(string name, int age, int period, string phone, string course, string email)
    {
        Name = name;
        Age = age;
        Period = period;
        Phone = phone;
        Course = course;
        Email = email;
    }
    private Player() { }
}
