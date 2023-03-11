using OnlineBookingTicket.Contracts;
using System.Net.Http.Headers;

namespace OnlineBookingTicket.Services
{
    public class BaseHttpService
    {
        private readonly HttpClient _httpClient;
        public readonly ILocalStorageService _localStorageService;

        public BaseHttpService(ILocalStorageService localStorageService,
            HttpClient httpClient)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public void AddBearerToken()
        {
            if (_localStorageService.Exists("token"))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _localStorageService.GetStorageValue<string>("token"));
        }
    }
}
