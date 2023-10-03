using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Domain.Entities;
using System.Security.Claims;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IAnswerRepository
{
    Task<bool> PostAnswer(Answer entity, ClaimsPrincipal claimUser);
    Task<IEnumerable<AnswerResponse>> GetAll();
}
