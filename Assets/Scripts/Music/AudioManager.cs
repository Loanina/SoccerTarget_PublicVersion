using Zenject;

namespace Music
{
    public class AudioManager
    {
        private readonly MusicPlayer musicPlayer;
        private readonly SoundEffectsPlayer soundEffectsPlayer;

        [Inject]
        public AudioManager(MusicPlayer musicPlayer, SoundEffectsPlayer soundEffectsPlayer)
        {
            this.musicPlayer = musicPlayer;
            this.soundEffectsPlayer = soundEffectsPlayer;
        }

        public void StopGameSounds() => soundEffectsPlayer.StopAllSounds();

        public void StopAll()
        {
            soundEffectsPlayer.StopAllSounds();
            musicPlayer.StopMusic();
        }
        public void PlayGameMusic() => musicPlayer.PlayGameMusic();
        public void PlayMenuMusic() => musicPlayer.PlayMenuMusic();
        public void PlayBallThrow() => soundEffectsPlayer.PlayBallThrow();
        public void PlayCrowd() => soundEffectsPlayer.PlayCrowd();
        public void PlayWin()
        {
            soundEffectsPlayer.PlayWinCrowd();
            musicPlayer.PlayWinMusic();
        }
        public void PlayHighBeep() => soundEffectsPlayer.PlayHighBeep();
        public void PlayLowBeep() => soundEffectsPlayer.PlayLowBeep();
        public void PlayTargetHit() => soundEffectsPlayer.PlayTargetHit();
    }
}
