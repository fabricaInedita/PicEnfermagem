using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Question;

namespace PicEnfermagem.Application.Interfaces;

public interface IQuestionService
{
    Task<bool> InsertAsync(QuestionInsertRequest question);
    Task<QuestionResponseList> GetAllAsync();
    Task<IEnumerable<QuestionResponse>> GetByCategoryAsync(int categoryId);
}
