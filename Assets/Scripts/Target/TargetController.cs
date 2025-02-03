using UnityEngine;
using Zenject;

namespace Target
{
    public class TargetController : IInitializable
    {
        private readonly TargetConfig targetConfig;
        private readonly DiContainer container;
        private int currentTargetIndex = 0;
        private GameObject currentTarget;
        private readonly Player.Player player;
        private readonly Transform parent;
        private bool isCurrentTargetDestroying;

        [Inject]
        public TargetController(TargetConfig targetConfig, DiContainer container, Player.Player player, Transform parent)
        {
            this.targetConfig = targetConfig;
            this.container = container;
            this.player = player;
            this.parent = parent;
        }

        public void Initialize()
        {
            SpawnTarget();
        }

        private void OnTargetHit()
        {
            if (currentTarget == null || isCurrentTargetDestroying) return;
            isCurrentTargetDestroying = true;
            RemoveTarget(currentTarget);
            player.DecreaseAttempt();
        }

        private void SpawnTarget()
        {
            if (currentTargetIndex < targetConfig.spawnPoints.Count)
            {
                currentTarget = container.InstantiatePrefab(targetConfig.targetReference, parent);
                currentTarget.transform.localPosition = targetConfig.spawnPoints[currentTargetIndex];
                SubscribeCurrentTarget();
                currentTargetIndex++;
            }
            else
            {
                player.EndGame();
            }
        }

        private void RemoveTarget(GameObject target)
        {
            SpawnDestroyEffect(target);
            var destruction = target.GetComponent<DestructionObject>();
            destruction.DestroyTarget(() =>
            {
                UnsubscribeCurrentTarget();
                isCurrentTargetDestroying = false;
                SpawnTarget();
            });
        }

        private void SubscribeCurrentTarget()
        {
            var target = currentTarget.GetComponent<Target>();
            if (target != null)
            {
                target.OnHit += OnTargetHit;
            }
        }
        
        private void UnsubscribeCurrentTarget()
        {
            var target = currentTarget.GetComponent<Target>();
            if (target != null)
            {
                target.OnHit -= OnTargetHit;
            }
        }

        private void SpawnDestroyEffect(GameObject target)
        {
            var effect = container.InstantiatePrefab(targetConfig.destroyEffect, target.transform.position,
                new Quaternion(), parent);
            Object.Destroy(effect, targetConfig.timeToRemoveEffects);
        }
    }
}