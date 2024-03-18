using Chatters.Characters.Behaviours;
using Chatters.Characters.Mediators;
using Unity.VisualScripting;
using UnityEngine;

namespace Chatters.Characters.CharacterStates
{
    public class FightingState : BaseCharacterState
    {
        public override void ExecuteState(float timeScale)
        {
        }

        public FightingState(BaseMediator.ServiceContainer mediatorServiceContainer) : base(mediatorServiceContainer)
        {
            AddBehaviour(new SkillExecutor(mediatorServiceContainer, new SkillExecutor.Ctx()));
            //1.Найти цель
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