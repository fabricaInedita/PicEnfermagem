﻿using PicEnfermagem.Application.DTOs.Category;
using PicEnfermagem.Domain.Entities;

namespace PicEnfermagem.Application.Interfaces.Repository;

public interface ICategoryRepository
{
    Task<bool> InsertAsync(Category player);
    Task<IEnumerable<CategoryResponse>> GetAsync();
    Task<Category> FindByIdAsync(int id);

}
