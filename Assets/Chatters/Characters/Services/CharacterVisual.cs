using System;
using System.Collections.Generic;
using System.Linq;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Chatters.Characters.Mediators;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;

namespace Chatters.Characters.Services
{
    public class CharacterVisual : MonoBehaviour
    {
        public struct Ctx
        {
            
        }

        private Ctx _ctx;
        
        public CharacterAnimator CharacterAnimator;
        public SpriteLibrary Library;
        public SpriteRenderer Renderer;
        public List<ParticlePair> Particles;
        protected BaseMediator.ServiceContainer ServiceContainer;

        public virtual void Init(BaseMediator.ServiceContainer serviceContainer)
        {
            ServiceContainer = serviceContainer;
            CharacterAnimator.Init(this);
        }

        public void SetLayerOrder(int layerOrder)
        {
            Renderer.sortingOrder = layerOrder;
        }

        public void PlayParticles(string particleName)
        {
            Particles.FirstOrDefault(x=> x.Id == particleName).Particle.Play(true);
        }
        
        public void SetState(AnimationState idle)
        {
            CharacterAnimator.SetState(idle);
        }
        
        [Serializable]
        public struct ParticlePair
        {
            [SerializeField] public string Id;
            [SerializeField] public ParticleSystem Particle;
        }
    }
}