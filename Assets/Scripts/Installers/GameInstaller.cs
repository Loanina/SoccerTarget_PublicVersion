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
            Container.Bind<AudioManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<MusicPlayer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SoundEffectsPlayer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GlobalLifecycleManager>().AsSingle().NonLazy();
        }
    }
}