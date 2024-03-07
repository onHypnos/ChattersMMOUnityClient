using System.Collections.Generic;
using Chatters.Lobby;
using Chatters.Services.SaveLoad;

namespace Chatters.DI
{
    public class PlayerManager
    {
        private Dictionary<string, ChatMember> _gamePlayers;
        private ISaveLoadSystem _saveLoadSystem;

        public PlayerManager(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            _gamePlayers = new Dictionary<string, ChatMember>();
            
        }

        
    }
}