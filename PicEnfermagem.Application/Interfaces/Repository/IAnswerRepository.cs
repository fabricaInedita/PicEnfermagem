using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Domain.Entities;
using System.Security.Claims;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IAnswerRepository
{
    Task<bool> PostAnswer(Answer entity, ClaimsPrincipal claimUser);
}
