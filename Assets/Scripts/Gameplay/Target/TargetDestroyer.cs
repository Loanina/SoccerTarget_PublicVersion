using System;
using Gameplay.Сrumble;
using UnityEngine;
using Zenject;

namespace Gameplay.Target
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
            var destruction = target.GetComponent<CrumblingObject>();

            if (destruction != null)
            {
                destruction.Crumble(() => onDestroyed?.Invoke());
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