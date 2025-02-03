using UnityEngine;

namespace Music
{
    [CreateAssetMenu(fileName = "MusicSettings", menuName = "Settings/MusicSettings")]

    public class MusicSettings : ScriptableObject
    {
        public AudioClip menuMusic;
        [Range(0f, 1f)] public float menuMusicVolume;
        public AudioClip[] backgroundMusic;
        [Range(0f, 1f)] public float backgroundMusicVolume;
        
        public AudioClip ballThrow;
        [Range(0f, 1f)] public float ballThrowVolume;
        [Range(0.5f, 2f)] public float maxPitch;
        [Range(0.5f, 2f)] public float minPitch;
        
        public AudioClip crowd;
        [Range(0f, 1f)] public float crowdVolume;

        public AudioClip winCrowd;
        [Range(0f, 1f)] public float winCrowdVolume;
        
        public AudioClip win;
        [Range(0f, 1f)] public float winVolume;
        
        public AudioClip highBeep;
        [Range(0f, 1f)] public float highBeepVolume;
        
        public AudioClip lowBeep;
        [Range(0f, 1f)] public float lowBeepVolume;
        
        public AudioClip targetHit;
        [Range(0f, 1f)] public float targetHitVolume;
        [Range(0.5f, 2f)] public float maxTargetPitch;
        [Range(0.5f, 2f)] public float minTargetPitch;
    }
}

