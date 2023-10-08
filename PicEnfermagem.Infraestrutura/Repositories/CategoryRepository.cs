using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.DTOs.Category;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<Category> _categories;
    public CategoryRepository(PicEnfermagemDb context)
    {
        _context = context;
        _categories = context.Set<Category>();
    }

    public async Task<bool> InsertAsync(Category category)
    {
        await _categories.AddAsync(category);

        var response = await _context.SaveChangesAsync();

        if (response < 1)
            return false;

        return true;
    }

    public async Task<Category> FindByIdAsync(int id)
    {
        var category = await _categories.FindAsync(id);

        return category;
    }

    public async Task<IEnumerable<CategoryResponse>> GetAsync()
    {
        var response = (from category in _categories
                       select new CategoryResponse()
                       {
                           Description = category.Description,
                           Name = category.Name
                       }).AsEnumerable();

        return response;
    }
}
