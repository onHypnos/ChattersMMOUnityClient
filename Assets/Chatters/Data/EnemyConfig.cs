using Chatters.Characters;
using Chatters.Characters.Mediators;
using UnityEngine;

namespace Chatters.Data
{
    [CreateAssetMenu(menuName = "NPCEnemy/CreateEnemyProfile", fileName = "BattleConfig", order = 0)]
    public class EnemyConfig : BattleProfileConfig
    {
        [SerializeField] public string Name;
        [Header("Example")]
        [SerializeField] public EnemyMediator Example;


    }
}