using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace VFX
{
    public class LightFlicker : MonoBehaviour
    {
        [SerializeField] private Light targetLight;
        [SerializeField, Range(0f, 10f)] public float minIntensity = 0f;
        [SerializeField, Range(0f, 30f)] public float maxIntensity = 5f;
        [SerializeField, Range(0.1f, 5f)] public float flickerSpeed = 1f;
        [SerializeField, Range(0f, 10f)] public float flickerDelay = 0.5f;
        [SerializeField, Range(0f, 20f)] public float initialDelay = 1f;
        private void Start()
        {
            StartCoroutine(StartFlicker());
        }

        private IEnumerator StartFlicker()
        {
            targetLight.enabled = true;
            targetLight.intensity = minIntensity;
            var randomDelay = Random.Range(0, initialDelay);
            yield return new WaitForSeconds(randomDelay);

            var sequence = DOTween.Sequence(targetLight);
            sequence.Append(targetLight.DOIntensity(maxIntensity, flickerSpeed / 2));
            sequence.Append(targetLight.DOIntensity(minIntensity, flickerSpeed / 2)).OnComplete(() =>
            {
                targetLight.enabled = false;
            });
            sequence.AppendInterval(flickerDelay).OnComplete((() =>
            {
                targetLight.enabled = true;
            }));
            sequence.SetLoops(-1, LoopType.Restart);
        }

        private void OnDisable()
        {
            DOTween.Kill(targetLight);
        }
    }
}