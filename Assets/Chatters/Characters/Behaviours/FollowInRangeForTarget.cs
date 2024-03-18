using Chatters.Characters.Mediators;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Characters.Behaviours
{
    public class FollowInRangeForTarget : BaseBehaviour
    {
        public struct Ctx
        {
            public Transform MovementTransform;
            public Transform MovementTarget { get; set; }
            public float Range;
        }

        private Ctx _ctx;

        public override void StartBehaviour()
        {
            base.StartBehaviour();
        }


        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
            MediatorServiceContainer.BaseCharacterMovement.MoveTo(_ctx.MovementTarget.position, deltaTime);
        }

        public override bool CompleteRequirements()
        {
            return (_ctx.MovementTarget.position - _ctx.MovementTransform.position).magnitude < _ctx.Range;
        }
        
        public FollowInRangeForTarget(BaseMediator.ServiceContainer mediatorServiceContainer, Ctx ctx) : base(mediatorServiceContainer)
        {
            _ctx = ctx;
        }
    }
}