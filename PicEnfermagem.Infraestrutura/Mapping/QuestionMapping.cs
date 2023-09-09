using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Infraestrutura.Mapping;

public class QuestionMapping : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasMany(x => x.Alternatives)
            .WithOne(x => x.Question)
            .HasForeignKey(x => x.QuestionId);

        builder
            .HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId);
    }
}
