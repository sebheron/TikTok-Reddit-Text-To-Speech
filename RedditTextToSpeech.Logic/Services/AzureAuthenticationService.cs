using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The azure authentication service.
    /// </summary>
    public class AzureAuthenticationService : IAzureAuthenticationService
    {
        private string subscriptionKey;
        private string tokenFetchUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAuthenticationService"/> class.
        /// </summary>
        /// <param name="tokenFetchUri">The token fetch uri.</param>
        /// <param name="subscriptionKey">The subscription key.</param>
        public AzureAuthenticationService(string tokenFetchUri, string subscriptionKey)
        {
            if (string.IsNullOrWhiteSpace(tokenFetchUri))
            {
                throw new ArgumentNullException(nameof(tokenFetchUri));
            }
            if (string.IsNullOrWhiteSpace(subscriptionKey))
            {
                throw new ArgumentNullException(nameof(subscriptionKey));
            }
            this.tokenFetchUri = tokenFetchUri;
            this.subscriptionKey = subscriptionKey;
        }

        /// <summary>
        /// Fetches the token asynchronously.
        /// </summary>
        /// <returns>Awaitable task returning token.</returns>
        public async Task<string> FetchTokenAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                UriBuilder uriBuilder = new UriBuilder(tokenFetchUri);

                HttpResponseMessage result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null).ConfigureAwait(false);
                return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
    }
}