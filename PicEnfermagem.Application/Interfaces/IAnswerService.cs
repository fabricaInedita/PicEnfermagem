using PicEnfermagem.Application.DTOs.Insert;
using System.Security.Claims;

namespace PicEnfermagem.Application.Interfaces;

public interface IAnswerService
{
    Task<bool> PostAnswer(AnswerInsertRequest dto, ClaimsPrincipal claimUser);
}
