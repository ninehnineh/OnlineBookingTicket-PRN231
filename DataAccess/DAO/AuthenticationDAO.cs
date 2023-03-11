using BusinessObject.Entities;
using DTO.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AuthenticationDAO
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSettings _jwtSettings;


        public AuthenticationDAO(UserManager<AppUsers> userManager,
               IOptions<JwtSettings> jwtSettings,
            SignInManager<AppUsers> signInManager,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<AuthenResponse>> Login(AuthenRequest request)
        {
            var response = new BaseResponse<AuthenResponse>();


            var adminEmail = _configuration.GetSection("DefaultAccount").GetSection("Email").Value;
            var adminPassowrd = _configuration.GetSection("DefaultAccount").GetSection("Password").Value;

            if (request.Email.Equals(adminEmail) && request.Password.Equals(adminPassowrd))
            {
                return GenerateTokenForAdminNotManagedByIdentity(response, adminEmail);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            var accessFailedCount = await _userManager.GetAccessFailedCountAsync(user);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Login failed, User is not exist!";
                response.Data = null;
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (result.IsLockedOut)
            {
                response.Success = false;
                response.Message = "Login failed, Your account has been locked (2 minutes) out due to too many failed login attempts.";

                return response;
            }

            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = $"Login failed, Wrong password!.Attention: Your account will be locked if fails 3 times ({2 - accessFailedCount} times left)";
                response.Data = null;
                return response;
            }


            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);


            response = new BaseResponse<AuthenResponse>
            {
                Data = new AuthenResponse
                {
                    Id = user.Id.ToString(),
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email,
                    UserName = user.UserName
                },
                Message = "Login Successful",
                Success = true,
            };


            return response;
        }

        private BaseResponse<AuthenResponse> GenerateTokenForAdminNotManagedByIdentity(BaseResponse<AuthenResponse> response, string adminEmail)
        {
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, adminEmail),
                    new Claim(ClaimTypes.Name, adminEmail),
                    new Claim(ClaimTypes.Role, "Admin")
                };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityTokenForAdmin = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            var identity = new ClaimsIdentity(claims, "custom");
            var principal = new ClaimsPrincipal(identity);

            _httpContextAccessor.HttpContext.User = principal;

            response.Success = true;
            response.Message = "Welcome Admin";
            response.Data = new AuthenResponse
            {
                Email = adminEmail,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityTokenForAdmin),
            };
            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(AppUsers user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<BaseResponse<RegistrationResponse>> Register(RegistrationRequest request)
        {
            var response = new BaseResponse<RegistrationResponse>();

            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"UserName '{request.UserName}' already exists.");
            }

            var user = new AppUsers
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = false
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    //response.Data.Id = existingEmail.Id;
                    response.Success = true;
                    response.Message = "Register Successful";
                    return response;
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"Email {request.Email} already existed.";
                //throw new Exception($"Email {request.Email} already existed.");
                return response;
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
