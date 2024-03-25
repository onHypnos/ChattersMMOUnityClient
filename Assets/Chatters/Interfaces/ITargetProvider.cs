using Chatters.Characters.Mediators;

namespace Chatters.Interfaces
{
    public interface ITargetProvider
    {
        BaseMediator GetTarget();
    }
}