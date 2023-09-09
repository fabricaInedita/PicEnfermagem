using PicEnfermagem.Application.DTOs.Insert;

namespace PicEnfermagem.Application.Interfaces;

public interface IQuestionService
{
    Task<bool> InsertAsync(QuestionInsertRequest question);
}
