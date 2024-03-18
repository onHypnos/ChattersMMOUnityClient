using System.Collections.Generic;
using UnityEngine;

namespace Chatters.Characters.BattleProfiles
{
    public class Profile : MonoBehaviour
    {
        [Header("BattleClass")]
        [SerializeField] private string _className;
        
        [Header("BaseParams")]
        [SerializeField] private float _maximumHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _attackPower;
        [SerializeField] private float _defence;

        [Header("Skills")] 
        [SerializeField] private Dictionary<string, int> _skills = new();
        
        public void Init()
        {
            
        }
    }
}