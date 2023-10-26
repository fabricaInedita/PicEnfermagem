using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;
using System.Security.Claims;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class GameSettingRepository : IGameSettingRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<GameSetting> _gameSettings;
    private readonly string _userId;

    public GameSettingRepository(PicEnfermagemDb context)
    {
        _context = context;
        _gameSettings = _context.Set<GameSetting>();
        _userId = _context._contextAcessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

    }

    public async Task<List<GameSetting>> GetAllAsync()
    {
        var gameSettings = (from gameSetting in _gameSettings
                           .Where(x => x.UserId == _userId)
                            select new GameSetting()
                            {
                                FirstQuestions = gameSetting.FirstQuestions,
                                EndQuestions = gameSetting.EndQuestions,
                                UserId = gameSetting.UserId
                            }).ToList();

        return gameSettings;
    }
}
