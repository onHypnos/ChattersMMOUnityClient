using System;
using System.Collections.Generic;
using Chatters.Services.UI;
using Lexone.UnityTwitchChat;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Chatters.Services.Connections
{
    public class ChatConnectionWrapper : MonoBehaviour
    {
        [FormerlySerializedAs("_twitchIRC")] [SerializeField] private IRC _twitchExample;

        [SerializeField] private List<ChatChannel> _channels = new();
        [SerializeField] private int _channelCap = 10;
        public List<string> ConnectedChannelList = new();
        private UIMediator _ui;
        
        public Action<ChatMemberContainer> OnChatterMessage;
        private ChatMemberContainer _instance;


        public void Init(UIMediator ui)
        {
            _ui = ui;
            _ui.ChannelManagerUI.OnAddNewChannelRequest += OnAddNewChannelRequest;
            UpdateAddingChannelAvailability();
            AddNewChannel();
        }


        private void OnAddNewChannelRequest()
        {
            AddNewChannel();
        }

        private void CallDebugMember(string message)
        {
            OnChatterMessage?.Invoke(new ChatMemberContainer()
            {
                UserID = "test",
                DisplayName = "test",
                Message = message,
                ChatType = "test"
            });
        }

        public ChatChannel AddNewChannel()
        {
            ChatChannel chatChannel = new ChatChannel(this,
                Instantiate(_twitchExample, transform),
                _ui.ChannelManagerUI.GetUIForChannel(),
                ChannelMessage);
            _channels.Add(chatChannel);
            UpdateAddingChannelAvailability();
            return chatChannel;
        }


        public void RemoveChannelFromList(ChatChannel channel)
        {
            _channels.Remove(channel);
            UpdateAddingChannelAvailability();
        }


        private void ChannelMessage(Chatter obj)
        {
            _instance.UserID = obj.tags.userId;
            _instance.DisplayName = obj.tags.displayName;
            _instance.Message = obj.message;
            _instance.Subscriber = false;
            _instance.ChatType = "twitch";
            _instance.Channel = obj.channel;

            Debug.Log($"Get Message from {_instance.DisplayName}: {_instance.Message}");
            OnChatterMessage?.Invoke(_instance);
            Debug.Log($"Message from {_instance.DisplayName} end action");

        }


        private void UpdateAddingChannelAvailability()
        {
            _ui.ChannelManagerUI.UpdateAddingChannelAvailability(_channels.Count < _channelCap);
        }
    }

    public struct ChatMemberContainer
    {
        public string UserID;
        public string DisplayName;
        public bool Subscriber;
        public string Message;
        public string ChatType;
        public string Channel;
    }

    [Serializable]
    public class ChatChannel
    {
        [SerializeField] private IRC _irc;
        [SerializeField] private ChannelUI _ui;
        private ChatConnectionWrapper _chatConnectionWrapper;

        public ChatChannel(ChatConnectionWrapper chatConnectionWrapper, IRC irc, ChannelUI ui,
            Action<Chatter> onChatMessage)
        {
            _chatConnectionWrapper = chatConnectionWrapper;
            _irc = irc;
            _ui = ui;
            _irc.OnChatMessage += onChatMessage;
            _irc.OnConnectionAlert += _ui.OnConnectionAlert;
            _ui.OnConnectClicked += ChangeNameAndConnect;
            _ui.OnDisconnectClicked += Disconnect;
            _ui.OnDeleteRequest += Destroy;
        }

        private string ChangeNameAndConnect(string obj)
        {
            if (_chatConnectionWrapper.ConnectedChannelList.Contains(obj))
            {
                return "Already Connected";
            }
            if(obj!=""&&obj!="Enter channel")
                _irc.channel = obj;
            _irc.Connect();
            _chatConnectionWrapper.ConnectedChannelList.Add(_irc.channel);
            _irc.gameObject.name = obj + "IRC";
            return _irc.channel;
        }

        private void Destroy()
        {
            Disconnect();
            _chatConnectionWrapper.RemoveChannelFromList(this);
            Object.Destroy(_irc.gameObject);
            Object.Destroy(_ui.gameObject);
        }

        public void Disconnect()
        {
            _irc.Disconnect();
            _chatConnectionWrapper.ConnectedChannelList.Remove(_irc.channel);
        }
    }

    
}