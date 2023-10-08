
using PicEnfermagem.Application.DTOs.Answer;
using PicEnfermagem.Application.DTOs.User;
using System.Security.Claims;

namespace PicEnfermagem.Application.Interfaces;

public interface IIdentityService
{
    Task<UserLoginResponse> LoginAsync(LoginRequest loginRequest);
    Task<UserRegisterResponse> RegisterUserAdmin(UserAdminRegisterRequest userRegister);
    Task<UserRegisterResponse> RegisterUser(UserInsertRequest userRegister);
    Task<DefaultResponse> DeleteUser(string email);
    Task<IEnumerable<UserResponse>> GetUser();
    Task<double> GetPunctuationByUserLogged();
    Task<IEnumerable<UserResponse>> GetRank();
    Task<bool> PostAnswer(AnswerInsertRequest dto, ClaimsPrincipal claimUser);
}
