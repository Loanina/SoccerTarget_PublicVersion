using System.Collections.Generic;
using Game.Ball;
using Game.Crowd;
using Game.Target;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private TargetConfig targetConfig;
        [SerializeField] private BallSettings ballSettings;
        [SerializeField] private Player.Player player;
        [SerializeField] private BallTouchHandler ballTouchHandler;
        [SerializeField] private Transform parentForObjects;
        [SerializeField] private List<Animator> crowdAnimators;
    
        public override void InstallBindings()
        {
            Container.Bind<Player.Player>().FromInstance(player).AsSingle();
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