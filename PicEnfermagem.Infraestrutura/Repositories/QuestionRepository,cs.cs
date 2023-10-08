using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.DTOs.Alternative;
using PicEnfermagem.Application.DTOs.Category;
using PicEnfermagem.Application.DTOs.Question;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<Question> _question;
    protected readonly UserManager<ApplicationUser> _user;
    public QuestionRepository(PicEnfermagemDb context, UserManager<ApplicationUser> user)
    {
        _context = context;
        _question = _context.Set<Question>();
        _user = user;
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
        var questions = (from question in _question
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
                         }).AsEnumerable();

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
}
