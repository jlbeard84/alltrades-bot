using System.Threading.Tasks;

namespace alltrades_bot.Core
{
    public abstract class BaseAsyncCommand<T>
    {
        public Task<T> Execute()
        {
            return ImplementExecute();
        }

        protected abstract Task<T> ImplementExecute();
    }
}