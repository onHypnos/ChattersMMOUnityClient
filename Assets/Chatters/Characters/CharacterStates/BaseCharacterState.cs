using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Chatters.Characters.Behaviours;
using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Characters.CharacterStates
{
    public abstract class BaseCharacterState
    {

        private List<BaseBehaviour> StateBehaviours = new();
        protected BaseBehaviour CurrentBehaviour;
        protected int CurrentTargetIndex = 0;
        protected int TaskRepeats = 0;

        public BaseCharacterState()
        {
        }

        public Action OnAllBehavioursEnd;

        protected BaseBehaviour AddBehaviour(BaseBehaviour moveToPosition)
        {
            StateBehaviours.Add(moveToPosition);
            return moveToPosition;
        }

        public virtual void Enter()
        {
            CurrentBehaviour = StateBehaviours[CurrentTargetIndex];
            CurrentBehaviour.StartBehaviour();
        }

        public virtual void Exit()
        {
        }

        public virtual void ExecuteState(float deltaTime)
        {
            if (!CurrentBehaviour.CheckComplete())
            {
                CurrentBehaviour.Execute(deltaTime);
            }
            else
            {
                NextBehaviour();
            }
        }

        protected void NextBehaviour()
        {
            CurrentTargetIndex++;
            if (StateBehaviours.Count() <= CurrentTargetIndex)
            {
                CurrentTargetIndex = 0;
                if (TaskRepeats == 0)
                {
                    OnAllBehavioursEnd?.Invoke();
                    OnAllBehavioursEnd = null;
                }
                else
                {
                    TaskRepeats = (TaskRepeats < 0) ? -1 : TaskRepeats--;
                }
            }

            CurrentBehaviour.EndBehaviour();
            CurrentBehaviour = StateBehaviours[CurrentTargetIndex];
            CurrentBehaviour.StartBehaviour();
        }
    }
}