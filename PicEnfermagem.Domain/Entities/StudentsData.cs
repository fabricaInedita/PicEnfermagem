using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class StudentsData : EntityCore
{
    public string Name { get; set; }
    public string StudentCode { get; set; }
    public string Course { get; set; }

    private StudentsData() { }
    public StudentsData(string name, string studentCode, string course) 
    {
        Name = name;
        StudentCode = studentCode;
        Course = course;
    }
}