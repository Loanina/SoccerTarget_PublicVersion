using Audio.Managers;
using UnityEngine;

namespace UI.Controllers
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] private AttemptsUIController attemptsUI;
        [SerializeField] private CountdownUIController countdownUI;

        public void StartCountdown(System.Action onFinish, AudioManager audioManager)
        {
            countdownUI.CountdownFinished += onFinish;
            countdownUI.StartCountDown(audioManager);
        }

        public int GetMaxAttemptsCount() => attemptsUI.GetMaxAttemptsCount();

        public void DecreaseAttempt()
        {
            attemptsUI.DecreaseAttempt();
        }
    }
}