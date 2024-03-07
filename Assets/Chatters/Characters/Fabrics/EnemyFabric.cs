using Chatters.Characters.Mediators;
using Chatters.Data;
using Chatters.DI;
using Chatters.Services.DamageExecutor;
using Chatters.Services.DI;
using Chatters.Services.EntityID;
using Chatters.Services.UI;
using Chatters.Services.Updater;
using UnityEngine;
using Zenject;

namespace Chatters.Characters.Fabrics
{
    public class EnemyFabric : MonoBehaviour
    {
        private UIMediator _uiMediator;
        private UpdateRunner _runner;
        private DamageGlobalExecutor _damageGlobalExecutor;
        private EntityIdNumerator _entityNumerator;

        public void Init(UpdateRunner runner, DamageGlobalExecutor damageGlobalExecutor, UIMediator mediator,
            EntityIdNumerator entityNumerator)
        {
            _entityNumerator = entityNumerator;
            _uiMediator = mediator;
            _runner = runner;
            _damageGlobalExecutor = damageGlobalExecutor;
        }

        public EnemyMediator GetEnemyInstance(EnemyProfileConfig config, Transform parent, int layerNumber)
        {
            var enemy = Instantiate(config.Example, parent);
            enemy.Init(new EnemyMediator.Ctx
            {
                ID = _entityNumerator.GetNewID(),
                Runner = _runner,
                UiMediator = _uiMediator,
                SpriteLayerOrder = layerNumber
            });
            enemy.Spawn();
            _damageGlobalExecutor.RegisterEnemy(enemy);
            return enemy;
        }
        
        
    }
}