using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chatters.Services.SaveLoad
{
    /*
     * SaveLoad временная заглушка
     */
    public interface ISaveLoadSystem
    {
        bool LoadChatter(string id, out ChatterData data);
    }

    public class SaveLoadSystem : ISaveLoadSystem
    {
        [Serializable]
        public class ChatterList
        {
            public List<string> ChattersIDs = new();
        }
        
        public ChatterList SavedChatters;

        public void Init()
        {
            LoadChatterList();
        }

        private void LoadChatterList()
        {
            if (PlayerPrefs.HasKey(SaveLoadConsts.CHATTER_LIST_KEY))
            {
                
            }
            else
            {
                SavedChatters = new() { ChattersIDs = new() };
            }
        }


        public ChatterData CreateNewSave(string id = "player")
        {
            ChatterData data = new ChatterData();
            data.ID = id;
            var jsonData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(id,jsonData);
            Debug.Log("New save created data saved!");
            return data;
        }
        
        public void SaveChatter(string id, ChatterData data)
        {
            var jsonData =JsonUtility.ToJson(data);
            PlayerPrefs.SetString(id,jsonData);
            Debug.Log("Chatter data saved");
        }
        
        public bool LoadChatter(string id, out ChatterData data)
        {
            ChatterData jsonData;
            if (PlayerPrefs.HasKey(id))
            {
                jsonData=JsonUtility.FromJson<ChatterData>(PlayerPrefs.GetString(id));
            }
            else
            {
                jsonData = new ChatterData();
            }

            data = jsonData;
            
            return true;
        }

        private void ResetData()
        {
            PlayerPrefs.DeleteAll();
        }

        private bool Validate(ChatterData data)
        {
            return true;
        }
    }
}