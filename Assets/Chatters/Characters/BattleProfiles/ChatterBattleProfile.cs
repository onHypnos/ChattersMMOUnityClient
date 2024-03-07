using System.Collections.Generic;
using UnityEngine;

namespace Chatters.Characters.BattleProfiles
{
    public class ChatterBattleProfile : BattleProfiles.BattleProfile
    {
        [SerializeField] private Dictionary<string, int> _skills = new();
        
        
        [SerializeField] private Dictionary<string, int> _resourcesInventory = new();
        
    }
}