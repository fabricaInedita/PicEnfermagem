
using Microsoft.AspNetCore.Identity;

namespace PicEnfermagem.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get;  set; }

    //private ApplicationUser() { }

    //internal ApplicationUser(string name,  string email) : base(email)
    //{
    //    Name = name;
    //}
}
