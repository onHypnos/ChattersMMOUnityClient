using Chatters.Characters.Behaviours;
using Chatters.Characters.Mediators;
using Chatters.Characters.Services;
using UnityEngine;

namespace Chatters.Characters.CharacterStates
{
    public class SpawningState : BaseCharacterState
    {
        public struct Ctx
        {
            public CharacterMovement MovementService;
            public Vector3 SpawnDestination;
        }
        
        private Ctx _ctx;

        public SpawningState( Ctx ctx) : base()
        {
            _ctx = ctx;
            AddBehaviour(new MoveToPosition(
                new MoveToPosition.Ctx
                {
                    MovementTarget = _ctx.SpawnDestination + new Vector3(Random.Range(-10f, 10f), 0, 0),
                    MovementService = _ctx.MovementService
                })
            );
        }
    }
}