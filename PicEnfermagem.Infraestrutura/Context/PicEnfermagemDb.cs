using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Infraestrutura.Context;

public class PicEnfermagemDb : DbContext
{
    public PicEnfermagemDb(DbContextOptions options) : base(options) { }

    public DbSet<Player> player { get; set; }

}
