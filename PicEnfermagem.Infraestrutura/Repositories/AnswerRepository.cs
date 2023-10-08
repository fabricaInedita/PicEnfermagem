using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    private readonly IQuestionRepository _questionRepository;
    private readonly UserManager<ApplicationUser> _user;
    private string _userId;

    public AnswerRepository(PicEnfermagemDb context, UserManager<ApplicationUser> user, IQuestionRepository questionRepository)
    {
        _context = context;
        _answer = _context.Set<Answer>();
        _user = user;
        _userId = _context._contextAcessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        _questionRepository = questionRepository;
    }

    public async Task<double> PostAnswer(Answer entity, ClaimsPrincipal claimUser)
    {
        var user = await _user.GetUserAsync(claimUser);
        entity.UserId = user.Id;
        var ponctuationActual = user.Punctuation += entity.Punctuation;

        _answer.Add(entity);

        var response = await _context.SaveChangesAsync();

        if (response < 1)
            return 0;

        return ponctuationActual;
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

}
