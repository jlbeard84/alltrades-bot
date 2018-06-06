using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Options;
using Microsoft.Extensions.Options;

namespace alltrades_bot.DataAccess
{
    public class TwitterRepository : ITwitterRepository
    {
        private readonly ITwitterOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public TwitterRepository(
            IHttpClientFactory httpClientFactory,
            IOptions<TwitterOptions> options)
        {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        public string ApiBase => _options?.ApiBase;

        private string _oAuthEndpoint => $"{_options?.ApiBase}/{_options?.OAuthEndpoint}";

        private string _encodedKey
        {
            get
            {
                var twitterKey = $"{_options?.ConsumerKey}:{_options?.ConsumerSecret}";
                var encodedBytes = Encoding.UTF8.GetBytes(twitterKey);
                var encodedText = Convert.ToBase64String(encodedBytes);

                return encodedText;
            }
        }

        public async Task<ITwitterAuth> GetAccessToken()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post, 
                _oAuthEndpoint);

            requestMessage.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", _options.OAuthGrantType)
            });

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", _encodedKey);

            var serializer = new DataContractJsonSerializer(typeof(TwitterAuth));

            var response = await httpClient.SendAsync(requestMessage);

            var authToken = serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as ITwitterAuth;

            return authToken;
        }
    }
}