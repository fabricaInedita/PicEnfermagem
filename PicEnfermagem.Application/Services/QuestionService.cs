using PicEnfermagem.Application.DTOs.Alternative;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Question;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Domain.Factories;

namespace PicEnfermagem.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRep;
    private readonly ICategoryRepository _categoryRep;
    private readonly IGameSettingRepository _gameSettingRep;
    private readonly IAnswerRepository _answerRep;
    private readonly IIdentityService _userService;
    public QuestionService(IQuestionRepository questionRep, ICategoryRepository categoryRep, IAnswerRepository answerRep, IIdentityService userService, IGameSettingRepository gameSettingRep)
    {
        _questionRep = questionRep;
        _categoryRep = categoryRep;
        _answerRep = answerRep;
        _userService = userService;
        _gameSettingRep = gameSettingRep;
    }

    public async Task<bool> InsertAsync(QuestionInsertRequest questionDto)
    {
        var alternatives = await PrepareAlternativesAsync(questionDto.Alternatives);

        var category = await _categoryRep.FindByIdAsync(questionDto.CategoryId);

        var question = QuestionFactory
            .Create(questionDto.statement,
            alternatives, category,
            questionDto.MaxPunctuation,
            questionDto.MinPunctuation,
            questionDto.Difficulty);

        return await _questionRep.InsertAsync(question);
    }

    public async Task<QuestionResponseList> GetAllAsync()
    {
        var questions = (await _questionRep.GetAllAsync()).ToList();
        var answers = await _answerRep.GetAll();

        foreach(var item in answers) 
        {
            var questionResponse = questions.Where(x => x.Id == item.QuestionId).FirstOrDefault();

            questions.Remove(questionResponse);
        }

        var initialDate = (await _gameSettingRep.GetAllAsync()).SingleOrDefault().FirstQuestions;
        var questionResponseList = new QuestionResponseList()
        {
            Punctuation = await _userService.GetPunctuationByUserLogged(),
            QuestionResponses = questions,
            InitialFormDate = initialDate
            
        };

        return questionResponseList;
    }
    public async Task<IEnumerable<QuestionResponse>> GetByCategoryAsync(int categoryId)
    {
        return await _questionRep.GetByCategoryIdAsync(categoryId);
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
