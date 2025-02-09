using Core.GameControllers;
using Gameplay.Player;
using TouchScript.Layers;
using UI.WinInfo;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SinglePlaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Player player;
        [SerializeField] private GameObject loadingButtons;
        [SerializeField] private FullscreenLayer fullscreenLayer;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SingleGameController>().AsSingle().WithArguments(player, loadingButtons, fullscreenLayer).NonLazy();
            Container.Bind<SingleWinInfo>().FromComponentInHierarchy().AsSingle();
        }
    }
}