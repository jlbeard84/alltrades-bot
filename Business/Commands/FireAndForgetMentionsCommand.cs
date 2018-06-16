using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.DataAccess;
using alltrades_bot.Factories;

namespace alltrades_bot.Business.Commands
{
    public class FireAndForgetMentionsCommand : BaseAsyncCommand<bool>
    {
        private readonly ITwitterRepository _twitterRepository;
        private readonly IHashtagResponseFactory _hashtagResponseFactory;

        public FireAndForgetMentionsCommand(
            ITwitterRepository twitterRepository,
            IHashtagResponseFactory hashtagResponseFactory)
        {
            _twitterRepository = twitterRepository;
            _hashtagResponseFactory = hashtagResponseFactory;
        }

        protected override Task<bool> ImplementExecute()
        {
            var mentionsTask = new RunMentionWatchCommand(
                _twitterRepository,
                _hashtagResponseFactory);

            Task.Factory.StartNew(async () => {
                await mentionsTask.Execute();
            });

            return Task.FromResult(true);
        }
    }
}