using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IPlayerRepository
{
    Task<bool> InsertPlayer(Player player);
}
