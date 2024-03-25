using Chatters.Characters.Behaviours;
using Chatters.Characters.Mediators;
using UniRx;
using UnityEngine;

namespace Chatters.Characters.CharacterStates
{
    public class FightingState : BaseCharacterState
    {
        public struct Ctx
        {
            
        }

        private Ctx _ctx;
        private BaseMediator _executor;
        public BaseMediator Target = null;
        public override void ExecuteState(float timeScale)
        {
            if (!CheckTarget(_executor))
            {
                _executor.EnterState<IdleState>();
            }
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
        

        public FightingState(Ctx ctx) : base()
        {
            _ctx = ctx;
            AddBehaviour(new SkillExecutor(new SkillExecutor.Ctx()
            {
                
            }));
            //2.Выбрать скилл
            //3.Добежать до рейнджа скилла к цели
            //4.Использовать Скилл
            //повторить
        }

        public override void Enter()
        {
            base.Enter();
            
        }
    }
}