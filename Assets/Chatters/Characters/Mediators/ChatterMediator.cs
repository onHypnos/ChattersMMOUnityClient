using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Chatters.Characters.BattleProfiles;
using Chatters.Characters.CharacterStates;
using Chatters.Characters.Fabrics;
using Chatters.Characters.Services;
using Chatters.Interfaces;
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
            public ITargetProvider TargetProvider { get; set; }
            public CharacterBuilder CharacterBuilder { get; set; }
        }

        private Ctx _ctx;
        private Vector3 _spawningDestinationPoint;
        
        [SerializeField] protected ChatterUIManager UIManagerManager;

        public void Init(Ctx ctx)
        {
            _ctx = ctx;
            _spawningDestinationPoint = _ctx.SpawningDestinationPoint;
            BaseInit(_ctx.ID, _ctx.Runner,_ctx.UiMediator, _ctx.TargetProvider);
            UIManagerManager.Init(_serviceContainer);
        }

        protected override void InitStates()
        {
            base.InitStates();
            _states.Add(typeof(IdleState), new IdleState(new IdleState.Ctx
            {
                Movement = _serviceContainer.BaseCharacterMovement,
                Visual = _serviceContainer.Visual,

            }));
            
            _states.Add(typeof(AfkState), new AfkState());
            
            _states.Add(typeof(SpawningState), new SpawningState(new SpawningState.Ctx
            {
                MovementService = _serviceContainer.BaseCharacterMovement,
                SpawnDestination = _spawningDestinationPoint
            }));
            _states.Add(typeof(FightingState), new FightingState(new FightingState.Ctx()
            {
                
            }));

            EnterState<SpawningState>();
            _states[typeof(SpawningState)].OnAllBehavioursEnd += EnterState<IdleState>;
        }

        public void LoadSetup(ChatterData chatterData, string containerDisplayName)
        {
            UIManagerManager.ChangeNickname(containerDisplayName);
            UIManagerManager.ChangeColor();
            _ctx.ID = chatterData.ID.GetHashCode();
            
            var visual = _visual as ChatterVisual;
            if (visual != null)
            {
                visual.SetupBuilder(_ctx.CharacterBuilder);
                visual.SetupChatterVisual(chatterData.SavedVisual);
            }
        }

        public void ShowMessage(string message)
        {
            UIManagerManager.ShowMessage(message);
        }
    }
}