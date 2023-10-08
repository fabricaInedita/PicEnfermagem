using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.DTOs.Alternative;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class AlternativeRepository : IAlternativeRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<Alternative> _alternatives;

    public AlternativeRepository(PicEnfermagemDb context)
    {
        _context = context;
        _alternatives = _context.Set<Alternative>();
    }

    public async Task<IEnumerable<AlternativeResponse>> GetByIdAsync(int id)
    {
        var alternative = (from alternatives in _alternatives
                          .AsNoTracking()
                          .Where(alternative => alternative.QuestionId == id)
                           select new AlternativeResponse()
                           {
                               Description = alternatives.Description,
                               Id = alternatives.Id,
                               IsCorrect = alternatives.IsCorrect,
                               Option = alternatives.Option
                           }).AsEnumerable();

        return alternative;
    }
}
