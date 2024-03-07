using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;

namespace Chatters.Characters.Services
{
    public class ChatterVisual : CharacterVisual
    {
        public CharacterBuilder Builder;
        
        public void SetupChatterVisual(Chatters.Services.SaveLoad.CharacterVisual playerSaveSavedVisual)
        {
            Builder.Head = playerSaveSavedVisual.Head;
            Builder.Ears = playerSaveSavedVisual.Ears;
            Builder.Eyes = playerSaveSavedVisual.Eyes;
            Builder.Body = playerSaveSavedVisual.Body; 
            Builder.Hair = playerSaveSavedVisual.Hair;
            Builder.Armor = playerSaveSavedVisual.Armor;
            Builder.Helmet = playerSaveSavedVisual.Helmet;
            Builder.Weapon = playerSaveSavedVisual.Weapon;
            Builder.Shield = playerSaveSavedVisual.Shield;
            Builder.Cape = playerSaveSavedVisual.Cape;
            Builder.Back = playerSaveSavedVisual.Back;
            Builder.Mask = playerSaveSavedVisual.Mask;
            Builder.Horns = playerSaveSavedVisual.Horns;
            Builder.Rebuild();
        }
    }
}