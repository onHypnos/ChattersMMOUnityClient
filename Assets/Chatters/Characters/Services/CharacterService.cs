using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Characters.Services
{
    public abstract class CharacterService : MonoBehaviour
    {
        protected BaseMediator.ServiceContainer ServiceContainer;

        public virtual void Init(BaseMediator.ServiceContainer serviceContainer)
        {
        }
    }
}