using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Domain.Factories;

namespace PicEnfermagem.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRep;
    private readonly ICategoryRepository _categoryRep;
    private readonly IAnswerRepository _answerRep;
    public QuestionService(IQuestionRepository questionRep, ICategoryRepository categoryRep, IAnswerRepository answerRep)
    {
        _questionRep = questionRep;
        _categoryRep = categoryRep;
        _answerRep = answerRep;
    }

    public async Task<bool> InsertAsync(QuestionInsertRequest questionDto)
    {
        var alternatives = await PrepareAlternativesAsync(questionDto.Alternatives);

        var category = await _categoryRep.FindByIdAsync(questionDto.CategoryId);

        var question = QuestionFactory.Create(questionDto.statement, alternatives, category);

        return await _questionRep.InsertAsync(question);
    }

    public async Task<IEnumerable<QuestionResponse>> GetAllAsync()
    {
        var questions = (await _questionRep.GetAllAsync()).ToList();
        var answers = await _answerRep.GetAll();

        foreach(var item in answers) 
        {
            var questionResponse = questions.Where(x => x.Id == item.QuestionId).FirstOrDefault();

            questions.Remove(questionResponse);
        }

        return questions;
    }
    public async Task<IEnumerable<QuestionResponse>> GetByCategoryAsync(int categoryId)
    {
        return await _questionRep.GetByCategoryId(categoryId);
    }
    private async Task<List<Alternative>> PrepareAlternativesAsync(ICollection<AlternativeInsertRequest> alternativesDto)
    {
        var alternatives = new List<Alternative>();

        foreach(var alternativeActual in alternativesDto)
        {
            alternatives.Add(AlternativeFactory.Create(alternativeActual.Option, alternativeActual.Description, alternativeActual.IsCorrect));
        }

        return alternatives;
    }
}
