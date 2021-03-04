using System.Threading.Tasks;

namespace SafetyBP.Interfaces
{
    public interface IPopUp
    {
        Task Show(string message);
    }
}
