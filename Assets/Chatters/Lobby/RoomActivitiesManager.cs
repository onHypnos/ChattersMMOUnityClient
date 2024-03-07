using System;
using System.Collections.Generic;
using System.Linq;
using Chatters.Characters.Fabrics;
using Chatters.Characters.Mediators;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatters.Lobby
{
    public class RoomActivitiesManager : MonoBehaviour
    {
        [Header("Activities")]
        [SerializeField]private List<RoomActivity> _allActivities;
        [SerializeField]private List<RoomActivity> _enabledActivities;
        [SerializeField]private List<Transform> _defaultSpawningPoints;
        

        public void Init(EnemyFabric fabric)
        {
            foreach (var activity in _allActivities)
            {
                activity.Init(fabric);
            }
            
            EnableActivity(_allActivities[0]);
        }

        public void EnableActivity(RoomActivity activity)
        {
            activity.StartActivity(_defaultSpawningPoints);
        }

        public void DisableActivity(RoomActivity activity)
        {
            activity.Disable();
        }
    }
}