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
        public Time ExitLobbyTime = new Time();
        
        [FormerlySerializedAs("BaseMediator")] public ChatterMediator Mediator;


        public void Init(ChatterMediator mediator, ChatterData chatterData, string containerDisplayName)
        {
            Mediator = mediator;
            Mediator.gameObject.name = containerDisplayName+"Chatter";
            Mediator.LoadSetup(chatterData, containerDisplayName);
        }

        public ChatMember UpdateWithMessage(ref ChatMemberContainer chatMemberContainer)
        {
            Mediator.ShowMessage(chatMemberContainer.Message);
            return this;
        }
    }
}