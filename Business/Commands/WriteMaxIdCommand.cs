using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.DataAccess;
using Newtonsoft.Json;

namespace alltrades_bot.Business.Commands
{
    public class WriteMaxIdCommand : BaseAsyncCommand<string>
    {
        private readonly string _fileId;

        private readonly ITwitterRepository _twitterRepository;

        public WriteMaxIdCommand(
            string fileId,
            ITwitterRepository twitterRepository)
        {
            _fileId = fileId;
            _twitterRepository = twitterRepository;
        }

        protected override async Task<string> ImplementExecute()
        {
            var filePath = _twitterRepository
                .GetMaxIdFilePath();

            if (!File.Exists(filePath))
            {
                using(var stream = File.Create(filePath))
                {
                    //do nothing
                };
            }

            var maxObject = new MaxFileId
            {
                Id = _fileId
            };

            var stringToWrite = JsonConvert.SerializeObject(
                maxObject,
                Formatting.Indented);

            await File.WriteAllTextAsync(
                filePath,
                stringToWrite);

            return _fileId;
        }
    }
}