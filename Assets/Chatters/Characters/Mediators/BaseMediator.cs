using System;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Chatters.Characters.BattleProfiles;
using Chatters.Characters.CharacterStates;
using Chatters.Characters.Services;
using Chatters.Interfaces;
using Chatters.Services.UI;
using Chatters.Services.Updater;
using Chatters.Services.Updater.Interfaces;
using UniRx;
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
        [SerializeField] protected Profile _profile;
        [SerializeField] protected ITargetProvider _targetProvider;
        protected Dictionary<Type, BaseCharacterState> _states = new();

        private BaseCharacterState _currentState;
        private UpdateRunner _runner;


        protected void BaseInit(int currentID, UpdateRunner runner, UIMediator uiMediator, ITargetProvider provider)
        {
            _id = currentID;
            _runner = runner;
            _targetProvider = provider;
            _serviceContainer = new ServiceContainer(this, _characterMovement, _visual, _collisions, uiMediator,
                new BattleActions(this), _profile);
            _visual.Init(_serviceContainer);
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
        
        public BaseMediator GetNextTarget()
        {
            return _targetProvider.GetTarget();
        }

        public ReactiveCommand OnDestroyMediator = new();
        public void Dispose()
        {
            _runner.Unsubscribe(this);
            OnDestroyMediator.Execute();
            Destroy(gameObject);
        }
        
        public struct ServiceContainer
        {
            public UIMediator UIMediator { get; }
            public BaseMediator BaseMediator { get; }
            public CharacterMovement BaseCharacterMovement { get; }
            public CharacterVisual Visual { get; }
            public CharacterCollisionDetector Collisions { get; }
            public BattleActions Actions { get; }
            public Profile Profile { get; }

            public ServiceContainer(BaseMediator baseMediator,
                CharacterMovement baseCharacterMovement,
                CharacterVisual visual,
                CharacterCollisionDetector collisions,
                UIMediator uiMediator,
                BattleActions actions,
                Profile profile)
            {
                UIMediator = uiMediator;
                Actions = actions;
                Profile = profile;
                BaseMediator = baseMediator;
                BaseCharacterMovement = baseCharacterMovement;
                Visual = visual;
                Collisions = collisions;
            }
        }
    }
}