using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IPlayerRepository
{
    Task<bool> InsertPlayer(Player player);
    Task<IEnumerable<PlayerResponse>> GetAsync();
}
