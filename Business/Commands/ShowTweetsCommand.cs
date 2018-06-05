using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities;

namespace alltrades_bot.Business.Commands
{
    public class ShowTweetsCommand : BaseAsyncCommand<IList<string>>
    {
        private const string TwitterApiBase = "https://api.twitter.com";
        private const string TwitterOAuthEndpoint = "oauth2/token";
        private const string TwitterOAuthGrantType = "client_credentials";

        private const string TwitterConsumerKey = "";
        private const string TwitterConsumerSecret = "";


        protected override async Task<IList<string>> ImplementExecute()
        {
            var oauthEndpoint = $"{TwitterApiBase}/{TwitterOAuthEndpoint}";
            
            var twitterKey = $"{TwitterConsumerKey}:{TwitterConsumerSecret}";
            var encodedBytes = Encoding.UTF8.GetBytes(twitterKey);
            var encodedText = Convert.ToBase64String(encodedBytes);

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("grant_type", TwitterOAuthGrantType);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedText);

            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post, 
                oauthEndpoint);

            var serializer = new DataContractJsonSerializer(typeof(TwitterAuth));

            var response = await httpClient.PostAsync(
                oauthEndpoint, 
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", TwitterOAuthGrantType)
                }));

            var authToken = serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as ITwitterAuth;

            var tweetTexts = new List<string>();

            return tweetTexts;
        }
    }
}