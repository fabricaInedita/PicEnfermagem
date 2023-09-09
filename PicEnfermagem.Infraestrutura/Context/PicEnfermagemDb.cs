using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Mapping;

namespace PicEnfermagem.Infraestrutura.Context;

public class PicEnfermagemDb : DbContext
{
    public PicEnfermagemDb(DbContextOptions options) : base(options) { }

    public DbSet<Player> player { get; set; }
    public DbSet<Question> question { get; set; }
    public DbSet<Alternative> alternative { get; set; }
    public DbSet<Category> category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new QuestionMapping());
    }
}
