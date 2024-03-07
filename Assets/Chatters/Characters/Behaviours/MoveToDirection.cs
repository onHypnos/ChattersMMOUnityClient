using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Characters.Behaviours
{
    public class MoveToDirection : BaseBehaviour
    {
        public struct Ctx
        {
            public Vector3 Direction { get; set; }
            public bool RandomDirection { get; set; }
        }

        private Ctx _ctx;
        private float _movingTimeDefaultValue = 2f;
        private float _movingTimeRemain = 0f;
        private float _randomDeltaMovingTime = 1.5f;

        public override void StartBehaviour()
        {
            base.StartBehaviour();
            Reset();
        }

        private void Reset()
        {
            _ctx.Direction = new Vector3(Mathf.Pow(-1, Random.Range(1, 3)), 0, 0);
            _movingTimeRemain = _movingTimeDefaultValue + Random.Range(-_randomDeltaMovingTime, _randomDeltaMovingTime);
        }

        private void ResetOnBound()
        {
            _ctx.Direction = _ctx.Direction * -1;
        }

        public MoveToDirection(BaseMediator.ServiceContainer mediatorServiceContainer, Ctx ctx) : base(mediatorServiceContainer)
        {
            _ctx = ctx;
        }
        
        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
            _movingTimeRemain -= deltaTime;
            if (MediatorServiceContainer.CharacterMovement)
            {
                MediatorServiceContainer.CharacterMovement.Move(_ctx.Direction, deltaTime);
            }
        }


        public override bool CompleteRequirements()
        {
            return _movingTimeRemain <= 0f;
        }
    }
}