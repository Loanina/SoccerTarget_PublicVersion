using System;
using UnityEngine;

namespace Game.Target
{
    public class Target : MonoBehaviour
    {
        public event Action OnHit;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Ball")) return;
            OnHit?.Invoke();
        }
    }
}