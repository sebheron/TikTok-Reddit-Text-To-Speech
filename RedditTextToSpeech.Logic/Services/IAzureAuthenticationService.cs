using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    internal interface IAzureAuthenticationService
    {
        Task<string> FetchTokenAsync();
    }
}
