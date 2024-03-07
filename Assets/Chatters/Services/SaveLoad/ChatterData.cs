using System;

namespace Chatters.Services.SaveLoad
{
    [Serializable]
    public class ChatterData
    {
        public ChatterData(string id = "player")
        {
            ID = id;
            SavedVisual.Head = "Human";
            SavedVisual.Ears = "Human";
            SavedVisual.Eyes = "Human";
            SavedVisual.Body = "Human";
            SavedVisual.Hair="";
            SavedVisual.Armor="";
            SavedVisual.Helmet="";
            SavedVisual.Weapon="";
            SavedVisual.Shield="";
            SavedVisual.Cape="";
            SavedVisual.Back="";
            SavedVisual.Mask="";
            SavedVisual.Horns="";
        }
        
        public string ID;
        public CharacterVisual SavedVisual;
    }

    [Serializable]
    public struct CharacterVisual
    {
        public string Head;
        public string Ears;
        public string Eyes;
        public string Body;
        public string Hair;
        public string Armor;
        public string Helmet;
        public string Weapon;
        public string Shield;
        public string Cape;
        public string Back;
        public string Mask;
        public string Horns;
    }
}