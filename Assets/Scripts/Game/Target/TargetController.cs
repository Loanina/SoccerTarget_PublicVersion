using UnityEngine;
using Zenject;

namespace Game.Target
{
    public class TargetController : IInitializable
    {
        /*
         * Вынести спавн мишеней в отдельный компонент/фабрику.
        Логику уничтожения мишеней (включая спавн эффекта) вынести в отдельный класс или сервис.
        Управление игровым состоянием (например, завершение игры) лучше обрабатывать через GameManager или через интерфейс, отвечающий за игровой цикл.
        Рассмотреть абстракцию для создания объектов вместо прямого использования DiContainer.
         */
        
        private GameObject currentTarget;
        private bool isCurrentTargetDestroying;
        private readonly Player.Player player;
        private readonly TargetSpawner targetSpawner;
        private readonly TargetDestroyer targetDestroyer;
        

        [Inject]
        public TargetController(TargetSpawner targetSpawner, TargetDestroyer targetDestroyer, Player.Player player)
        {
            this.targetSpawner = targetSpawner;
            this.targetDestroyer = targetDestroyer;
            this.player = player;
        }

        public void Initialize()
        {
            SpawnTarget();
        }

        private void OnTargetHit()
        {
            if (currentTarget == null || isCurrentTargetDestroying) return;
            
            isCurrentTargetDestroying = true;
            UnsubscribeCurrentTarget();
            
            targetDestroyer.DestroyTarget(currentTarget, () =>
            {
                isCurrentTargetDestroying = false;
                
                SpawnTarget();
            });
            player.DecreaseAttempt();
        }

        private void SpawnTarget()
        {
            if (!targetSpawner.HasMoreTargets())
            {
                player.EndGame();
                return;
            }

            currentTarget = targetSpawner.SpawnNextTarget();
            SubscribeCurrentTarget();
        }

        private void SubscribeCurrentTarget()
        {
            if (currentTarget== null) return;
            var target = currentTarget.GetComponent<Target>();
            if (target != null) target.OnHit += OnTargetHit;
        }
        
        private void UnsubscribeCurrentTarget()
        {
            if (currentTarget == null) return;
            var target = currentTarget.GetComponent<Target>();
            if (target != null) target.OnHit -= OnTargetHit;
        }
    }
}