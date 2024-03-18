using System;
using System.Collections.Generic;
using Chatters.Characters.Fabrics;
using Chatters.Characters.Mediators;
using Chatters.Data;
using UnityEngine;

namespace Chatters.Lobby
{
    [Serializable]
    public class EnemySpawnerData
    {
        public List<EnemyMediator> ActiveInstances =new();
        public Queue<EnemyMediator> DisabledInstances =new();
        
        public EnemyConfig Enemy;
        public int AmountCap;
        public int LayerInitialNumber = 1000;
        public Transform Parent;

        public void Init(Transform parentHolder, EnemyFabric fabric, int spawnerInitialNumber)
        {
            LayerInitialNumber = spawnerInitialNumber;
            Parent = new GameObject().transform;
            Parent.parent = parentHolder.transform;
            Parent.name = Enemy.Name + "_Parent";
            FillEnemyPull(fabric);
        }

        private void FillEnemyPull(EnemyFabric fabric)
        {
            for (int i = 0; i < AmountCap; i++)
            {
                var npc = fabric.GetEnemyInstance(Enemy, Parent, LayerInitialNumber + i);
                
                DisabledInstances.Enqueue(npc);
                npc.gameObject.SetActive(false);
            }
        }

        public EnemyMediator SpawnEnemy(Vector3 spawnPositions)
        {
            var result = DisabledInstances.Dequeue();
            ActiveInstances.Add(result);
            result.transform.position = spawnPositions;
            result.SubscribeOnDeath(DespawnEnemy);
            result.gameObject.SetActive(true);
            return result;
        }

        public void DespawnEnemy(BaseMediator instance)
        {
            ActiveInstances.Remove(instance as EnemyMediator);
            DisabledInstances.Enqueue(instance as EnemyMediator);
        }


        public Action<EnemySpawnerData, int, int> EnemyAmountUpdate;

        public void NotifyEnemyAmount()
        {
            EnemyAmountUpdate?.Invoke(this, ActiveInstances.Count, AmountCap);
        }

        public void Dispose()
        {
            ActiveInstances.Clear();
            DisabledInstances.Clear();
        }
    }
}