using PicEnfermagem.Application.DTOs.Category;
using PicEnfermagem.Application.DTOs.User;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Factories;

namespace PicEnfermagem.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<DefaultResponse> GetAsync()
    {
        var response = new DefaultResponse();

        var categories = await _categoryRepository.GetAsync();

        response.Data.AddRange(categories);

        return response;

    }

    public Task<bool> InsertAsync(CategoryInsertRequest categoryDto)
    {
        var category = CategoryFactory.Create(categoryDto.Name, categoryDto.Description);

        return _categoryRepository.InsertAsync(category);
    }
}
