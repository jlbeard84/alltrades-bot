using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;
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

        public Task<List<Tweet>> GetTweets()
        {
            var endpoint = $"{_options.ApiBase}/{_options.UserTimelineEndpoint}?screen_name={_options.UserId}&count=200";

            return CallTwitter<List<Tweet>>(endpoint);
        }

        public Task<SearchResponse> GetMentions(
            string sinceID = null)
        {
            var endpoint = $"{_options.ApiBase}/{_options.SearchTweetsEndpoint}?q=@{_options.UserId}&count=100&result_type=recent";

            if (!string.IsNullOrWhiteSpace(sinceID))
            {
                endpoint = $"{endpoint}&since_id={sinceID}";
            }

            return CallTwitter<SearchResponse>(endpoint);
        }

        private async Task<T> CallTwitter<T>(
            string endpoint, 
            HttpRequestMessage message = null)
        {
            var token = await GetAccessToken();

            var httpClient = _httpClientFactory.CreateClient();

            if (message == null)
            {
                message = new HttpRequestMessage(
                    HttpMethod.Get, 
                    endpoint);
            }

            message.Headers.Authorization = new AuthenticationHeaderValue(
                token.token_type, 
                token.access_token);

            var serializer = new DataContractJsonSerializer(typeof(T));

            var response = await httpClient.SendAsync(message);

            var responseObject = serializer.ReadObject(await response.Content.ReadAsStreamAsync());

            return (T)responseObject;
        }
    }
}