using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineBookingTicket.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using OnlineBookingTicket.Models.AuthenVMs;
using OnlineBookingTicket.Response;

namespace OnlineBookingTicket.Controllers
{
    public class AuthenticationController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILocalStorageService _localStorageService;
        private string apiUrl = "";

        public AuthenticationController(IHttpContextAccessor httpContextAccessor,
            ILocalStorageService localStorageService)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _tokenHandler = new JwtSecurityTokenHandler();
            _httpContextAccessor = httpContextAccessor;
            _localStorageService = localStorageService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginVM request)
        {
            apiUrl = "https://localhost:7099/api/Authentication/login";

            HttpResponseMessage response = await SendRequest(apiUrl, request);

            BaseResponse<LoginResponseVM> data = await GetDataResponse<BaseResponse<LoginResponseVM>>(response);

            if (response.IsSuccessStatusCode && data.Success)
            {
                var tokenContext = _tokenHandler.ReadJwtToken(data.Data.Token);
                var claims = ParseClaims(tokenContext);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                _localStorageService.SetStorageValue("token", data.Data.Token);
                _httpContextAccessor.HttpContext.User = user;

                if (data.Data.Email.Contains("admin"))
                {
                    return View("Welcome");
                }
                else
                {
                    return RedirectToAction("Index", "Movies");
                }
            }
            else
            {
                ViewBag.Message = data.Message;
                return View("Login");
            }
        }

        private async Task<HttpResponseMessage> SendRequest<T>(string Url, T requestBody)
        {
            string strData = JsonSerializer.Serialize(requestBody);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{Url}", contentData);
            return response;
        }

        private static async Task<T> GetDataResponse<T>(HttpResponseMessage response)
        {
            var contentString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<T>(contentString, options);
            return data;
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(
                new Claim(ClaimTypes.Name, tokenContent.Subject)
                );
            return claims;
        }

        public async Task<ActionResult> Logout()
        {
            _localStorageService.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM register)
        {
            apiUrl = "https://localhost:7099/api/Authentication/register";
            HttpResponseMessage response = await SendRequest(apiUrl, register);
            BaseResponse<RegisterResponseVM> data = await GetDataResponse<BaseResponse<RegisterResponseVM>>(response);

            if (response.IsSuccessStatusCode && data.Success)
            {
                ViewBag.Message = data.Message;
                return View("Login");
            }
            else
            {
                ViewBag.Message = data.Message;
                return View();
            }

        }
    }
}
