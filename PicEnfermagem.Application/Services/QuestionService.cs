using PicEnfermagem.Application.DTOs.Alternative;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Question;
using PicEnfermagem.Application.DTOs.User;
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
        var questions = new List<QuestionResponse>();

        var questionaryIsValid = await _userService.GetUserByUsername("QuestionaryIsValid");

        if (questionaryIsValid == null) 
        { 
            questions = (await _questionRep.GetAllAsync()).ToList();
            var answers = await _answerRep.GetAll();

            foreach (var item in answers)
            {
                var questionResponse = questions.Where(x => x.Id == item.QuestionId).FirstOrDefault();

                questions.Remove(questionResponse);
            }
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
    public async Task<bool> ChangeQuestionaryState()
    {
        var requestUserData = new UserInsertRequest()
        {
            ConfirmPassword = "OkAny12345*.",
            Password = "OkAny12345*.",
            Email = "user.example.com.br",
            Username = "QuestionaryIsValid"
        };

        var user = new ApplicationUser()
        {
            UserName = requestUserData.Username,
            Name = requestUserData.Username,
            Course = "ENFERMAGEM",
            StudentCode = requestUserData.Username,
            Email = requestUserData.Email,
            RegistrationDate = DateTime.Now.ToUniversalTime(),
        };

        var state = _userService.GetUserByUsername("QuestionaryIsValid");

        if (state == null)
        {
            await _userService.InsertUserAsync(user, requestUserData);

            return false;
        }
        else
        {
            _userService.DeleteUserAsync(user);

            return true;
        }
    }
    public async Task<bool> GetQuestionaryState()
    {
        var state = _userService.GetUserByUsername("QuestionaryIsValid");

        if (state == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
