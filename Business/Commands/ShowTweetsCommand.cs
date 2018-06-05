using System.Collections.Generic;
using System.Threading.Tasks;
using alltrades_bot.Core;

namespace alltrades_bot.Business.Commands
{
    public class ShowTweetsCommand : BaseAsyncCommand<IList<string>>
    {
        protected override async Task<IList<string>> ImplementExecute()
        {
            var tweetTexts = new List<string>();

            return tweetTexts;
        }
    }
}