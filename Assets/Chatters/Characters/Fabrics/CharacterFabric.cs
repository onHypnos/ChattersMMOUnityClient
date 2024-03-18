using Chatters.Characters.Mediators;
using Chatters.Services.DamageExecutor;
using Chatters.Services.UI;
using Chatters.Services.Updater;
using UnityEngine;

namespace Chatters.Characters.Fabrics
{
    public class CharacterFabric : MonoBehaviour
    {
        [SerializeField] private ChatterMediator _example;
        private UpdateRunner _runner;
        private DamageGlobalExecutor _damageGlobalExecutor;

        [SerializeField] private Transform _sceneSpawnPoint;
        [SerializeField] private Transform _spawnTarget;
        private UIMediator _uiMediator;

        public void Init(UpdateRunner runner, DamageGlobalExecutor damageGlobalExecutor, UIMediator mediator)
        {
            _uiMediator = mediator;
            _runner = runner;
            _damageGlobalExecutor = damageGlobalExecutor;
        }

        public ChatterMediator CreateChatterCharacter(Vector3 playerSpawnPosition)
        {
            var character = Instantiate(_example, playerSpawnPosition, Quaternion.identity);
            character.Init(new ChatterMediator.Ctx()
            {
                Runner = _runner,
                SpawningDestinationPoint = _spawnTarget?_spawnTarget.position:playerSpawnPosition,
                UiMediator = _uiMediator
            }
            );
            
            return character;
        }

        public ChatterMediator CreateChatterCharacter()
        {
            return CreateChatterCharacter(_sceneSpawnPoint.position);
        }

        public void SetSpawnPoints(Transform spawnPosition, Transform spawnTarget)
        {
            _sceneSpawnPoint = spawnPosition;
            _spawnTarget = spawnTarget;
        }
    }
}