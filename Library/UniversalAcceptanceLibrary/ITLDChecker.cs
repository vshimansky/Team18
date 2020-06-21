using System.Threading.Tasks;

namespace UniversalAcceptanceLibrary
{
    public interface ITLDChecker
    {
        Task<bool> IsTLDExistsAsync(string tld);
    }
}
