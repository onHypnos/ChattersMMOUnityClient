using Chatters.Characters.CharacterStates;
using Chatters.Characters.Mediators;
using UnityEngine;
using UniRx;

namespace Chatters.Characters.Behaviours
{
    public class SkillExecutor : ComplexBehaviour
    {
        public struct Ctx
        {
            
        }

        private Ctx _ctx;
        private BaseMediator _executor;
        public BaseMediator Target = null;


        public SkillExecutor(BaseMediator.ServiceContainer mediatorServiceContainer, Ctx ctx) : base(
            mediatorServiceContainer)
        {
            _ctx = ctx;
            _executor = mediatorServiceContainer.BaseMediator;
        }

        public override void StartBehaviour()
        {
            base.StartBehaviour();
            if (!CheckTarget(_executor))
            {
                _executor.EnterState<IdleState>();
            }
        }

        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
        }

        private bool CheckTarget(BaseMediator mediator)
        {
            if (!Target)
            {
                var target = mediator.GetNextTarget();
                if (!target)
                {
                    return false;
                }

                SetNewTarget(target);
            }
            return true;
        }

        private void SetNewTarget(BaseMediator target)
        {
            target.OnDestroyMediator.Subscribe(_ => CheckTarget(_executor));
        }
    }

    
}