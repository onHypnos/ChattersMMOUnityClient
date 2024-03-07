using Chatters.Characters.Mediators;

namespace Chatters.Characters.CharacterStates
{
    public class AfkState : BaseCharacterState
    {
        public AfkState(BaseMediator.ServiceContainer mediatorServiceContainer) : base(mediatorServiceContainer)
        {
        }
    }
}