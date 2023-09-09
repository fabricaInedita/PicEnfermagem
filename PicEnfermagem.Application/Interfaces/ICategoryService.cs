using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;

namespace PicEnfermagem.Application.Interfaces;

public interface ICategoryService
{
    Task<bool> InsertAsync(CategoryInsertRequest category);
    Task<DefaultResponse> GetAsync();
}
