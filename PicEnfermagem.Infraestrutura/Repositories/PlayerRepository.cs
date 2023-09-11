using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly PicEnfermagemDb _dbContext;
    private readonly DbSet<Player> _players;

    public PlayerRepository(PicEnfermagemDb dbContext)
    {
        _dbContext = dbContext;
        _players = _dbContext.Set<Player>();
    }
    public async Task<bool> InsertPlayer(Player player)
    {
        await _players.AddAsync(player);

        var result = await _dbContext.SaveChangesAsync();

        if (result < 1)
            return false;

        return true;
    }

    public async Task<IEnumerable<PlayerResponse>> GetAsync()
    {
        var response = (from player in _players
                        select new PlayerResponse()
                        {
                            Name = player.Name,
                            Age = player.Age,
                            Period = player.Period,
                            Phone = player.Phone,
                            Course = player.Course,
                            Email = player.Email
                        }).AsEnumerable();
        return response;
    }
}
