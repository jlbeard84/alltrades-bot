using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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

        public Task<Tweet> SendTweet(
            string text,
            string responseID = null)
        {
            var endpoint = $"{_options.ApiBase}/{_options.SendTweetEndpoint}";

            var message = new HttpRequestMessage(
                HttpMethod.Post,
                endpoint);

            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("status", text)   
            };

            if (!string.IsNullOrWhiteSpace(responseID))
            {
                body.Add(new KeyValuePair<string, string>(
                    "in_reply_to_status_id", 
                    responseID));
            }

            message.Content = new FormUrlEncodedContent(body);

            return PostTwitter<Tweet>(
                message);
        }

        public string GetMaxIdFileName()
        {
            return _options.MaxIdFileName;
        }
        public string GetMaxIdFilePath()
        {
            var path = System.IO.Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            path = Path.Combine(path, _options.MaxIdFileName);

            return path;
        }

        private async Task<T> PostTwitter<T>(
            HttpRequestMessage message)
        {
            var httpClient = _httpClientFactory.CreateClient();

            message.Headers.Authorization = CreateUserAccessHeader();

            var serializer = new DataContractJsonSerializer(typeof(T));

            var response = await httpClient.SendAsync(message);

            var responseObject = serializer.ReadObject(await response.Content.ReadAsStreamAsync());

            return (T)responseObject;
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

        private AuthenticationHeaderValue CreateUserAccessHeader()
        {
            var timestamp = Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1).ToUniversalTime())
                .TotalSeconds);

            var authorizationValue = $"oauth_consumer_key=\"{_options.ConsumerKey}\",";
            authorizationValue = $"{authorizationValue}oauth_nonce=\"{CreateNonce()}\",";
            authorizationValue = $"{authorizationValue}oauth_signature=\"{_encodedKey}\",";
            authorizationValue = $"{authorizationValue}oauth_signature_method=\"HMAC-SHA1\",";
            authorizationValue = $"{authorizationValue}oauth_timestamp=\"{timestamp}\",";
            authorizationValue = $"{authorizationValue}oauth_token=\"{_options.UserAccessToken}\",";
            authorizationValue = $"{authorizationValue}oauth_version=\"1.0\"";

            var header = new AuthenticationHeaderValue(
                "OAuth", 
                authorizationValue);

            return header;
        }

        private string CreateNonce()
        {
            var guids = new List<Guid>();

            for(var i = 0; i < 4; i++)
            {
                guids.Add(Guid.NewGuid());
            }

            var guidString = guids
                .Select(g => g.ToString().Replace("-", string.Empty))
                .Aggregate((a,b) => $"{a}{b}");

            guidString.Substring(0, 64);

            return guidString;
        }
    }
}