using PicEnfermagem.Application.DTOs;

namespace PicEnfermagem.Application.Interfaces;

public interface IPlayerService
{
    Task<bool> InsertPlayer(PlayerInsertRequest player);
}
