using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Factories;

namespace PicEnfermagem.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRep;
    public PlayerService(IPlayerRepository playerRep)
    {
        _playerRep = playerRep;
    }
    public async Task<bool> InsertPlayer(PlayerInsertRequest player)
    {
        var newPlayer = PlayerFactory
            .Create
            (player.Name,
            player.Age,
            player.Period,
            player.Phone,
            player.Course,
            player.Email);

        return await _playerRep.InsertPlayer(newPlayer);
    }

    public async Task<DefaultResponse> GetAsync()
    {
        var response = new DefaultResponse();

        var players = await _playerRep.GetAsync();

        response.Data.AddRange(players);

        return response;
    }
}
