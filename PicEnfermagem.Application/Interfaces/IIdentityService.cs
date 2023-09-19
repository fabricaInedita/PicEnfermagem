
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;

namespace PicEnfermagem.Application.Interfaces;

public interface IIdentityService
{
    Task<UserLoginResponse> LoginAsync(LoginRequest loginRequest);
    Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister);
    Task<DefaultResponse> DeleteUser(string email);
    Task<IEnumerable<UserResponse>> GetUser();
}
