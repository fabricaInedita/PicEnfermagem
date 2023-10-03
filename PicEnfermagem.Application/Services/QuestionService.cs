﻿using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Domain.Factories;

namespace PicEnfermagem.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRep;
    private readonly ICategoryRepository _categoryRep;
    public QuestionService(IQuestionRepository questionRep, ICategoryRepository categoryRep)
    {
        _questionRep = questionRep;
        _categoryRep = categoryRep;
    }

    public async Task<bool> InsertAsync(QuestionInsertRequest questionDto)
    {
        var alternatives = await PrepareAlternativesAsync(questionDto.Alternatives);

        var category = await _categoryRep.FindByIdAsync(questionDto.CategoryId);

        var question = QuestionFactory.Create(questionDto.statement, alternatives, category);

        return await _questionRep.InsertAsync(question);
    }

    public async Task<IEnumerable<QuestionResponse>> GetAllAsync()
    {
        return await _questionRep.GetAllAsync();
    }
    public async Task<IEnumerable<QuestionResponse>> GetByCategoryAsync(int categoryId)
    {
        return await _questionRep.GetByCategoryId(categoryId);
    }
    private async Task<List<Alternative>> PrepareAlternativesAsync(ICollection<AlternativeInsertRequest> alternativesDto)
    {
        var alternatives = new List<Alternative>();

        foreach(var alternativeActual in alternativesDto)
        {
            alternatives.Add(AlternativeFactory.Create(alternativeActual.Option, alternativeActual.Description, alternativeActual.IsCorrect));
        }

        return alternatives;
    }
}
