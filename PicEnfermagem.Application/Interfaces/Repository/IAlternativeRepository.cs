using PicEnfermagem.Application.DTOs.Alternative;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IAlternativeRepository
{
    Task<IEnumerable<AlternativeResponse>> GetByIdAsync(int id);
}
