using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace VFX
{
    public class LightManager : MonoBehaviour
    {
        [SerializeField] private List<Light> lights;
        [SerializeField, Range(0.5f, 5f)] private float minFlickerDuration = 1f;
        [SerializeField, Range(0.5f, 5f)] private float maxFlickerDuration = 3f;
        [SerializeField, Range(1, 10)] private int minActiveLights = 4;
        [SerializeField, Range(1, 10)] private int maxActiveLights = 8;
        [SerializeField, Range(0f, 5f)] private float minDelay = 0f;
        [SerializeField, Range(0f, 5f)] private float maxDelay = 1f;
        [SerializeField, Range(0f, 10f)] public float minIntensity = 0.5f;
        [SerializeField, Range(0f, 10f)] public float maxIntensity = 5f;
        

        private readonly List<Light> activeLights = new();
        private readonly Dictionary<Light, Sequence> lightSequences = new();

        private void Start()
        {
            StartCoroutine(ManageLights());
        }

        private IEnumerator ManageLights()
        {
            while (true)
            {
                var targetActiveCount = Random.Range(minActiveLights, maxActiveLights + 1);

                while (activeLights.Count < targetActiveCount)
                {
                    var light = GetRandomInactiveLight();
                    if (light != null) ActivateLight(light);
                }

                while (activeLights.Count > targetActiveCount)
                {
                    DeactivateLight(activeLights[0]);
                }

                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            }
        }

        private Light GetRandomInactiveLight()
        {
            var inactiveLights = lights.FindAll(l => !activeLights.Contains(l));
            return inactiveLights.Count > 0 ? inactiveLights[Random.Range(0, inactiveLights.Count)] : null;
        }

        private void ActivateLight(Light light)
        {
            if (light == null || activeLights.Contains(light)) return;

            activeLights.Add(light);
            light.enabled = true;

            var flickerDuration = Random.Range(minFlickerDuration, maxFlickerDuration);
            var flickerSequence = DOTween.Sequence()
                .Append(light.DOIntensity(Random.Range(minIntensity, maxIntensity), flickerDuration / 2))
                .Append(light.DOIntensity(0f, flickerDuration / 2))
                .OnComplete(() => DeactivateLight(light));

            lightSequences[light] = flickerSequence;
        }

        private void DeactivateLight(Light light)
        {
            if (light == null || !activeLights.Contains(light)) return;

            activeLights.Remove(light);
            light.enabled = false;

            if (!lightSequences.TryGetValue(light, out var sequence)) return;
            sequence.Kill();
            lightSequences.Remove(light);
        }

        private void OnDestroy()
        {
            foreach (var sequence in lightSequences.Values)
            {
                sequence.Kill();
            }
            lightSequences.Clear();
        }
    }
}
