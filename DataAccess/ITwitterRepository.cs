using System.Threading.Tasks;
using alltrades_bot.Core.Entities;

namespace alltrades_bot.DataAccess
{
    public interface ITwitterRepository
    {
        string ApiBase { get; }

        Task<ITwitterAuth> GetAccessToken();
    }
}