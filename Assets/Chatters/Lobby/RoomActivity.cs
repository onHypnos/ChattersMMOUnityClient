using System.Collections;
using System.Collections.Generic;
using Chatters.Characters.Fabrics;
using UnityEngine;

namespace Chatters.Lobby
{
    public class RoomActivity : MonoBehaviour
    {
        public string Name;
        public List<EnemySpawnerData> Spawner = new();
        public bool UseCustomSpawningPoints = false;
        public List<Transform> CustomSpawningPoints;
        public void Init(EnemyFabric fabric)
        {
            for (var index = 0; index < Spawner.Count; index++)
            {
                var npc = Spawner[index];
                npc.Init(transform, fabric, index);
            }
        }

        public void StartActivity(List<Transform> defaultSpawningPoints)
        {
            foreach (var npc in Spawner)
            {
                while (npc.ActiveInstances.Count < npc.AmountCap)
                {
                    npc.SpawnEnemy(defaultSpawningPoints[Random.Range(0, defaultSpawningPoints.Count)].position);
                }
            }

            //StartTicks
        }


        public void Disable()
        {
            foreach (var npc in Spawner)
            {
                npc.Dispose();
            }
        }
    }
}