using DataAccess.DAO;
using DataAccess;
using DTO.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;

namespace Repository.AuthenticationRepository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationDAO _authenticationDAO;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AuthenticationRepository(UserManager<AppUsers> userManager,
               IOptions<JwtSettings> jwtSettings,
            SignInManager<AppUsers> signInManager,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _signInManager = signInManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _authenticationDAO = new AuthenticationDAO(_userManager, _jwtSettings, _signInManager, _configuration, _httpContextAccessor);
        }
        public async Task<BaseResponse<AuthenResponse>> Login(AuthenRequest request)
        {
            return await _authenticationDAO.Login(request);
        }

        public async Task<BaseResponse<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return await _authenticationDAO.Register(request);
        }
    }
}
