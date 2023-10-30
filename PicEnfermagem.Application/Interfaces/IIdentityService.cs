
using PicEnfermagem.Application.DTOs.Answer;
using PicEnfermagem.Application.DTOs.User;
using PicEnfermagem.Domain.Entities;
using System.Security.Claims;

namespace PicEnfermagem.Application.Interfaces;

public interface IIdentityService
{
    Task<UserLoginResponse> LoginAsync(LoginRequest loginRequest);
    //Task<UserRegisterResponse> RegisterUser(UserInsertRequest userRegister);
    Task<UserRegisterResponse> PreparaUser(UserInsertRequest userRegister);
    Task<DefaultResponse> DeleteUser(string email);
    Task<IEnumerable<UserResponse>> GetUser();
    Task<double> GetPunctuationByUserLogged();
    Task<IEnumerable<UserResponse>> GetRank();
    Task<bool> PostAnswer(AnswerInsertRequest dto, ClaimsPrincipal claimUser);
    Task<DefaultResponse> ConfirmEmail(string token, string idUser);
    Task IssueCertificate();
    Task<DefaultResponse> ResetPasswordAsync(UserResetPassword model);
    Task GenarateRefreshPasswordToken(string email);
    Task<ApplicationUser> GetUserByUsername(string username);
    Task<UserRegisterResponse> InsertUserAsync(ApplicationUser user, UserInsertRequest userRegister);
}
