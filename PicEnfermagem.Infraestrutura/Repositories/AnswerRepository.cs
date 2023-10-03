using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Domain.Factories;
using PicEnfermagem.Infraestrutura.Context;
using System.Security.Claims;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<Answer> _answer;
    private readonly UserManager<ApplicationUser> _user;
    private string _userId;

    public AnswerRepository(PicEnfermagemDb context, UserManager<ApplicationUser> user)
    {
        _context = context;
        _answer = _context.Set<Answer>();
        _user = user;
        _userId = _context._contextAcessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

    }

    public async Task<bool> PostAnswer(Answer entity, ClaimsPrincipal claimUser)
    {
        var user = await _user.GetUserAsync(claimUser);
        entity.UserId = user.Id;
        _answer.Add(entity);
        var response = await _context.SaveChangesAsync();

        if (response < 1)
            return false;

        return true;
    }

    public async Task<IEnumerable<AnswerResponse>> GetAll()
    {
        var answerResponse = (from Answer in _answer
                             .AsNoTracking()
                             .Where(x => x.UserId == _userId)
                              select new AnswerResponse()
                              {
                                  IsCorrectAnswer = Answer.IsCorrectAnswer,
                                  QuestionId = Answer.QuestionId,
                                  SecondsAnswer = Answer.SecondsAnswer,
                              }).AsEnumerable();

        return answerResponse;
    }

}
