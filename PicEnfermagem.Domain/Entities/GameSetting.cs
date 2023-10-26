using PicEnfermagem.Domain.EntitieBase;

namespace PicEnfermagem.Domain.Entities;

public sealed class GameSetting : EntityCore
{
    public DateTime? FirstQuestions { get;set; } 
    public DateTime? EndQuestions { get;set; } 
    public ApplicationUser User { get;set; }
    public string UserId { get;set; }
}
