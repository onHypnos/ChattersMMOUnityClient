using System;
using Chatters.Characters;
using Chatters.Characters.Mediators;
using Chatters.Services.Connections;
using Chatters.Services.SaveLoad;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Lobby
{
    [Serializable]
    public class ChatMember
    {
        public float ExitLobbyTime;
        public string ID;
        
        [FormerlySerializedAs("BaseMediator")] public ChatterMediator Mediator;


        public void Init(ChatterMediator mediator, ChatterData chatterData, string containerDisplayName)
        {
            Mediator = mediator;
            Mediator.gameObject.name = containerDisplayName+"Chatter";
            Mediator.LoadSetup(chatterData, containerDisplayName);
        }

        public ChatMember UpdateWithMessage(ChatMemberContainer chatMemberContainer)
        {
            Mediator.ShowMessage(chatMemberContainer.Message);
            return this;
        }

        public void DisposeMember()
        {
            Mediator.Dispose();
        }
    }
}