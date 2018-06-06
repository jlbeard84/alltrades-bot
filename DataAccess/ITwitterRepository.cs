using System.Collections.Generic;
using System.Threading.Tasks;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.DataAccess
{
    public interface ITwitterRepository
    {
        string ApiBase { get; }

        Task<ITwitterAuth> GetAccessToken();

        Task<List<Tweet>> GetTweets();
    }
}