using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Factories;
using System.Security.Claims;

namespace PicEnfermagem.Application.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _ashwerRepository;

    public AnswerService(IAnswerRepository ashwerRepository)
    {
        _ashwerRepository = ashwerRepository;
    }

    public async Task<bool> PostAnswer(AnswerInsertRequest dto, ClaimsPrincipal claimUser)
    {
        var answer = AnswerFactory.Create(dto.QuestionId, dto.IsCorrectAnswer, dto.SecondsAnswer);
        return await _ashwerRepository.PostAnswer(answer, claimUser);
    }
}
