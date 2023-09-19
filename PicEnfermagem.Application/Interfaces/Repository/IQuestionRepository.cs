using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IQuestionRepository
{
    Task<bool> InsertAsync(Question question);
    Task<IEnumerable<QuestionResponse>> GetAllAsync();
    Task<IEnumerable<QuestionResponse>> GetByCategoryId(int categoryId);
}
