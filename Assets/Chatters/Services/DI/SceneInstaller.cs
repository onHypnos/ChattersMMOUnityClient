using Chatters.Characters;
using Chatters.Characters.Fabrics;
using Chatters.DI;
using Chatters.Lobby;
using Chatters.Services.Connections;
using Chatters.Services.DamageExecutor;
using Chatters.Services.EntityID;
using Chatters.Services.SaveLoad;
using Chatters.Services.UI;
using Chatters.Services.Updater;
using UnityEngine;
using Zenject;

namespace Chatters.Services.DI
{
    public class SceneInstaller : MonoInstaller
    {
        [Header("Instances")][SerializeField]
        private SceneRunner _sceneRunner;

        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _spawnTarget;
        
        [Space][Header("Examples")]
        [SerializeField] private CharacterFabric _characterFabricInstance;
        private DamageGlobalExecutor _damageGlobalExecutor;
        [SerializeField] private EnemyFabric _enemyFabricInstance;
        
        
        [Header("Dependencies")]
        private PlayerManager _playerManager;
        private ISaveLoadSystem _saveLoad;
        private UpdateRunner _updRunner;
        private UIMediator _uiMediator;
        private ChatConnectionWrapper _wrapper;

        [Inject]
        public void Construct(UpdateRunner updRunner,
            PlayerManager playerManager, 
            ISaveLoadSystem saveLoad, 
            UIMediator uiMediator, 
            ChatConnectionWrapper wrapper)
        {
            _wrapper = wrapper;
            _uiMediator = uiMediator;
            _updRunner = updRunner;
            _saveLoad = saveLoad;
            _playerManager = playerManager;
        }
        
        
        public override void InstallBindings()
        {
            var damageGlobalExecutor = BindGlobalDamageExecutor();
            var fabric = BindCharacterFabric(_updRunner, damageGlobalExecutor, _spawnPosition, _spawnTarget,
                _uiMediator);
            var entityNumerator = new EntityIdNumerator();
            Container.Bind<EntityIdNumerator>().FromInstance(entityNumerator).AsSingle().NonLazy();
            var enemyFabric = BindEnemyFabric(damageGlobalExecutor,entityNumerator, _uiMediator, _updRunner);
            BindSceneRunner(fabric,enemyFabric);
        }

        private void BindSceneRunner(CharacterFabric fabric, EnemyFabric enemyFabric)
        {
            Container.Bind<SceneRunner>().FromInstance(_sceneRunner).AsSingle().NonLazy();
            _sceneRunner.Init(_playerManager, fabric, enemyFabric, _wrapper, _saveLoad);
        }
        
        
        private DamageGlobalExecutor BindGlobalDamageExecutor()
        {
            var damageGlobalExecutor = new DamageGlobalExecutor();
            Container.Bind<DamageGlobalExecutor>().FromInstance(damageGlobalExecutor).AsSingle().NonLazy();
            return damageGlobalExecutor;
        }

        private CharacterFabric BindCharacterFabric(UpdateRunner updateRunner,
            DamageGlobalExecutor damageGlobalExecutor, 
            Transform spawnPosition,
            Transform spawnTarget,
            UIMediator uiMediator)
        {
            var fabric = Instantiate(_characterFabricInstance, transform);
            fabric.SetSpawnPoints(spawnPosition, spawnTarget);
            Container.Bind<CharacterFabric>().FromInstance(fabric).AsSingle().NonLazy();
            fabric.Init(updateRunner, damageGlobalExecutor, uiMediator);
            return fabric;
        }

        private EnemyFabric BindEnemyFabric(DamageGlobalExecutor executor, EntityIdNumerator entityNumerator,
            UIMediator uiMediator, UpdateRunner runner)
        {
            var enemyFabric = Instantiate(_enemyFabricInstance, transform);
            Container.Bind<EnemyFabric>().FromInstance(enemyFabric).AsSingle().NonLazy();
            enemyFabric.Init(runner,executor,uiMediator, entityNumerator);
            return enemyFabric;
        }
        
       
    }
}