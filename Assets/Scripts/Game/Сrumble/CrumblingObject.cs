using System;
using System.Collections;
using UnityEngine;

namespace Game.Сrumble
{
    public class CrumblingObject : MonoBehaviour
    {
        [SerializeField, Range(0,20)] private float fragmentLifetime = 1f;
        [SerializeField] private GameObject modelGameObject;
        [SerializeField] private GameObject fragmentsParent;

        private bool isDestroying;

        public void Crumble(Action onComplete = null)
        {
            if (isDestroying) return;
            isDestroying = true;
            
            modelGameObject.SetActive(false);
            fragmentsParent.SetActive(true);

            StartCoroutine(CrumbleCoroutine(onComplete));
        }

        private IEnumerator CrumbleCoroutine(Action onComplete)
        {
            yield return new WaitForSeconds(fragmentLifetime);
            onComplete?.Invoke();
            Destroy(gameObject);
        }
    }
}