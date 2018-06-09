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

        public RunMentionWatchCommand(
            ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        protected override async Task<bool> ImplementExecute()
        {
            try
            {
                var lastIdCommand = new ReadMaxIdCommand(
                    _twitterRepository);

                var lastId = await lastIdCommand.Execute();

                var mentionTask = _twitterRepository
                    .GetMentions(lastId)
                    .ContinueWith(async (Task<SearchResponse> searchResultTask) => {

                        if (searchResultTask.IsCompletedSuccessfully) {
                            var results = searchResultTask.Result;

                            if (results != null)
                            {
                                var currentMaxFileId = results.search_metadata.max_id_str;

                                var writeMaxIdCommand = new WriteMaxIdCommand(
                                    currentMaxFileId,
                                    _twitterRepository);
                                
                                await writeMaxIdCommand.Execute();

                                var handleMentionsTask = new HandleMentionsCommand(
                                    results.statuses,
                                    _twitterRepository);

                                Task.Factory.StartNew(async () => {
                                    await handleMentionsTask.Execute();
                                });
                            }
                        }

                        await Task.Delay(DelayMilliseconds);

                        var command = new RunMentionWatchCommand(
                            _twitterRepository);

                        await command.Execute();
                });
            }
            catch(Exception)
            {
                //log somehow
                return false;
            }

            return true;
        }
    }
}