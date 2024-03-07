using UnityEngine;

namespace Chatters.Characters.Services
{
    public class BattleProfile : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float _maximumHealth;
        [SerializeField] private float _currentHealth;
        
        [SerializeField] private float _baseDamage;


        public void Init()
        {
            
        }
    }
}