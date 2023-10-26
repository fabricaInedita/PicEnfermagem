using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IGameSettingRepository
{
    Task<List<GameSetting>> GetAllAsync();
}
