using Music;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private MusicSettings musicSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(musicSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<AudioManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GlobalLifecycleManager>().AsSingle().NonLazy();
        }
    }
}