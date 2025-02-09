using Audio.Settings;
using UnityEngine;
using Zenject;

namespace Audio.Managers
{
    public class SoundEffectsPlayer : IInitializable
    {
        private readonly MusicSettings musicSettings;
        private AudioSource effectsSource;
        private AudioSource ballSource;
        private AudioSource targetSource;
        private AudioSource crowdSource;

        [Inject]
        public SoundEffectsPlayer(MusicSettings musicSettings)
        {
            this.musicSettings = musicSettings;
        }

        public void Initialize()
        {
            var audioObject = new GameObject("AudioManager");
            Object.DontDestroyOnLoad(audioObject);
            
            effectsSource = audioObject.AddComponent<AudioSource>();
            ballSource = audioObject.AddComponent<AudioSource>();
            targetSource = audioObject.AddComponent<AudioSource>();
            crowdSource = audioObject.AddComponent<AudioSource>();
        }

        public void PlayCrowd()
        {
            PlaySimple(musicSettings.crowd, musicSettings.crowdVolume, crowdSource);
        }
        
        public void PlayHighBeep()
        {
            PlaySimple(musicSettings.highBeep, musicSettings.highBeepVolume, effectsSource);
        }
        
        public void PlayLowBeep()
        {
            PlaySimple(musicSettings.lowBeep, musicSettings.lowBeepVolume, effectsSource);
        }
        
        public void PlayBallThrow()
        {
            ballSource.pitch = Random.Range(musicSettings.minPitch, musicSettings.maxPitch);
            PlaySimple(musicSettings.ballThrow, musicSettings.ballThrowVolume, ballSource);
        }
        
        public void PlayTargetHit()
        {
            targetSource.pitch = Random.Range(musicSettings.minTargetPitch, musicSettings.maxTargetPitch);
            PlaySimple(musicSettings.targetHit, musicSettings.targetHitVolume, targetSource);
        }
        
        public void PlayWinCrowd()
        {
            crowdSource.clip = musicSettings.winCrowd;
            crowdSource.loop = true;
            crowdSource.volume = musicSettings.winCrowdVolume;
            crowdSource.Play();
        }

        private void PlaySimple(AudioClip clip, float volume, AudioSource source)
        {
            if (clip == null) return;
            source.volume = volume;
            source.PlayOneShot(clip);
        }

        public void StopAllSounds()
        {
            effectsSource.Stop();
            crowdSource.Stop();
            ballSource.Stop();
            targetSource.Stop();
        }
    }
}
