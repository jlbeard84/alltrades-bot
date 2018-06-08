using System;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.DataAccess;

namespace alltrades_bot.Business.Commands
{
    public class RunMentionWatchCommand : BaseAsyncCommand<bool>
    {
        private const int DelayMilliseconds = 60000;

        private readonly ITwitterRepository _twitterRepository;
        private readonly string _lastID;

        public RunMentionWatchCommand(
            ITwitterRepository twitterRepository,
            string lastID = null)
        {
            _twitterRepository = twitterRepository;
            _lastID = lastID;
        }

        protected override async Task<bool> ImplementExecute()
        {
            try
            {
                var mentionTask = _twitterRepository
                    .GetMentions(_lastID)
                    .ContinueWith(async (Task<SearchResponse> searchResultTask) => {

                        var lastId = string.Empty;

                        if (searchResultTask.IsCompletedSuccessfully) {
                            var results = searchResultTask.Result;

                            if (results != null)
                            {
                                lastId = results.search_metadata.max_id_str;

                                var handleMentionsTask = new HandleMentionsCommand(
                                    results.statuses);

                                Task.Factory.StartNew(async () => {
                                    await handleMentionsTask.Execute();
                                });
                            }
                        }

                        await Task.Delay(DelayMilliseconds);

                        var command = new RunMentionWatchCommand(
                            _twitterRepository,
                            lastId);

                        await command.Execute();
                });
            }
            catch(Exception ex)
            {
                //log somehow
                return false;
            }

            return true;
        }
    }
}