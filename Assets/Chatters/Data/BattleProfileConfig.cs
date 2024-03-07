using Chatters.Characters.Mediators;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Data
{
    [CreateAssetMenu(menuName = "NPCEnemy/CreateBattleProfile", fileName = "BattleConfig", order = 0)]
    public class BattleProfileConfig : ScriptableObject
    {
        public string Name;

        [Header("ProfileSettings")]
        [SerializeField] public float BaseHealth;
        [SerializeField] public float HealthIncreaseWithLevel;
        [SerializeField] public float BaseDamage;
        [SerializeField] public float DamageIncreaseWithLevel;
        
        
    }
}