using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chatters.Characters.Services
{
    public class CharacterAnimator : CharacterService
    {
        public Animator Animator;
        private CharacterVisual _visual;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Ready = Animator.StringToHash("Ready");
        private static readonly int Walking = Animator.StringToHash("Walking");
        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int Crawling = Animator.StringToHash("Crawling");
        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Climbing = Animator.StringToHash("Climbing");
        private static readonly int Blocking = Animator.StringToHash("Blocking");
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Landed = Animator.StringToHash("Landed");
        private static readonly int Down = Animator.StringToHash("GetDown");
        private static readonly int Up = Animator.StringToHash("GetUp");

        public List<int> AnimatorParams;
        public void Init(CharacterVisual visual)
        {
            _visual = visual;
            AnimatorParams = new();
            foreach (var param in Animator.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Bool)
                {
                    AnimatorParams.Add(param.nameHash);
                }
            }
        }

        public CharacterAnimator SetState(AnimationState state)
        {
            foreach (var variable in AnimatorParams)
            {
                Animator.SetBool(variable, false);
            }

            switch (state)
            {
                case AnimationState.Idle:
                    Animator.SetBool(Idle, true);
                    break;
                case AnimationState.Ready:
                    Animator.SetBool(Ready, true);
                    break;
                case AnimationState.Walking:
                    Animator.SetBool(Walking, true);
                    break;
                case AnimationState.Running:
                    Animator.SetBool(Running, true);
                    break;
                case AnimationState.Crawling:
                    Animator.SetBool(Crawling, true);
                    break;
                case AnimationState.Jumping:
                    Animator.SetBool(Jumping, true);
                    break;
                case AnimationState.Climbing:
                    Animator.SetBool(Climbing, true);
                    break;
                case AnimationState.Blocking:
                    Animator.SetBool(Blocking, true);
                    break;
                case AnimationState.Dead:
                    Animator.SetBool(Dead, true);
                    break;
                default: throw new NotSupportedException();
            }

            return this;
        }

        public CharacterAnimator SetJumping()
        {
            SetState(AnimationState.Jumping);
            return this;
        }

        private CharacterAnimator GetLanded()
        {
            Animator.SetTrigger(Landed);
            SetState(AnimationState.Ready);
            _visual.PlayParticles("JumpDust");
            return this;
        }

        private CharacterAnimator GetDown()
        {
            Animator.SetTrigger(Down);
            return this;
        }

        private CharacterAnimator GetUp()
        {
            Animator.SetTrigger(Up);
            return this;
        }

        public CharacterAnimator Turn(int direction)
        {
            var scale = transform.localScale;

            scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);

            transform.localScale = scale;

            return this;
        }

        //это не мое это из плагина, клянусь
        public AnimationState GetState()
        {
            if (Animator.GetBool(Idle)) return AnimationState.Idle;
            if (Animator.GetBool(Ready)) return AnimationState.Ready;
            if (Animator.GetBool(Walking)) return AnimationState.Walking;
            if (Animator.GetBool(Running)) return AnimationState.Running;
            if (Animator.GetBool(Crawling)) return AnimationState.Crawling;
            if (Animator.GetBool(Jumping)) return AnimationState.Jumping;
            if (Animator.GetBool(Climbing)) return AnimationState.Climbing;
            if (Animator.GetBool(Blocking)) return AnimationState.Blocking;
            if (Animator.GetBool(Dead)) return AnimationState.Dead;
            return AnimationState.Ready;
        }
    }

    public enum AnimationState
    {
        Idle,
        Ready,
        Walking,
        Running,
        Jumping,
        Blocking,
        Crawling,
        Climbing,
        Dead
    }
}