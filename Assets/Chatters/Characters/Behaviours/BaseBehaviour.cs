using System;
using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Characters.Behaviours
{
    [Serializable]
    public abstract class BaseBehaviour
    {
        protected BaseMediator.ServiceContainer MediatorServiceContainer;
        public bool IsComplete { get; private set; } = false;

        public event Action OnBehaviourComplete;

        protected BaseBehaviour(BaseMediator.ServiceContainer mediatorServiceContainer)
        {
            MediatorServiceContainer = mediatorServiceContainer;
        }

        public virtual void StartBehaviour()
        {
            IsComplete = false;
        }

        public virtual void Execute(float deltaTime)
        {
        }

        public virtual bool CheckComplete()
        {
            if (CompleteRequirements())
            {
                IsComplete = true;
                OnBehaviourComplete?.Invoke();
            }

            return IsComplete;
        }

        public virtual bool CompleteRequirements()
        {
            return true;
        }

        public virtual void EndBehaviour()
        {
            
        }
    }
}