using Chatters.Characters.CharacterStates;
using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Characters.Behaviours
{
    public class SkillExecutor : ComplexBehaviour
    {
        public struct Ctx
        {
            
        }

        public BaseMediator Target = null;
        
        
        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
        }

        public SkillExecutor(BaseMediator.ServiceContainer mediatorServiceContainer, Ctx ctx) : base(mediatorServiceContainer)
        {
            if (!Target)
            {
                var target = mediatorServiceContainer.BaseMediator.GetNextTarget();
                if (target)
                {
                    mediatorServiceContainer.BaseMediator.EnterState<IdleState>();
                }
            }
        }
    }

    
}