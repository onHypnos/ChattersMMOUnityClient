using Chatters.Characters.Mediators;
using Chatters.Characters.Services;
using UnityEngine;
using AnimationState = Chatters.Characters.Services.AnimationState;

namespace Chatters.Characters.Behaviours
{
    public class IdleWaiting : BaseBehaviour
    {
        public struct Ctx
        {
            public float MinimumWaitingTime { get; set; } 
            public float MaximumWaitingValue { get; set; }
            public CharacterVisual Visual { get; set; }
            
        }

        private Ctx _ctx;
        private float _waitingTimeRemain = 0f;

        public IdleWaiting(Ctx ctx) : base()
        {
            _ctx = ctx;
        }

        public override void StartBehaviour()
        {
            base.StartBehaviour();
            _waitingTimeRemain = Random.Range(_ctx.MinimumWaitingTime, _ctx.MaximumWaitingValue);
            _ctx.Visual.SetState(AnimationState.Idle);
        }

        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
            _waitingTimeRemain -= deltaTime;
        }

        public override bool CompleteRequirements() => _waitingTimeRemain <= 0f;
    }
}