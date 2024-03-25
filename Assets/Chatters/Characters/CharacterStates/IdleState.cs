using Chatters.Characters.Behaviours;
using Chatters.Characters.Mediators;
using Chatters.Characters.Services;
using UnityEngine;

namespace Chatters.Characters.CharacterStates
{
    public class IdleState : BaseCharacterState
    {
        public struct Ctx
        {
            public CharacterMovement Movement;
            public CharacterVisual Visual;
        }

        private Ctx _ctx;

        public IdleState(Ctx ctx) : base()
        {
            _ctx = ctx;
            
            AddBehaviour(new IdleWaiting(new IdleWaiting.Ctx()
            {
                MinimumWaitingTime = 1f,
                MaximumWaitingValue = 6f,
                Visual = _ctx.Visual
            }));

            AddBehaviour(new MoveToDirection(new MoveToDirection.Ctx
            {
                Direction = new Vector3(),
                RandomDirection = true,
                Movement = _ctx.Movement
            }));
        }
    }
}