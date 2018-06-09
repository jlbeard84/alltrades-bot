using System.IO;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.DataAccess;
using Newtonsoft.Json;

namespace alltrades_bot.Business.Commands
{
    public class ReadMaxIdCommand: BaseAsyncCommand<string>
    {
        private readonly ITwitterRepository _twitterRepository;

        public ReadMaxIdCommand(
            ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        protected override async Task<string> ImplementExecute()
        {
            var filePath = _twitterRepository
                .GetMaxIdFilePath();

            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            var maxFileId = new MaxFileId();

            var stringToDeserialize = await File.ReadAllTextAsync(filePath);

            var dynamicMaxFile = JsonConvert
                .DeserializeAnonymousType(stringToDeserialize, maxFileId);

            return dynamicMaxFile.Id;
        }
    }
}