using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IQuestionRepository
{
    Task<bool> InsertAsync(Question question);
}
