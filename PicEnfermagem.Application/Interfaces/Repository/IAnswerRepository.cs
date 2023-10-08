using PicEnfermagem.Application.DTOs.Answer;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Domain.Entities;
using System.Security.Claims;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IAnswerRepository
{
    Task<double> PostAnswer(Answer entity, ClaimsPrincipal claimUser);
    Task<IEnumerable<AnswerResponse>> GetAll();
}
