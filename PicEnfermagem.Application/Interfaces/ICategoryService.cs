using PicEnfermagem.Application.DTOs.Category;
using PicEnfermagem.Application.DTOs.User;

namespace PicEnfermagem.Application.Interfaces;

public interface ICategoryService
{
    Task<bool> InsertAsync(CategoryInsertRequest category);
    Task<DefaultResponse> GetAsync();
}
