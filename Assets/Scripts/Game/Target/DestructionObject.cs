using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Target
{
    public class DestructionObject : MonoBehaviour
    {
        [SerializeField, Range(0,20)] private float fragmentLifetime = 1f;
        [SerializeField] private GameObject modelGameObject;
        [SerializeField] private GameObject fragmentsParent;

        private bool IsDestroying { get; set; }

        public void DestroyTarget(Action onComplete = null)
        {
            if (IsDestroying) return;
            IsDestroying = true;
            
            modelGameObject.SetActive(false);
            fragmentsParent.SetActive(true);
            
            DOVirtual.DelayedCall(fragmentLifetime, () =>
            {
                onComplete?.Invoke();
                Destroy(gameObject);
            });
        }
    }
}