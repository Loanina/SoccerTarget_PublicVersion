using Game.Ball;
using Target;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private TargetConfig targetConfig;
        [FormerlySerializedAs("puckSettings")] [SerializeField] private BallSettings ballSettings;
        [SerializeField] private Player.Player player;
        [FormerlySerializedAs("puckTouchHandler")] [SerializeField] private BallTouchHandler ballTouchHandler;
        [SerializeField] private Transform parentForObjects;
    
        public override void InstallBindings()
        {
            Container.Bind<Player.Player>().FromInstance(player).AsSingle();
            Container.BindInterfacesAndSelfTo<TargetController>().AsSingle().WithArguments(targetConfig, parentForObjects);
            Container.BindInterfacesAndSelfTo<BallSpawner>().AsSingle().WithArguments(parentForObjects);
            Container.Bind<BallThrower>().AsSingle().WithArguments(ballSettings);
            Container.Bind<BallTouchHandler>().FromInstance(ballTouchHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<BallPhysics>().AsSingle().WithArguments(ballSettings);
            Container.BindInterfacesAndSelfTo<BallDestroyer>().AsSingle().WithArguments(ballSettings);
            Container.BindInterfacesAndSelfTo<BallFactory>().AsSingle().WithArguments(ballSettings);
        }
    }
}