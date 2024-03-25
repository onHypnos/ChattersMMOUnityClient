using System;
using System.Collections.Generic;
using Chatters.Characters.Mediators;

namespace Chatters.Characters.Services
{
    public class BattleActions
    {
        private readonly BaseMediator _baseMediator;
        private Dictionary<string, Action> _customActions;

        public Action<BaseMediator> OnDeathAnimationEnd;

        public void DeathAnimationEnd()
        {
            OnDeathAnimationEnd?.Invoke(_baseMediator);
        }

        public BattleActions(BaseMediator baseMediator)
        {
            _baseMediator = baseMediator;
        }


        public void SubscribeCustom(string name, Action callBack, CallType callType = CallType.CallOnce)
        {
            _customActions.TryAdd(name, callBack);
            switch (callType)
            {
                case CallType.CallOnce:
                    UnsubscribeCustom(name, callBack);
                    break;
                case CallType.Persistent:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(callType), callType, null);
            }
        }

        public void UnsubscribeCustom(string name, Action callback)
        {
            if (_customActions.ContainsKey(name))
                _customActions[name] -= callback;
        }

        public void NotifyCustom(string name)
        {
            if(_customActions.ContainsKey(name))
                _customActions[name]?.Invoke();
        }

        
        
        
        
        
        public enum CallType
        {
            CallOnce,
            Persistent
        }
    }
}