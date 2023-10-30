using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.DTOs.Alternative;
using PicEnfermagem.Application.DTOs.Category;
using PicEnfermagem.Application.DTOs.Question;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;
using System.Security.Claims;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<Question> _question;
    private readonly DbSet<GameSetting> _gameSettings;
    private readonly IGameSettingRepository _gameSettingsRepository;
    protected readonly UserManager<ApplicationUser> _user;
    public QuestionRepository(PicEnfermagemDb context, UserManager<ApplicationUser> user, IGameSettingRepository gameSettingsRepository)
    {
        _context = context;
        _question = _context.Set<Question>();
        _user = user;
        _gameSettings = _context.Set<GameSetting>();
        _gameSettingsRepository = gameSettingsRepository;
    }
    public async Task<bool> InsertAsync(Question question)
    {
        await _question.AddAsync(question);

        var response = await _context.SaveChangesAsync();

        if (response < 1)
            return false;

        return true;
    }

    public async Task<IEnumerable<QuestionResponse>> GetAllAsync()
    {
        var questions = new List<QuestionResponse>();
        var dateTimeActual = DateTime.Now.ToUniversalTime();
        await VerifyFirstQuestionGameSetting();

        var game = (await _gameSettingsRepository.GetAllAsync()).SingleOrDefault();
        var dateFirstQuestion = game.FirstQuestions.Value.AddMinutes(40);

        if (dateFirstQuestion > dateTimeActual)
        {
             questions = (from question in _question
                       .AsNoTracking()
                       .Include(x => x.Category)
                       .Include(x => x.Alternatives)
                             select new QuestionResponse()
                             {
                                 Id = question.Id,
                                 Statement = question.Statement,
                                 Difficulty = question.Difficulty,
                                 MaxPunctuation = question.MaxPunctuation,
                                 MinPunctuation = question.MinPunctuation,
                                 Alternatives = (ICollection<AlternativeResponse>)question.Alternatives.Select(alternative => new AlternativeResponse()
                                 {
                                     Id = alternative.Id,
                                     Option = alternative.Option,
                                     Description = alternative.Description,
                                 }),
                                 Category = new CategoryResponse()
                                 {
                                     Description = question.Category.Description,
                                     Name = question.Category.Name
                                 }
                             }).ToList();
        }
      

        return questions;
    }

    public async Task<IEnumerable<QuestionResponse>> GetByCategoryIdAsync(int categoryId)
    {
        var questions = (from question in _question
                        .AsNoTracking()
                        .Where(x => x.CategoryId == categoryId)
                         select new QuestionResponse()
                         {
                             Alternatives = (ICollection<AlternativeResponse>)question.Alternatives.Select(alternative => new AlternativeResponse()
                             {
                                 Option = alternative.Option
                             }),
                             Category = new CategoryResponse()
                             {
                                 Description = question.Category.Description,
                                 Name = question.Category.Name
                             },
                             Statement = question.Statement
                         }).AsEnumerable();

        return questions;
    }

    public async Task<QuestionResponse> GetByIdAsync(int questionId)
    {
        var questions = (from question in _question
               .AsNoTracking()
               .Include(x => x.Alternatives)
               .Where(x => x.Id == questionId)
                         select new QuestionResponse()
                         {
                             Alternatives = (ICollection<AlternativeResponse>)question.Alternatives.Select(x => new AlternativeResponse
                             {
                                 Id = x.Id,
                                 Description = x.Description,
                                 Option = x.Option,
                                 IsCorrect = x.IsCorrect,

                             }),
                             Difficulty = question.Difficulty,
                             MaxPunctuation = question.MaxPunctuation,
                             MinPunctuation = question.MinPunctuation,
                             Id = question.Id
                         }).ToList().FirstOrDefault();

        return questions;
    }

    #region AUX
    //Criar método para consulta do GameSetting, não retornar nada no método abaixo, ele vai inserir de qualquer forma
    private async Task VerifyFirstQuestionGameSetting()
    {
        var userId = _context._contextAcessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
     
        var game = from gameSetting in _gameSettings
                   .AsNoTracking()
                   .Where(x => x.UserId == userId)
                   select new GameSetting()
                   {
                       FirstQuestions = gameSetting.FirstQuestions,
                       EndQuestions = gameSetting.EndQuestions,
                       UserId = gameSetting.UserId
                   };

        if (!game.Any())
        {
            var newGame = new GameSetting()
            {
                FirstQuestions = DateTime.Now.ToUniversalTime(),
                UserId = userId,
                RegistrationDate = DateTime.Now.ToUniversalTime()
            };

            await _gameSettings.AddAsync(newGame);
            await _context.SaveChangesAsync();
        }
    }
    #endregion
}
