using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Chatters.Services.UI
{
    public class ChannelManagerUI : MonoBehaviour
    {
        [SerializeField] private ChannelUI _example;
        [SerializeField] private List<ChannelUI> _channels;
        [SerializeField] private Transform _layoutParent;
        [SerializeField] private Button _addChannelButton;

        public Action OnAddNewChannelRequest; 
        public void Init()
        {
            _addChannelButton.onClick.AddListener(AddChannelRequest);
        }

        public void AddChannelRequest()
        {
            OnAddNewChannelRequest?.Invoke();
        }

        public void UpdateAddingChannelAvailability(bool value)
        {
            _addChannelButton.gameObject.SetActive(value);
        }
        
        public ChannelUI GetUIForChannel()
        {
            return AddNewChannelUI();
        }

        private ChannelUI AddNewChannelUI()
        {
            var channelUI = Instantiate(_example, _layoutParent);
            _channels.Add(channelUI);
            channelUI.OnDestroyed += () => { _channels.Remove(channelUI); };
            channelUI.Init();
            return channelUI;
        }
    }
}