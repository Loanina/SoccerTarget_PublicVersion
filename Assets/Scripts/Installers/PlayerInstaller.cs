using System.Collections.Generic;
using Gameplay.Ball;
using Gameplay.Crowd;
using Gameplay.Player;
using Gameplay.Target;
using UI.Controllers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player player;
        [SerializeField] private TargetConfig targetConfig;
        [SerializeField] private BallSettings ballSettings;
        [SerializeField] private PlayerUIController playerUIController;
        [SerializeField] private BallTouchHandler ballTouchHandler;
        [SerializeField] private Transform parentForObjects;
        [SerializeField] private List<Animator> crowdAnimators;
    
        public override void InstallBindings()
        {
            Container.Bind<PlayerUIController>().FromInstance(playerUIController).AsSingle();
            Container.Bind<Player>().FromInstance(player).AsSingle();
            Container.BindInterfacesAndSelfTo<TargetController>().AsSingle();
            Container.Bind<TargetSpawner>().AsSingle().WithArguments(targetConfig, parentForObjects);
            Container.Bind<TargetDestroyer>().AsSingle().WithArguments(targetConfig, parentForObjects);
            Container.BindInterfacesAndSelfTo<BallSpawner>().AsSingle().WithArguments(parentForObjects);
            Container.Bind<BallThrower>().AsSingle().WithArguments(ballSettings);
            Container.Bind<BallTouchHandler>().FromInstance(ballTouchHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<BallPhysics>().AsSingle().WithArguments(ballSettings);
            Container.BindInterfacesAndSelfTo<BallDestroyer>().AsSingle().WithArguments(ballSettings);
            Container.BindInterfacesAndSelfTo<BallFactory>().AsSingle().WithArguments(ballSettings);
            Container.BindInterfacesAndSelfTo<CrowdAnimationController>().AsSingle().WithArguments(crowdAnimators);
        }
    }
}