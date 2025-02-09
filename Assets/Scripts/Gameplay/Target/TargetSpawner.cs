using UnityEngine;
using Zenject;

namespace Gameplay.Target
{
    public class TargetSpawner
    {
        private readonly TargetConfig targetConfig;
        private readonly DiContainer container;
        private readonly Transform parent;
        private int currentTargetIndex;

        [Inject]
        public TargetSpawner(TargetConfig targetConfig, DiContainer container, Transform parent)
        {
            this.targetConfig = targetConfig;
            this.container = container;
            this.parent = parent;
            currentTargetIndex = 0;
        }

        public GameObject SpawnNextTarget()
        {
            if (currentTargetIndex >= targetConfig.spawnPoints.Count)
            {
                return null;
            }

            var target = container.InstantiatePrefab(targetConfig.targetReference, parent);
            target.transform.localPosition = targetConfig.spawnPoints[currentTargetIndex];
            currentTargetIndex++;
            return target;
        }

        public bool HasMoreTargets() => currentTargetIndex < targetConfig.spawnPoints.Count;
    }
}