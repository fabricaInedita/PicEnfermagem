namespace PicEnfermagem.Identity.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PicEnfermagem.Application.DTOs.Answer;
using PicEnfermagem.Application.DTOs.User;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Domain.Factories;
using PicEnfermagem.Identity.Configuration;
using PicEnfermagem.Identity.Utils;
using PicEnfermagem.Infraestrutura.Context;
using PicEnfermagem.Infraestrutura.Migrations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _jwtOptions;
    private readonly PicEnfermagemDb _context;
    private readonly DbSet<StudentsData> _studentsDatas;
    private readonly IEmailSenderService _emailSenderService;
    private string _userId;

    public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions, PicEnfermagemDb context, IEmailSenderService emailSenderService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
        _context = context;
        _userId = _context._contextAcessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        _studentsDatas = _context.Set<StudentsData>();
        _emailSenderService = emailSenderService;
    }

    public async Task<UserLoginResponse> LoginAsync(LoginRequest userLogin)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(userLogin.Username, userLogin.Password, isPersistent: false, lockoutOnFailure: true);
        if (signInResult.Succeeded)
        {
            var credenciais = await GerarCredenciais(userLogin.Username);
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
    public async Task<UserRegisterResponse> RegisterUser(UserInsertRequest userRegister)
    {
        UserRegisterResponse userRegisterResponse = new UserRegisterResponse();

        var response = await ValidateStudentCode(userRegister.Username);

        if (response is null)
        {
            userRegisterResponse.Errors.AddError("O código do aluno informado não existe ou esta incorreto");
            return userRegisterResponse;
        }

        var user = new ApplicationUser()
        {
            UserName = userRegister.Username,
            Name = response,
            Course = "ENFERMAGEM",
            StudentCode = userRegister.Username,
            Email = userRegister.Email

        };

        IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

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
                        userRegisterResponse.Errors.AddError("O código do aluno informado já foi cadastrado!");
                        break;

                    default:
                        userRegisterResponse.Errors.AddError("Erro ao criar usuário.");
                        break;
                }

            }
        }
        else
        {
            _userManager.RegisterTokenProvider("default", new EmailConfirmationTokenProvider<ApplicationUser>());

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = $"https://pic-enfermagem-frontend.vercel.app/confirm-email?token={token}";

            _emailSenderService.SendEmail(user.Name, user.Email, link);

            userRegisterResponse.SucessMessage = "Email de confirmação enviado.";
        }

        return userRegisterResponse;
    }
    public async Task<IEnumerable<UserResponse>> GetUser()
    {
        var result = (from users in _userManager.Users
                      select new UserResponse()
                      {
                          Email = users.Email,
                          Name = users.Name,
                          Punctuation = users.Punctuation
                      }).AsEnumerable();

        return result;

    }
    public async Task<double> GetPunctuationByUserLogged()
    {
        double punctuation = 0;

        if (_userId is not null)
            punctuation = (await _userManager.FindByIdAsync(_userId)).Punctuation;

        return punctuation;
    }
    public async Task<IEnumerable<UserResponse>> GetRank()
    {
        var result = (from users in _userManager.Users
                      select new UserResponse()
                      {
                          Email = users.Email,
                          Name = users.Name,
                          Punctuation = users.Punctuation,

                      }).OrderByDescending(x => x.Punctuation).Take(10);

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
    public async Task<bool> PostAnswer(AnswerInsertRequest dto, ClaimsPrincipal claimUser)
    {
        var user = await _userManager.GetUserAsync(claimUser);
        var answer = AnswerFactory.Create(dto.QuestionId, dto.Punctuation);

        user.Answers.Add(answer);

        var response = await _context.SaveChangesAsync();

        if (response < 1)
            return false;

        return true;
    }
    public async Task<DefaultResponse> ConfirmEmail(string token, string idUser)
    {
        var response = new DefaultResponse();
        _userManager.RegisterTokenProvider("default", new EmailConfirmationTokenProvider<ApplicationUser>());

        var user = await _userManager.FindByIdAsync(idUser);
        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            response.Errors.AddError("Erro ao confirmar email.");
            response.Sucess = false;
            return response;
        }
        response.Sucess = true;

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
    public async Task IssueCertificate()
    {
        var user = await _userManager.FindByIdAsync(_userId);

        _emailSenderService.SendCertificateEmail(user.Name, user.Email);
    }

    public async Task GenarateRefreshPasswordToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(_userId);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var link = $"https://pic-enfermagem-frontend.vercel.app/change-password?token={token}";

        _emailSenderService.SendResetPasswordEmail(user.Name, email, link);
    }

    public async Task<DefaultResponse> ResetPasswordAsync(UserResetPassword model)
    {
        var response = new DefaultResponse();

        var user = await _userManager.FindByIdAsync(_userId);
        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

        if (!result.Succeeded)
        {
            response.Sucess = false;
            response.Errors.AddError("Erro ao mudar senha");
            return response;
        }

        response.Sucess = true;
        return response;
    }

    #region Aux
    private async Task<string> ValidateStudentCode(string studentCode)
    {
        var student = await _studentsDatas.Where(x => x.StudentCode == studentCode).SingleOrDefaultAsync();

        if (student is null)
            return null;

        return student.Name;
    }

    #endregion

}
