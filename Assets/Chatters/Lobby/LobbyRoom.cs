using System.Collections.Generic;
using Chatters.Characters;
using Chatters.Characters.Fabrics;
using Chatters.Services.Connections;
using Chatters.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Chatters.Lobby
{
    public class LobbyRoom : MonoBehaviour
    {
        [SerializeField] private RoomActivitiesManager _activityManager;
        private CharacterFabric _fabric;
        private ChatConnectionWrapper _source;
        private ISaveLoadSystem _saveLoad;

        public bool TrackMessages = true;
        public bool Chatters = true;
        public bool Subscribers = true;
        public bool Vip = true;
        public int LobbyMaxSize { get; } = 100;

        public Dictionary<string, ChatMember> LobbyList;

        public void Init(CharacterFabric fabric, EnemyFabric enemyFabric, ChatConnectionWrapper source,
            ISaveLoadSystem saveLoad)
        {
            _saveLoad = saveLoad;
            _fabric = fabric;
            _source = source;

            LobbyList = new();
            _source.OnChatterMessage += ChatterMessage;
            
            _activityManager.Init(enemyFabric);
        }

        private void ChatterMessage(ChatMemberContainer obj)
        {
            if (!TrackMessages) return;
            //todo проверка на присутствие пользователя в черном листе
            var id = obj.ChatType + obj.UserID;
            
            if (LobbyList.ContainsKey(id))
            {
                LobbyList[id].UpdateWithMessage(ref obj);
            }
            else
            {
                CreateChatMember(ref obj).UpdateWithMessage(ref obj);
            }
        }

        private ChatMember CreateChatMember(ref ChatMemberContainer container)
        {
            var member = new ChatMember();
            var id = container.ChatType + container.UserID;
            LobbyList.Add(id, member);
            _saveLoad.LoadChatter(id, out ChatterData playerSave);
            var mediator = _fabric.CreateChatterCharacter();
            member.Init(mediator, playerSave, container.DisplayName);
            mediator.Spawn();
            return member;
        }

    }
}