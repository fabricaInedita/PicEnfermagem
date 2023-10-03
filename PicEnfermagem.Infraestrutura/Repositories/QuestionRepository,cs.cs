using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<Question> _question;
    private readonly IAnswerRepository _answerRepository;
    public QuestionRepository(PicEnfermagemDb context, IAnswerRepository answerRepository)
    {
        _context = context;
        _question = _context.Set<Question>();
        _answerRepository = answerRepository;
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
                             Alternatives = (ICollection<AlternativeResponse>)question.Alternatives.Select(alternative => new AlternativeResponse()
                             {
                                 IsCorrect = alternative.IsCorrect,
                                 Option = alternative.Option
                             }),
                             Category = new CategoryResponse()
                             {
                                 Description = question.Category.Description,
                                 Name = question.Category.Name
                             }
                         }).AsEnumerable();

        return questions;
    }


    public async Task<IEnumerable<QuestionResponse>> GetByCategoryId(int categoryId)
    {
        var questions = (from question in _question
                        .AsNoTracking()
                        .Where(x => x.CategoryId == categoryId)
                         select new QuestionResponse()
                         {
                             Alternatives = (ICollection<AlternativeResponse>)question.Alternatives.Select(alternative => new AlternativeResponse()
                             {
                                 IsCorrect = alternative.IsCorrect,
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

}
