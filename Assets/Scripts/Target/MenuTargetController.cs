using UnityEngine;

namespace Target
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
            var destroy = target.GetComponent<DestructionObject>();
            destroy.DestroyTarget();
            var effect = Instantiate(destroyEffect, target.transform.position, new Quaternion(), null);
            Destroy(effect, 2f);
        }
    }
}
