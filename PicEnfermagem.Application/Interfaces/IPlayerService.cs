using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;

namespace PicEnfermagem.Application.Interfaces;

public interface IPlayerService
{
    Task<bool> InsertPlayer(PlayerInsertRequest player);
    Task<DefaultResponse> GetAsync();
}
