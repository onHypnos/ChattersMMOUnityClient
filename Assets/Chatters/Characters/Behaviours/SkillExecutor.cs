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
        


        public SkillExecutor(Ctx ctx) : base()
        {
            _ctx = ctx;
        }

        public override void StartBehaviour()
        {
            base.StartBehaviour();
            
        }

        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
        }

        
    }

    
}