﻿using System.ComponentModel.DataAnnotations;

namespace PicEnfermagem.Application.DTOs.Alternative;

public class AlternativeInsertRequest
{
    public string Option { get; set; }
    public string Description { get; set; }
    public bool IsCorrect { get; set; }
}
