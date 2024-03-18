﻿using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Chatters.Characters.Services
{
    public class ChatterVisual : CharacterVisual
    {
        private CharacterBuilder _builder;
        private SpriteLibraryAsset _currentAsset;
        [SerializeField] private SpriteLibrary _library;

        public void SetupBuilder(CharacterBuilder ctxCharacterBuilder)
        {
            _builder = ctxCharacterBuilder;
        }
        
        public void SetupChatterVisual(Chatters.Services.SaveLoad.CharacterVisual playerSaveSavedVisual)
        {
            Destroy(_currentAsset);
            _builder.SpriteLibrary = _library;
            _builder.Head = playerSaveSavedVisual.Head;
            _builder.Ears = playerSaveSavedVisual.Ears;
            _builder.Eyes = playerSaveSavedVisual.Eyes;
            _builder.Body = playerSaveSavedVisual.Body; 
            _builder.Hair = playerSaveSavedVisual.Hair;
            _builder.Armor = playerSaveSavedVisual.Armor;
            _builder.Helmet = playerSaveSavedVisual.Helmet;
            _builder.Weapon = playerSaveSavedVisual.Weapon;
            _builder.Shield = playerSaveSavedVisual.Shield;
            _builder.Cape = playerSaveSavedVisual.Cape;
            _builder.Back = playerSaveSavedVisual.Back;
            _builder.Mask = playerSaveSavedVisual.Mask;
            _builder.Horns = playerSaveSavedVisual.Horns;
            _currentAsset = _builder.Rebuild();
        }
        
        
    }
}