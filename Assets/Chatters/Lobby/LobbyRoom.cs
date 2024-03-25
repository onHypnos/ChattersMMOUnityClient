using System.Collections.Generic;
using Chatters.Characters;
using Chatters.Characters.Fabrics;
using Chatters.Characters.Mediators;
using Chatters.Interfaces;
using Chatters.Services.Connections;
using Chatters.Services.SaveLoad;
using Chatters.Services.Updater;
using Chatters.Services.Updater.Interfaces;
using UnityEngine;
using Zenject;

namespace Chatters.Lobby
{
    public class LobbyRoom : MonoBehaviour, IFixedUpdatable, ITargetProvider
    {
        [SerializeField] private RoomActivitiesManager _activityManager;
        private CharacterFabric _fabric;
        private ChatConnectionWrapper _source;
        private ISaveLoadSystem _saveLoad;

        public float ChatterLiveTime = 600;
        public bool TrackMessages = true;
        public bool Chatters = true;
        public bool Subscribers = true;
        public bool Vip = true;
        public int LobbyMaxSize { get; } = 100;

        public Dictionary<string, ChatMember> LobbyList;
        private Dictionary<int, List<string>> _timerList = new();
        private UpdateRunner _updateRunner;

        public void Init(CharacterFabric fabric, EnemyFabric enemyFabric, ChatConnectionWrapper source,
            ISaveLoadSystem saveLoad, UpdateRunner runner)
        {
            _saveLoad = saveLoad;
            _fabric = fabric;
            _source = source;
            _updateRunner = runner;
            LobbyList = new();
            _source.OnChatterMessage += ChatterMessage;
            
            _activityManager.Init(enemyFabric);
            
            _updateRunner.Subscribe(this);
        }

        private void ChatterMessage(ChatMemberContainer obj)
        {
            if (!TrackMessages) return;
            //todo проверка на присутствие пользователя в черном листе
            var id = obj.ChatType + obj.UserID;
            
            if (LobbyList.ContainsKey(id))
            {
                UpdateTimerValue(LobbyList[id].UpdateWithMessage(obj));
            }
            else
            {
                UpdateTimerValue(CreateChatMember(obj).UpdateWithMessage(obj));
            }
        }


        private void UpdateTimerValue(ChatMember member)
        {
            if (_timerList.TryGetValue((int)member.ExitLobbyTime, out var list))
            {
                list.Remove(member.ID);
            }
            var removeTime = (int)(Time.fixedTime + ChatterLiveTime);
            
            if(!_timerList.TryGetValue((int)removeTime, out var nextList))
            {
                _timerList.Add((int)removeTime, new());
                nextList = _timerList[(int)removeTime];
            }
            nextList.Add(member.ID);
            member.ExitLobbyTime = removeTime;
        }

        public void FixedExecute()
        {
            var removeTime = Time.fixedTime;
            if (!_timerList.TryGetValue((int)removeTime, out var list))
            {
                return;
            }
            
            list.ForEach((id) =>
            {
                RemoveMember(LobbyList[id]);
            });
            list = null;
            
            _timerList.Remove((int)removeTime);
        }

        private ChatMember CreateChatMember(ChatMemberContainer container)
        {
            var member = new ChatMember
            {
                ID = container.ChatType + container.UserID
            };

            LobbyList.Add(member.ID, member);
            _saveLoad.LoadChatter(member.ID, out ChatterData playerSave);
            
            var mediator = _fabric.CreateChatterCharacter();
            member.Init(mediator, playerSave, container.DisplayName);
            mediator.Spawn();
            return member;
        }

        private void RemoveMember(ChatMember member)
        {
            member.DisposeMember();
            LobbyList.Remove(member.ID);
        }

        public BaseMediator GetTarget() => _activityManager.GetTarget();
    }
}