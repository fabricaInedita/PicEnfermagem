using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.DTOs.Alternative;
using PicEnfermagem.Application.DTOs.Answer;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;
using System.Security.Claims;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<Answer> _answer;
    private readonly DbSet<Question> _question;
    private readonly DbSet<GameSetting> _gameSetting;
    private readonly UserManager<ApplicationUser> _user;
    private string _userId;

    public AnswerRepository(PicEnfermagemDb context, UserManager<ApplicationUser> user)
    {
        _context = context;
        _answer = _context.Set<Answer>();
        _user = user;
        _userId = _context._contextAcessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        _question = _context.Set<Question>();
        _gameSetting = _context.Set<GameSetting>();
    }

    public async Task<double> PostAnswer(Answer entity, ClaimsPrincipal claimUser, AlternativeResponse alternativeResponse)
    {
        await VerifyEndQuestionsAsync();

        var user = await _user.GetUserAsync(claimUser);
        double punctuation = user.Punctuation;

        if (alternativeResponse.IsCorrect)
            punctuation = await CalculatePunctuationUser(user, entity);

        entity.UserId = user.Id;

        _answer.Add(entity);

        var response = await _context.SaveChangesAsync();

        if (response < 1)
            return 0;

        return punctuation;
    }

    public async Task<IEnumerable<AnswerResponse>> GetAll()
    {
        var answerResponse = (from Answer in _answer
                             .AsNoTracking()
                             .Where(x => x.UserId == _userId)
                              select new AnswerResponse()
                              {
                                  QuestionId = Answer.QuestionId,
                                  Punctuation = Answer.Punctuation
                              }).AsEnumerable();

        return answerResponse;
    }

    private async Task<double> CalculatePunctuationUser(ApplicationUser user, Answer answer)
    {
        var ponctuationActual = user.Punctuation += answer.Punctuation;

        return ponctuationActual;
    }

    #region AUX
    private async Task<bool> VerifyEndQuestionsAsync()
    {
        var questionsTotal = _question.ToList().Count;
        var answerTotal = (await GetAll()).ToList().Count;

        if(questionsTotal == answerTotal)
        {
            var gameSetting = await _gameSetting.FindAsync(_userId);
            gameSetting.EndQuestions = DateTime.Now;

            _gameSetting.Update(gameSetting);

            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }
    #endregion
}
