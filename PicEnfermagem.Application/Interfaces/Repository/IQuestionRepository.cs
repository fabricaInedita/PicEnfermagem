using PicEnfermagem.Application.DTOs.Question;
using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface IQuestionRepository
{
    Task<bool> InsertAsync(Question question);
    Task<IEnumerable<QuestionResponse>> GetAllAsync();
    Task<QuestionResponse> GetByIdAsync(int questionId);
    Task<IEnumerable<QuestionResponse>> GetByCategoryIdAsync(int categoryId);
}
