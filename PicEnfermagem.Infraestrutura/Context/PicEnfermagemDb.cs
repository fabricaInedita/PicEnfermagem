using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Mapping;

namespace PicEnfermagem.Infraestrutura.Context;

public class PicEnfermagemDb : IdentityDbContext<ApplicationUser>
{
    public readonly IHttpContextAccessor _contextAcessor;
    public PicEnfermagemDb(DbContextOptions options, IHttpContextAccessor contextAcessor) : base(options)
    {
        _contextAcessor = contextAcessor;
    }

    public DbSet<Question> question { get; set; }
    public DbSet<Alternative> alternative { get; set; }
    public DbSet<Answer> answer { get; set; }
    public DbSet<Category> category { get; set; }
    public DbSet<StudentsData> studentdata { get; set; }
    public DbSet<GameSetting> gamesetting { get; set; }
    public DbSet<ApplicationUser> user { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new QuestionMapping());
    }
}

