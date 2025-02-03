using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Crowd
{
    public class CrowdAnimationController : MonoBehaviour
    {
        [SerializeField] private List<Animator> crowdAnimator;
        private static readonly int IsCheering = Animator.StringToHash("IsCheer");
        private Coroutine cheerRoutine;

        private void Start()
        {
            StartCoroutine(Sitting());
        }

        public void PlayCheer(float duration)
        {
            if (cheerRoutine != null)
            {
                StopCoroutine(cheerRoutine);
            }
            cheerRoutine = StartCoroutine(CheerCoroutine(duration));
        }
        
        public void PlayInfiniteCheer()
        {
            if (cheerRoutine != null)
            {
                StopCoroutine(cheerRoutine);
            }

            foreach (var animator in crowdAnimator)
            {
                animator.SetBool(IsCheering, true);
            }
        }

        private IEnumerator Sitting()
        {
            foreach (var animator in crowdAnimator)
            {
                var delay = Random.Range(0f, 0.1f);
                yield return new WaitForSeconds(delay);
                animator.SetTrigger("Sitting");
            }
        }

        private IEnumerator CheerCoroutine(float duration)
        {
            foreach (var animator in crowdAnimator)
            {
                var delay = Random.Range(0.001f, 0.05f);
                yield return new WaitForSeconds(delay);
                animator.SetBool(IsCheering, true);
            }
            yield return new WaitForSeconds(duration);
            foreach (var animator in crowdAnimator)
            {
                var delay = Random.Range(0f, 0.05f);
                yield return new WaitForSeconds(delay);
                animator.SetBool(IsCheering, false);
            }
        }
    }
}
