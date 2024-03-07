namespace Chatters.Services.Updater.Interfaces
{
    public interface IFixedUpdatable : IExecutor
    {
        public void FixedExecute();
    }
}