using PicEnfermagem.Application.DTOs.Answer;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Factories;
using System.Security.Claims;

namespace PicEnfermagem.Application.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IAlternativeRepository _alternativeRepository;

    public AnswerService(IAnswerRepository ashwerRepository, IAlternativeRepository alternativeRepository)
    {
        _answerRepository = ashwerRepository;
        _alternativeRepository = alternativeRepository;
    }

    public async Task<AnswerResponse> PostAnswer(AnswerInsertRequest dto, ClaimsPrincipal claimUser)
    {
        var answer = AnswerFactory.Create(dto.QuestionId, dto.Punctuation);

        var question = (await _alternativeRepository.GetByIdAsync(dto.QuestionId));

        var alternativeAnswer = question.Where(x => x.Id == dto.AlternativeId).SingleOrDefault();
        var result = await _answerRepository.PostAnswer(answer, claimUser, alternativeAnswer);

        var alternativeCorrect = question.Where(x => x.IsCorrect == true).FirstOrDefault();

        var response = new AnswerResponse()
        {
            AlternativeCorrectId = alternativeCorrect.Id,
            Punctuation = result
        };

        return response;

    }
}
