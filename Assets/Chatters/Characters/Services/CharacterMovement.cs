using Chatters.Characters.Mediators;
using Chatters.Services.Updater;
using UnityEngine;

namespace Chatters.Characters.Services
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : CharacterService
    {
        [SerializeField] private CharacterController _characterController;
        private Transform _pivot;
        public float MaximumSpeed = 5f;
        public float Speed = 5f;


        public void Move(Vector3 direction, float deltaTime)
        {
            _characterController.Move(direction*deltaTime);
            ServiceContainer.Visual.CharacterAnimator
                .Turn((int)direction.normalized.x)
                .SetState(AnimationState.Running);
        }
        
        public void MoveTo(Vector3 target, float deltaTime)
        {
            var direction = (target - _pivot.position).normalized;
            Move(direction, deltaTime);
        }

        public void WarpTo(Vector3 newPosition)
        {
            
        }

        public override void Init(BaseMediator.ServiceContainer serviceContainer)
        {
            base.Init(serviceContainer);
            ServiceContainer = serviceContainer;
            _pivot = ServiceContainer.BaseMediator.transform;
        }
    }
}