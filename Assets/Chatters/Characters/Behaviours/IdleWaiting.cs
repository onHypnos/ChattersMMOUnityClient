using Chatters.Characters.Mediators;
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
            
            public Ctx(float min=1f, float max= 6f)
            {
                MinimumWaitingTime = min;
                MaximumWaitingValue = max;
            }
        }

        private Ctx _ctx;
        private float _waitingTimeRemain = 0f;
        
        public override void StartBehaviour()
        {
            base.StartBehaviour();
            _waitingTimeRemain = Random.Range(_ctx.MinimumWaitingTime, _ctx.MaximumWaitingValue);
            MediatorServiceContainer.Visual.SetState(AnimationState.Idle);
        }

        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
            _waitingTimeRemain -= deltaTime;
        }

        public override bool CompleteRequirements() => _waitingTimeRemain <= 0f;

        public IdleWaiting(BaseMediator.ServiceContainer mediatorServiceContainer, Ctx ctx) : base(mediatorServiceContainer)
        {
            _ctx = ctx;
        }
    }
}