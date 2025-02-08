using TouchScript.Layers;
using UI;
using UI.WinInfo;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MultiPlaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Player.Player firstPlayer;
        [SerializeField] private Player.Player secondPlayer;
        [SerializeField] private GameObject loadingButtons;
        [SerializeField] private FullscreenLayer firstFullscreenLayer;
        [SerializeField] private FullscreenLayer secondFullscreenLayer;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MultiGameController>().AsSingle().WithArguments(firstPlayer, secondPlayer, loadingButtons, firstFullscreenLayer, secondFullscreenLayer);
            Container.Bind<MultiWinInfo>().FromComponentInHierarchy().AsSingle();
        }
    }
}