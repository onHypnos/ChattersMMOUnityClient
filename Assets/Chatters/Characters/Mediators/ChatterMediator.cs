using Chatters.Characters.BattleProfiles;
using Chatters.Characters.CharacterStates;
using Chatters.Characters.Services;
using Chatters.Services.SaveLoad;
using Chatters.Services.UI;
using Chatters.Services.Updater;
using UnityEngine;

namespace Chatters.Characters.Mediators
{
    public class ChatterMediator : BaseMediator
    {
        public struct Ctx
        {
            public int ID;
            public UpdateRunner Runner;
            public UIMediator UiMediator;
            public Vector3 SpawningDestinationPoint;
        }

        private Ctx _ctx;
        private Vector3 _spawningDestinationPoint;
        private ChatterBattleProfile _bProfile => _profile as ChatterBattleProfile;
        
        [SerializeField] protected ChatterUIManager UIManagerManager;

        public void Init(Ctx ctx)
        {
            _ctx = ctx;
            _spawningDestinationPoint = _ctx.SpawningDestinationPoint;
            BaseInit(_ctx.ID, _ctx.Runner,_ctx.UiMediator);
            _bProfile.Init();
            UIManagerManager.Init(_serviceContainer);
        }

        protected override void InitStates()
        {
            base.InitStates();
            _states.Add(typeof(IdleState), new IdleState(_serviceContainer));
            _states.Add(typeof(AfkState), new AfkState(_serviceContainer));
            _states.Add(typeof(SpawningState), new SpawningState(_serviceContainer, new SpawningState.Ctx(_spawningDestinationPoint)));
            _states.Add(typeof(FightingState), new FightingState(_serviceContainer));

            EnterState<SpawningState>();
            _states[typeof(SpawningState)].OnAllBehavioursEnd += EnterState<IdleState>;
        }

        public void LoadSetup(ChatterData chatterData, string containerDisplayName)
        {
            UIManagerManager.ChangeNickname(containerDisplayName);
            UIManagerManager.ChangeColor();
            _ctx.ID = chatterData.ID.GetHashCode();
            _bProfile.Init();
            
            var visual = _visual as ChatterVisual;
            if (visual != null) visual.SetupChatterVisual(chatterData.SavedVisual);
        }

        public void ShowMessage(string message)
        {
            UIManagerManager.ShowMessage(message);
        }
    }
}