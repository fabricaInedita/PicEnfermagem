using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;

namespace PicEnfermagem.Application.Interfaces;

public interface IQuestionService
{
    Task<bool> InsertAsync(QuestionInsertRequest question);
    Task<IEnumerable<QuestionResponse>> GetAllAsync();
}
