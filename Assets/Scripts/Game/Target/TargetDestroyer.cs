using System;
using UnityEngine;
using Zenject;

namespace Game.Target
{
    public class TargetDestroyer
    {
        private readonly TargetConfig targetConfig;
        private readonly DiContainer container;
        private readonly Transform parent;

        public TargetDestroyer(TargetConfig targetConfig, DiContainer container, Transform parent)
        {
            this.targetConfig = targetConfig;
            this.container = container;
            this.parent = parent;
        }

        public void DestroyTarget(GameObject target, Action onDestroyed)
        {
            SpawnDestroyEffect(target);
            var destruction = target.GetComponent<DestructionObject>();

            if (destruction != null)
            {
                destruction.DestroyTarget(() => onDestroyed?.Invoke());
            }
            else
            {
                GameObject.Destroy(target);
                onDestroyed.Invoke();
            }
        }
        
        private void SpawnDestroyEffect(GameObject target)
        {
            if (targetConfig.destroyEffect == null) return;
            
            var effect = container.InstantiatePrefab(targetConfig.destroyEffect, target.transform.position,
                Quaternion.identity, parent);
            GameObject.Destroy(effect, targetConfig.timeToRemoveEffects);
        }
    }
}