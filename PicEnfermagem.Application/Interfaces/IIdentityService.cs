
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;
using System.Security.Claims;

namespace PicEnfermagem.Application.Interfaces;

public interface IIdentityService
{
    Task<UserLoginResponse> LoginAsync(LoginRequest loginRequest);
    Task<UserRegisterResponse> RegisterUserAdmin(UserAdminRegisterRequest userRegister);
    Task<UserRegisterResponse> RegisterUser(UserInsertRequest userRegister);
    Task<DefaultResponse> DeleteUser(string email);
    Task<IEnumerable<UserResponse>> GetUser();
    Task<bool> PostAnswer(AnswerInsertRequest dto, ClaimsPrincipal claimUser);
}
