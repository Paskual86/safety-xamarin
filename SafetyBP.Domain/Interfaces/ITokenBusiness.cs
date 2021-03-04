using System.Threading.Tasks;

namespace SafetyBP.Domain.Interfaces
{
    public interface ITokenBusiness
    {
        Task<string> GetTokenAsync();
        bool RemoveToken();
        Task SetTokenAsync(string token);
    }
}