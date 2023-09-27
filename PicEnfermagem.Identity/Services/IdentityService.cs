namespace PicEnfermagem.Identity.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PicEnfermagem.Application.DTOs.Insert;
using PicEnfermagem.Application.DTOs.Response;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Identity.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


public class IdentityService : IIdentityService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly JwtOptions _jwtOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
        _serviceProvider = serviceProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserLoginResponse> LoginAsync(LoginRequest userLogin)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, isPersistent: false, lockoutOnFailure: true);
        if (signInResult.Succeeded)
        {
            var credenciais = await GerarCredenciais(userLogin.Email);
            return credenciais;
        }

        UserLoginResponse userLoginResponse = new UserLoginResponse(signInResult.Succeeded);
        if (!signInResult.Succeeded)
        {
            if (signInResult.IsLockedOut)
            {
                userLoginResponse.Errors.AddError("Esta conta está bloqueada.");
            }
            else if (signInResult.IsNotAllowed)
            {
                userLoginResponse.Errors.AddError("Esta conta não tem permissão para entrar.");
            }
            else if (signInResult.RequiresTwoFactor)
            {
                userLoginResponse.Errors.AddError("Confirme seu email.");
            }
            else
            {
                userLoginResponse.Errors.AddError("Nome de usuário ou senha estão incorretos.");
            }
        }

        return userLoginResponse;
    }

    public async Task<UserRegisterResponse> RegisterUserAdmin(UserAdminRegisterRequest userRegister)
    {
        var user = new ApplicationUser()
        {
            Email = userRegister.Email,
            Name = userRegister.Name,
            UserName = userRegister.Email,
        };

        IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

        UserRegisterResponse userRegisterResponse = new UserRegisterResponse(result.Succeeded);

        if (!result.Succeeded)
        {
            foreach (var erroAtual in result.Errors)
            {
                switch (erroAtual.Code)
                {
                    case "PasswordRequiresNonAlphanumeric":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um caracter especial - ex( * | ! ).");
                        break;

                    case "PasswordRequiresDigit":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um número (0 - 9).");
                        break;

                    case "PasswordRequiresUpper":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um caracter em maiúsculo.");
                        break;

                    case "DuplicateUserName":
                        userRegisterResponse.Errors.AddError("O email informado já foi cadastrado!");
                        break;

                    default:
                        userRegisterResponse.Errors.AddError("Erro ao criar usuário.");
                        break;
                }

            }
        }
        else
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            await _userManager.AddToRoleAsync(user, "Admin");
            await _userManager.AddClaimAsync(user, new Claim("permissions", "CriarPergunta"));
            await _userManager.AddClaimAsync(user, new Claim("permissions", "DeletarPergunta"));
            await _userManager.AddClaimAsync(user, new Claim("permissions", "EditarPergunta"));
            await _userManager.AddClaimAsync(user, new Claim("permissions", "VisualizarPergunta"));
        }


        return userRegisterResponse;
    }
    public async Task<UserRegisterResponse> RegisterUser(UserInsertRequest userRegister)
    {
        var user = new ApplicationUser()
        {
            UserName = userRegister.Code,
            Name = userRegister.Code
        };

        IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

        UserRegisterResponse userRegisterResponse = new UserRegisterResponse(result.Succeeded);

        if (!result.Succeeded)
        {
            foreach (var erroAtual in result.Errors)
            {
                switch (erroAtual.Code)
                {
                    case "PasswordRequiresNonAlphanumeric":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um caracter especial - ex( * | ! ).");
                        break;

                    case "PasswordRequiresDigit":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um número (0 - 9).");
                        break;

                    case "PasswordRequiresUpper":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um caracter em maiúsculo.");
                        break;

                    case "DuplicateUserName":
                        userRegisterResponse.Errors.AddError("O email informado já foi cadastrado!");
                        break;

                    default:
                        userRegisterResponse.Errors.AddError("Erro ao criar usuário.");
                        break;
                }

            }
        }

        return userRegisterResponse;
    }

    public async Task<IEnumerable<UserResponse>> GetUser()
    {
        var result = (from users in _userManager.Users
                      select new UserResponse()
                      {
                          Email = users.Email,
                          Name = users.Name
                      }).AsEnumerable();

        return result;
                           
    }
    public async Task<DefaultResponse> DeleteUser(string email)
    {
        var response = new DefaultResponse();
        var user = await _userManager.FindByEmailAsync(email);

        var result = await _userManager.DeleteAsync(user);
        response.Sucess = result.Succeeded;

        return response;
    }
    private async Task<UserLoginResponse> GerarCredenciais(string email)
    {
        var user = await _userManager.FindByNameAsync(email);
        var accessTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: true);
        var refreshTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: false);

        var dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
        var dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

        var accessToken = GerarToken(accessTokenClaims, dataExpiracaoAccessToken);
        var refreshToken = GerarToken(refreshTokenClaims, dataExpiracaoRefreshToken);
        var expirationAcessToken = _jwtOptions.AccessTokenExpiration.ToString();
        var expirationTimeRefreshToken = _jwtOptions.RefreshTokenExpiration.ToString();

        return new UserLoginResponse
        (
            true,
            accessToken,
            refreshToken,
            expirationTimeRefreshToken,
            expirationAcessToken
        );
    }
    private string GerarToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
    {
        JwtSecurityToken token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, DateTime.Now, dataExpiracao, _jwtOptions.SigningCredentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private async Task<IList<Claim>> ObterClaims(ApplicationUser user, bool adicionarClaimsUsuario)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.UserName));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

        if (adicionarClaimsUsuario)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(userClaims);

            foreach (var role in roles)
                claims.Add(new Claim("role", role));
        }

        return claims;
    }

}
