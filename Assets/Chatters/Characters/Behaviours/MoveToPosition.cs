using Chatters.Characters.Mediators;
using Chatters.Characters.Services;
using UnityEngine;

namespace Chatters.Characters.Behaviours
{
    public class MoveToPosition : BaseBehaviour
    {
        public struct Ctx
        {
            public Vector3 MovementTarget { get; set; }
            public CharacterMovement MovementService { get; set; }
        }

        private Ctx _ctx;

        public override void StartBehaviour()
        {
            base.StartBehaviour();
        }

        public MoveToPosition(Ctx ctx) : base()
        {
            _ctx = ctx;
        }

        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
            _ctx.MovementService.MoveTo(_ctx.MovementTarget, deltaTime);
        }

        public override bool CompleteRequirements()
        {
            return (_ctx.MovementTarget - _ctx.MovementService.transform.position).magnitude < 1f;
        }
    }
}