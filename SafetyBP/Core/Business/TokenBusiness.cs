using SafetyBP.Domain.Interfaces;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SafetyBP.Core.Business
{
    public class TokenBusiness : ITokenBusiness
    {
        private string _token = null;

        public async Task<string> GetTokenAsync()
        {
            if (_token == null) _token = await SecureStorage.GetAsync("token");
            return _token;
        }

        public bool RemoveToken()
        {
            _token = null;
            return SecureStorage.Remove("token");
        }

        public async Task SetTokenAsync(string token)
        {
            await SecureStorage.SetAsync("token", token);
        }
    }
}
