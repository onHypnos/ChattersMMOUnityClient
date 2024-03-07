namespace Chatters.Services.Updater.Interfaces
{
    public interface ILateUpdatable : IExecutor
    {
        public void LateExecute();
    }
}