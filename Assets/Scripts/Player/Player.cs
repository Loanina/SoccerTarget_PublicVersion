using System;
using Crowd;
using Music;
using UnityEngine;
using VFX;
using Zenject;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private AttemptsUIController attemptsUIController;
        [SerializeField] private CountdownUIController countdownUIController;
        [SerializeField] private CrowdAnimationController crowdAnimationController;
        private AudioManager audioManager;
        private Timer timer = new Timer();
        private int totalAttempts = 0;

        [Inject]
        public void Construct(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }
        
        public bool CanShoot { get; private set; } = false;
        public event Action<Player> OnEndGame;
        
        public void Start()
        {
            CanShoot = false;
            countdownUIController.CountdownFinished += OnCountdownFinished;
            countdownUIController.StartCountDown(audioManager);
            timer.Start();
        }

        public void IncreaseTotalAttempts()
        {
            totalAttempts++;
        }

        public int GetTotalAttempts()
        {
            return totalAttempts;
        }

        private void OnCountdownFinished()
        {
            CanShoot = true;
            countdownUIController.CountdownFinished -= OnCountdownFinished;
        }

        public void DecreaseAttempt()
        {
            attemptsUIController.DecreaseAttempt();
            crowdAnimationController.PlayCheer(3f); 
            audioManager.PlayTargetHit();
            audioManager.PlayCrowd();
        }

        public void EndGame()
        {
            OnEndGame?.Invoke(this);
            CanShoot = false;
            timer.Stop();
        }

        public void Win()
        {
            crowdAnimationController.PlayInfiniteCheer();
            audioManager.PlayWin();
        }

        public int GetPlaytime()
        {
            return (int)timer.GetElapsedTime() - 4;
        }
    }
}