﻿
using Microsoft.AspNetCore.Identity;

namespace PicEnfermagem.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get;  set; }

}
