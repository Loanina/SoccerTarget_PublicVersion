using System;
using System.Collections;
using Music;
using TMPro;
using UnityEngine;

namespace UI.PlayerUI
{
    public class CountdownUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        public event Action CountdownFinished;

        public void StartCountDown(AudioManager audioManager)
        {
            StartCoroutine(Countdown(audioManager));
        }

        private IEnumerator Countdown(AudioManager audioManager)
        {
            var time = 3;
            while (time > 0)
            {
                audioManager.PlayLowBeep();
                textMeshPro.text = time.ToString();
                yield return new WaitForSeconds(0.9f);
                time--;
            }
            audioManager.PlayHighBeep();
            textMeshPro.text = "GO!";
            yield return new WaitForSeconds(1f);
            textMeshPro.enabled = false;
            CountdownFinished?.Invoke();
        }
    }
}