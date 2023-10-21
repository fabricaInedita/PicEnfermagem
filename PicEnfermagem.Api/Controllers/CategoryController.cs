using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PicEnfermagem.Application.DTOs.Category;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Infraestrutura.Context;
using RestSharp;
using System.Reflection.Metadata;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace PicEnfermagem.Api.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<StudentsData> _studenteData;

    public CategoryController(PicEnfermagemDb context, ICategoryService categoryService)
    {
        _context = context;
        _studenteData = _context.Set<StudentsData>();
        _categoryService = categoryService;

    }

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var response = await _categoryService.GetAsync();

        if (!response.Sucess)
            return Ok(response.Data);

        return BadRequest(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> InsertAsync(CategoryInsertRequest category)
    {
        var result = await _categoryService.InsertAsync(category);

        if (result)
            return Ok();

        return BadRequest();
    }
}

