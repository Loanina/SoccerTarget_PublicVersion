using Gameplay.Сrumble;
using UnityEngine;

namespace Gameplay.Target
{
    public class MenuTargetController : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject destroyEffect;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Ball")) return;
            Hit();
        }

        private void Hit()
        {
            if (target.TryGetComponent(out CrumblingObject destroy))
            {
                destroy.Crumble();
            }

            if (destroyEffect == null) return;
            var effect = Instantiate(destroyEffect, target.transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}
