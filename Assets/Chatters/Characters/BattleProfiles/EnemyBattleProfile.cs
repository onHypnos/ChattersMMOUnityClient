using System.Collections.Generic;
using UnityEngine;

namespace Chatters.Characters.BattleProfiles
{
    public class EnemyBattleProfile : BattleProfiles.BattleProfile
    {
        [SerializeField] private List<string> _availableDrop;
    }
}