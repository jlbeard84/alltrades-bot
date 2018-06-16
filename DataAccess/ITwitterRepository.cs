using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.DataAccess {
    public interface ITwitterRepository {
        string ApiBase { get; }

        Task<ITwitterAuth> GetAccessToken ();

        Task<List<Tweet>> GetTweets ();

        Task<SearchResponse> GetMentions (
            string sinceID = null);

        string GetMaxIdFileName ();

        string GetMaxIdFilePath ();

        Task<Tweet> SendTweet (
            string text,
            string responseID = null);
    }
}