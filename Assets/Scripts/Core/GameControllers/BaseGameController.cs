using System;
using Audio.Managers;
using UnityEngine;
using Zenject;

namespace Core.GameControllers
{
    public abstract class BaseGameController : IInitializable, IDisposable
    {
        protected readonly AudioManager audioManager;
        protected readonly GameObject loadingButtons;

        protected BaseGameController(AudioManager audioManager, GameObject loadingButtons)
        {
            this.audioManager = audioManager ?? throw new ArgumentNullException(nameof(audioManager));
            this.loadingButtons = loadingButtons ?? throw new ArgumentNullException(nameof(loadingButtons));
        }

        public virtual void Initialize()
        {
            loadingButtons.SetActive(false);
            audioManager.PlayGameMusic();
        }

        public virtual void Dispose()
        {
            audioManager.PlayMenuMusic();
        }

        protected void ShowLoadingButtons() => loadingButtons.SetActive(true);
    }
}