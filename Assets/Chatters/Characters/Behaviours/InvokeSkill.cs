using Chatters.Characters.Mediators;
using Chatters.Characters.Skills;
using UnityEngine;

namespace Chatters.Characters.Behaviours
{
    public class InvokeSkill : BaseBehaviour
    {
        public struct Ctx
        {
            public BaseActiveSkill ActiveSkill;
            public Vector3 DestinationTarget;
        }

        private Ctx _ctx;

        public override void StartBehaviour()
        {
            base.StartBehaviour();
            _ctx.ActiveSkill.Invoke();
        }

        public InvokeSkill(BaseMediator.ServiceContainer mediatorServiceContainer, Ctx ctx) : base(mediatorServiceContainer)
        {
            _ctx = ctx;
        }
    }
}