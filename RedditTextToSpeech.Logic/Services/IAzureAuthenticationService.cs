using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    internal interface IAzureAuthenticationService
    {
        Task<string> FetchTokenAsync();
    }
}
