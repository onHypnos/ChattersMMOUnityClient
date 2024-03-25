using Chatters.Characters;
using Chatters.Characters.Fabrics;
using Chatters.DI;
using Chatters.Lobby;
using Chatters.Services.Connections;
using Chatters.Services.SaveLoad;
using Chatters.Services.Updater;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Services.DI
{
    public class SceneRunner : MonoBehaviour
    {
        private CharacterFabric _characterFabric;
        private PlayerManager _playerManager;
        private ChatConnectionWrapper _source;
        private ISaveLoadSystem _saveLoad;

        [SerializeField] private LobbyRoom _lobbyRoom;

        public void Init(PlayerManager playerManager, CharacterFabric characterFabric, EnemyFabric enemyFabric, ChatConnectionWrapper source,
            ISaveLoadSystem saveLoad, UpdateRunner runner)
        {
            _saveLoad = saveLoad;
            _source = source;
            _playerManager = playerManager;
            _characterFabric = characterFabric;
            
            _lobbyRoom.Init(characterFabric, enemyFabric,source, saveLoad, runner);
            _characterFabric.BindTargetProvider(_lobbyRoom);
        }

        
        

        
        
    }
}