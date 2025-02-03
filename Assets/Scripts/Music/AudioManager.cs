using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Music
{
    public class AudioManager : IInitializable
    {
        private readonly MusicSettings musicSettings;
        private AudioSource musicSource;
        private AudioSource effectsSource;
        private AudioSource crowdSource;
        private AudioSource ballThrowAudioSource;
        private AudioSource targetAudioSource;
        private int currentBackgroundSongIndex;
        private bool isChosenGameMusic;

        [Inject]
        public AudioManager(MusicSettings musicSettings)
        {
            this.musicSettings = musicSettings;
        }

        public void Initialize()
        {
            var audioObject = new GameObject("AudioManager");
            Object.DontDestroyOnLoad(audioObject);

            musicSource = audioObject.AddComponent<AudioSource>();
            effectsSource = audioObject.AddComponent<AudioSource>();
            ballThrowAudioSource = audioObject.AddComponent<AudioSource>();
            targetAudioSource = audioObject.AddComponent<AudioSource>();
            crowdSource = audioObject.AddComponent<AudioSource>();

            PlayBackgroundMusic();
        }
        
        private void PlayBackgroundMusic()
        {
            ChangeBackgroundToMenu();
            musicSource.Play();
        }

        public void ChangeBackgroundToMenu()
        {
            isChosenGameMusic = false;
            musicSource.loop = true;
            musicSource.volume = musicSettings.menuMusicVolume;
            musicSource.clip = musicSettings.menuMusic;
            musicSource.Play();
        }

        public void ChangeBackgroundToGame()
        {
            isChosenGameMusic = true;
            musicSource.volume = musicSettings.backgroundMusicVolume;
            musicSource.loop = false;
            currentBackgroundSongIndex = 0;
            PlayNextBackgroundSong();
        }
        
        private void PlayNextBackgroundSong()
        {
            musicSource.clip = musicSettings.backgroundMusic[currentBackgroundSongIndex];
            musicSource.Play();
            TrackSongEnd();
        }

        private async void TrackSongEnd()
        {
            await Task.Delay((int) Math.Ceiling(musicSource.clip.length * 1000));
            if (!isChosenGameMusic) return;
            currentBackgroundSongIndex++;
            if (currentBackgroundSongIndex >= musicSettings.backgroundMusic.Length)
            {
               currentBackgroundSongIndex = 0;
            }
            PlayNextBackgroundSong();
        }
        
        public void PlayBallThrow()
        {
            ballThrowAudioSource.pitch = Random.Range(musicSettings.minPitch, musicSettings.maxPitch);
            ballThrowAudioSource.volume = musicSettings.ballThrowVolume;
            ballThrowAudioSource.PlayOneShot(musicSettings.ballThrow);
        }
        
        public void PlayCrowd()
        {
            effectsSource.volume = musicSettings.crowdVolume;
            effectsSource.PlayOneShot(musicSettings.crowd);
        }
        
        public void PlayHighBeep()
        {
            effectsSource.volume = musicSettings.highBeepVolume;
            effectsSource.PlayOneShot(musicSettings.highBeep);
        }
        
        public void PlayLowBeep()
        {
            effectsSource.volume = musicSettings.lowBeepVolume;
            effectsSource.PlayOneShot(musicSettings.lowBeep);
        }
        
        public void PlayWin()
        {
            isChosenGameMusic = false;
            musicSource.volume = musicSettings.winVolume;
            musicSource.clip = musicSettings.win;
            musicSource.loop = true;
            musicSource.Play();
            crowdSource.clip = musicSettings.winCrowd;
            crowdSource.loop = true;
            crowdSource.volume = musicSettings.winCrowdVolume;
            crowdSource.Play();
        }

        public void StopWinCrowd()
        {
            crowdSource.Stop();
        }
        
        public void PlayTargetHit()
        {
            targetAudioSource.pitch = Random.Range(musicSettings.minTargetPitch, musicSettings.maxTargetPitch);
            targetAudioSource.volume = musicSettings.targetHitVolume;
            targetAudioSource.PlayOneShot(musicSettings.targetHit);
        }
    }
}
