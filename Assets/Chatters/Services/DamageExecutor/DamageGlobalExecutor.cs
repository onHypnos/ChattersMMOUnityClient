using System.Collections.Generic;
using Chatters.Characters.Mediators;

namespace Chatters.Services.DamageExecutor
{
    public class DamageGlobalExecutor
    {
        private Dictionary<int, EnemyMediator> _enemyList = new();
        
        public void RegisterEnemy(EnemyMediator enemy)
        {
            _enemyList.Add(enemy.ID, enemy);
        }
        
    }
}