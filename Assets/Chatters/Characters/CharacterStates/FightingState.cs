using Chatters.Characters.Mediators;

namespace Chatters.Characters.CharacterStates
{
    public class FightingState : BaseCharacterState
    {
        public override void ExecuteState(float timeScale)
        {
        }

        public FightingState(BaseMediator.ServiceContainer mediatorServiceContainer) : base(mediatorServiceContainer)
        {
        }
    }
}