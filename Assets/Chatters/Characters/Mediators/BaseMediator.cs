using System;
using System.Collections.Generic;
using Chatters.Characters.BattleProfiles;
using Chatters.Characters.CharacterStates;
using Chatters.Characters.Services;
using Chatters.Services.UI;
using Chatters.Services.Updater;
using Chatters.Services.Updater.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Characters.Mediators
{
    public abstract class BaseMediator : MonoBehaviour, IFixedUpdatable
    {
        [SerializeField] private int _id;
        public int ID => _id;
        
        protected ServiceContainer _serviceContainer;

        [SerializeField] protected CharacterMovement _characterMovement;
        [SerializeField] protected CharacterVisual _visual;
        [SerializeField] protected CharacterCollisionDetector _collisions;
        [SerializeField] protected BattleProfile _profile;
        protected Dictionary<Type, BaseCharacterState> _states = new();

        private BaseCharacterState _currentState;
        private UpdateRunner _runner;


        protected void BaseInit(int currentID,UpdateRunner runner, UIMediator uiMediator)
        {
            _id = currentID;
            _runner = runner;
            _serviceContainer = new ServiceContainer(this, _characterMovement, _visual, _collisions, uiMediator,
                new BattleActions(this), _profile);
            _visual.Init();
            _characterMovement.Init(_serviceContainer);
            _runner.Subscribe(this);
        }

        public void Spawn() => InitStates();

        protected virtual void InitStates() => _states.Clear();

        public void EnterState<T>() where T : BaseCharacterState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        public void FixedExecute()
        {
            _currentState?.ExecuteState(_runner.ChattersTimeScale * Time.fixedDeltaTime);
        }

        public void SubscribeOnDeath(Action<BaseMediator> despawnCallback)
        {
            _serviceContainer.Actions.OnDeathAnimationEnd += despawnCallback;
        }


        public struct ServiceContainer
        {
            public UIMediator UIMediator { get; }
            public BaseMediator BaseMediator { get; }
            public CharacterMovement CharacterMovement { get; }
            public CharacterVisual Visual { get; }
            public CharacterCollisionDetector Collisions { get; }
            public BattleActions Actions { get; }
            public BattleProfile Profile { get; }

            public ServiceContainer(BaseMediator baseMediator,
                CharacterMovement characterMovement,
                CharacterVisual visual,
                CharacterCollisionDetector collisions,
                UIMediator uiMediator,
                BattleActions actions,
                BattleProfile profile)
            {
                UIMediator = uiMediator;
                Actions = actions;
                Profile = profile;
                BaseMediator = baseMediator;
                CharacterMovement = characterMovement;
                Visual = visual;
                Collisions = collisions;
            }
        }
    }
}