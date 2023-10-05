
using Microsoft.AspNetCore.Identity;

namespace PicEnfermagem.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get;  set; }
    public ICollection<Answer> Answers { get; set; }
    public int Punctuation { get; set; }

}
