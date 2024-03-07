using Chatters.Characters.Behaviours;
using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Characters.CharacterStates
{
    public class SpawningState : BaseCharacterState
    {
        public struct Ctx
        {
            public Vector3 SpawnDestination;
            public Ctx(Vector3 spawnDestination)
            {
                SpawnDestination = spawnDestination;
            }
        }
        
        private Ctx _ctx;

        public SpawningState(BaseMediator.ServiceContainer mediatorServiceContainer, Ctx ctx) : base(mediatorServiceContainer)
        {
            _ctx = ctx;
            AddBehaviour(new MoveToPosition(
                mediatorServiceContainer,
                new MoveToPosition.Ctx
                {
                    MovementTarget = _ctx.SpawnDestination + new Vector3(Random.Range(-10f, 10f), 0, 0)
                })
            );
        }
    }
}