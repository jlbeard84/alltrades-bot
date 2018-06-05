namespace alltrades_bot.Core
{
    public abstract class BaseCommand<T>
    {
        public T Execute()
        {
            var returnObject = ImplementExecute();

            return returnObject;
        }

        protected abstract T ImplementExecute();
    }
}