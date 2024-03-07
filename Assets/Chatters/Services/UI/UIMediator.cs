using UnityEngine;

namespace Chatters.Services.UI
{
    public class UIMediator : MonoBehaviour
    {
        [SerializeField] private ChannelManagerUI _channelManager;
        public ChannelManagerUI ChannelManagerUI => _channelManager;


        public void Init()
        {
            ChannelManagerUI.Init();
        }
    }
}