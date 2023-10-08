using PicEnfermagem.Application.DTOs.Answer;
using System.Security.Claims;

namespace PicEnfermagem.Application.Interfaces;

public interface IAnswerService
{
    Task<AnswerResponse> PostAnswer(AnswerInsertRequest dto, ClaimsPrincipal claimUser);
    
}
