using System;
using Audio.Managers;
using Gameplay.Crowd;
using UI.Controllers;
using UnityEngine;
using Utilities;
using Zenject;

namespace Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerUIController playerUIController;
        private CrowdAnimationController crowdAnimationController;
        private AudioManager audioManager;
        private Timer timer = new Timer();
        private int totalAttempts = 0;

        [Inject]
        public void Construct(AudioManager audioManager, CrowdAnimationController crowdAnimationController)
        {
            this.audioManager = audioManager;
            this.crowdAnimationController = crowdAnimationController;
        }
        
        public bool CanShoot { get; private set; } = false;
        public event Action<Player> OnEndGame;
        
        public void Start()
        {
            CanShoot = false;
            playerUIController.StartCountdown(OnCountdownFinished, audioManager);
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

        public int GetMissedAttempts()
        {
            return totalAttempts - playerUIController.GetMaxAttemptsCount();
        }

        private void OnCountdownFinished()
        {
            CanShoot = true;
        }

        public void DecreaseAttempt()
        {
            playerUIController.DecreaseAttempt();
            crowdAnimationController.PlayCheer(3f); 
            audioManager.PlayTargetHit();
            audioManager.PlayCrowd();
        }

        public void EndGame()
        {
            Debug.Log("EndGame вызван");
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