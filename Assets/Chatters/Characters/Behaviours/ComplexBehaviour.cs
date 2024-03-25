using System;
using System.Collections.Generic;
using System.Linq;
using Chatters.Characters.Mediators;

namespace Chatters.Characters.Behaviours
{
    public class ComplexBehaviour : BaseBehaviour
    {
        private List<BaseBehaviour> StateBehaviours = new();
        protected BaseBehaviour CurrentBehaviour;
        protected int CurrentTargetIndex = 0;
        private float _taskRepeat = -1;
        public bool BehavioursEnd = false;


        public ComplexBehaviour() : base()
        {
        }
        
        public override void StartBehaviour()
        {
            base.StartBehaviour();
            BehavioursEnd = false;
            CurrentBehaviour = StateBehaviours[CurrentTargetIndex];
            CurrentBehaviour.StartBehaviour();
        }

        public ComplexBehaviour AddBehaviour(BaseBehaviour beh)
        {
            StateBehaviours.Add(beh);
            return this;
        }

        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);
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
                if (_taskRepeat == 0)
                {
                    BehavioursEnd = true;
                }
                else
                {
                    _taskRepeat = (_taskRepeat < 0) ? -1 : --_taskRepeat;
                }
            }

            CurrentBehaviour.EndBehaviour();
            CurrentBehaviour = StateBehaviours[CurrentTargetIndex];
            CurrentBehaviour.StartBehaviour();
        }

        public override bool CompleteRequirements()
        {
            return base.CompleteRequirements();
        }
    }
}