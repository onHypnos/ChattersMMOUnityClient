using Chatters.Characters.Behaviours;
using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Characters.CharacterStates
{
    public class IdleState : BaseCharacterState
    {

        public IdleState(BaseMediator.ServiceContainer mediatorServiceContainer) : base(mediatorServiceContainer)
        {
            AddBehaviour(new IdleWaiting(mediatorServiceContainer, new IdleWaiting.Ctx(1f)));
            AddBehaviour(new MoveToDirection(mediatorServiceContainer, new MoveToDirection.Ctx
            {
                Direction = new Vector3(),
                RandomDirection = true
            }));
        }
    }
}