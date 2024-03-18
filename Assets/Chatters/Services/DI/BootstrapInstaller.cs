using Chatters.DI;
using Chatters.Services.Connections;
using Chatters.Services.SaveLoad;
using Chatters.Services.UI;
using Chatters.Services.Updater;
using UnityEngine;
using Zenject;

namespace Chatters.Services.DI
{
    public class BootstrapInstaller : MonoInstaller
    {
        [Header("Prefabs")] 
        [SerializeField] private UpdateRunner _updateRunner;
        [SerializeField] private UIMediator _uiMediator;
        [SerializeField] private ChatConnectionWrapper _chatConnectionWrapper;
        
        
        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            
            var saveLoad = BindSaveLoad();
            BindRunner();
            var ui = BindUI();
            BindPlayerManager(saveLoad);
            BindChatWrapper(ui);
        }

        private void BindChatWrapper(UIMediator ui)
        {
            Container.Bind<ChatConnectionWrapper>().FromInstance(_chatConnectionWrapper).AsSingle().NonLazy();
            _chatConnectionWrapper.Init(ui);
        }

        private ISaveLoadSystem BindSaveLoad()
        {
            var saveLoad = new SaveLoadSystem();
            Container.Bind<ISaveLoadSystem>().FromInstance(saveLoad).AsSingle().NonLazy();
            return saveLoad;
        }

        private void BindPlayerManager(ISaveLoadSystem saveLoadSystem)
        {
            var playerManager = new PlayerManager(saveLoadSystem);
            Container.Bind<PlayerManager>().FromInstance(playerManager).AsSingle().NonLazy();
        }

        private UIMediator BindUI()
        {
            var uiMediator = Instantiate(_uiMediator, transform);
            Container.Bind<UIMediator>().FromInstance(uiMediator).AsSingle().NonLazy();
            uiMediator.Init();
            return uiMediator;
        }

        private void BindRunner()
        {
            var runner = Instantiate(_updateRunner, transform);
            Container.Bind<UpdateRunner>().FromInstance(runner).AsSingle().NonLazy();
        }

    }
}
