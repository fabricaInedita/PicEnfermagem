﻿using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;

namespace PicEnfermagem.Infraestrutura.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<Question> _question;
    public QuestionRepository(PicEnfermagemDb context)
    {
        _context = context;
        _question = _context.Set<Question>();
    }
    public async Task<bool> InsertAsync(Question question)
    {
        await _question.AddAsync(question);

        var response = await _context.SaveChangesAsync();

        if(response < 1)
            return false;

        return true;
    }
}