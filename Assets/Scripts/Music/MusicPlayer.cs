using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Music
{
    public class MusicPlayer : IInitializable
    {
        private readonly MusicSettings musicSettings;
        private AudioSource musicSource;
        private int currentTrackIndex;
        private bool isGameMusicPlaying;
        
        [Inject]
        public MusicPlayer(MusicSettings musicSettings)
        {
            this.musicSettings = musicSettings;
        }

        public void Initialize()
        {
            musicSource = new GameObject("MusicPlayer").AddComponent<AudioSource>();
            Object.DontDestroyOnLoad(musicSource.gameObject);
            PlayMenuMusic();
        }
        
        public void PlayWinMusic()
        {
            StopMusic();
            musicSource.volume = musicSettings.winVolume;
            musicSource.clip = musicSettings.win;
            musicSource.loop = true;
            musicSource.Play();
        }
        
        public void PlayMenuMusic()
        {
            StopMusic();
            isGameMusicPlaying = false;
            musicSource.loop = true;
            musicSource.volume = musicSettings.menuMusicVolume;
            musicSource.clip = musicSettings.menuMusic;
            musicSource.Play();
        }

        public void PlayGameMusic()
        {
            StopMusic();
            isGameMusicPlaying = true;
            musicSource.loop = false;
            musicSource.volume = musicSettings.backgroundMusicVolume;
            currentTrackIndex = 0;
            PlayNextTrack();
        }

        private async void PlayNextTrack()
        {
            if (currentTrackIndex >= musicSettings.backgroundMusic.Length) currentTrackIndex = 0;

            musicSource.clip = musicSettings.backgroundMusic[currentTrackIndex];
            musicSource.Play();
            await TrackSongEnd();
        }

        private async Task TrackSongEnd()
        {
            await Task.Delay((int)(musicSource.clip.length * 1000));
            if (!isGameMusicPlaying) return;
            currentTrackIndex++;
            PlayNextTrack();
        }

        public void StopMusic()
        {
            isGameMusicPlaying = false;
            musicSource.Stop();
        }
    }
}