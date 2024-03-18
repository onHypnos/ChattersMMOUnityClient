using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Characters.Behaviours
{
    public class MoveToPosition : BaseBehaviour
    {
        public struct Ctx
        {
            public Vector3 MovementTarget { get; set; }
        }

        private Ctx _ctx;

        public override void StartBehaviour()
        {
            base.StartBehaviour();
        }

        public MoveToPosition(BaseMediator.ServiceContainer mediatorServiceContainer, Ctx ctx) : base(mediatorServiceContainer)
        {
            _ctx = ctx;
        }

        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
            MediatorServiceContainer.BaseCharacterMovement.MoveTo(_ctx.MovementTarget,deltaTime);
        }

        public override bool CompleteRequirements()
        {
            return (_ctx.MovementTarget - MediatorServiceContainer.BaseMediator.transform.position).magnitude < 1f;
        }
    }
}