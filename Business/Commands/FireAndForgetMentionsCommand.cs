using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.DataAccess;

namespace alltrades_bot.Business.Commands
{
    public class FireAndForgetMentionsCommand : BaseAsyncCommand<bool>
    {
        private readonly ITwitterRepository _twitterRepository;

        public FireAndForgetMentionsCommand(
            ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        protected override Task<bool> ImplementExecute()
        {
            var mentionsTask = new RunMentionWatchCommand(
                _twitterRepository);

            Task.Factory.StartNew(async () => {
                await mentionsTask.Execute();
            });

            return Task.FromResult(true);
        }
    }
}