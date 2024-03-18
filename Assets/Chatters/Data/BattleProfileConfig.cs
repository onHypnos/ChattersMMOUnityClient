using Chatters.Characters.Mediators;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Data
{
    [CreateAssetMenu(menuName = "NPCEnemy/CreateBattleProfile", fileName = "BattleConfig", order = 0)]
    public class BattleProfileConfig : ScriptableObject
    {
        [Header("ProfileSettings")]
        [SerializeField] private float _baseHealth;
        [SerializeField] private float _healthIncreaseWithLevel;
        [SerializeField] private float _baseDamage;
        [SerializeField] private float _baseArmour;
        [SerializeField] private float _damageIncreaseWithLevel;
        
    }
}