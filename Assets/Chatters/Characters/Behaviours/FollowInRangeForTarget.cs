using Chatters.Characters.Mediators;
using Chatters.Characters.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Characters.Behaviours
{
    public class FollowInRangeForTarget : BaseBehaviour
    {
        public struct Ctx
        {
            public CharacterMovement MovementService { get; set; }
            public Transform MovementTransform { get; set; }
            public Transform MovementTarget { get; set; }
            public float Range { get; set; }
        }

        private Ctx _ctx;

        public override void StartBehaviour()
        {
            base.StartBehaviour();
        }


        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
            _ctx.MovementService.MoveTo(_ctx.MovementTarget.position, deltaTime);
        }

        public override bool CompleteRequirements()
        {
            return (_ctx.MovementTarget.position - _ctx.MovementTransform.position).magnitude < _ctx.Range;
        }
        
        public FollowInRangeForTarget(Ctx ctx) : base()
        {
            _ctx = ctx;
        }
    }
}