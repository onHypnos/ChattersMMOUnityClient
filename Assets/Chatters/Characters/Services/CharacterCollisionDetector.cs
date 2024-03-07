using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chatters.Characters.Services
{
    public class CharacterCollisionDetector : MonoBehaviour
    {
        public List<HitBox> HitBoxes = new();

        public void Init()
        {
            GetComponentsInChildren<HitBox>();
        }

#if  UNITY_EDITOR
        public void OnValidate()
        {
            InitHitBoxes();
        }
#endif
        private void InitHitBoxes()
        {
            HitBoxes = GetComponentsInChildren<HitBox>().ToList();
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.collider)
            {
                Debug.Log($"Collide with {other.gameObject}");
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Collide with {other.gameObject}");
        }
    }
}