using System;
using Chatters.Characters.CharacterStates;
using Chatters.Characters.Services;
using Chatters.Services.UI;
using Chatters.Services.Updater;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Characters.Mediators
{
    public class EnemyMediator : BaseMediator
    {
        public struct Ctx
        {
            public int ID;
            public UpdateRunner Runner;
            public UIMediator UiMediator;
            [FormerlySerializedAs("SpriteLayer")] public int SpriteLayerOrder;
        }

        private Ctx _ctx;
        
        public void Init(Ctx ctx)
        {
            _ctx = ctx;
            BaseInit(ctx.ID, _ctx.Runner,_ctx.UiMediator);
            _visual.SetLayerOrder(_ctx.SpriteLayerOrder);
        }
        
        protected override void InitStates()
        {
            base.InitStates();
            _states.Add(typeof(IdleState), new IdleState(_serviceContainer));
            _states.Add(typeof(AfkState), new AfkState(_serviceContainer));
            EnterState<IdleState>();
        }

        
    }
}