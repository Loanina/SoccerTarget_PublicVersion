using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Managers;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Gameplay.Ball
{
    public class BallDestroyer : IBallDestroyer
    {
        private readonly DiContainer container;
        private readonly BallSettings settings;
        private readonly GlobalLifecycleManager lifecycleManager;

        public BallDestroyer(DiContainer container, BallSettings settings, GlobalLifecycleManager lifecycleManager)
        {
            this.container = container;
            this.settings = settings;
            this.lifecycleManager = lifecycleManager;
        }

        public async Task DestroyWithEffect(GameObject ball, Transform parent)
        {
            var token = lifecycleManager.RegisterTask();
            
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(settings.destroyDelay), token);
                
                if (settings.destroyEffect != null && ball != null)
                {
                    var effect = container.InstantiatePrefab(settings.destroyEffect, parent);
                    effect.transform.position = ball.transform.position;
                    if (Application.isPlaying)
                    {
                        Object.Destroy(effect, 2f);
                    }
                    else
                    {
                        Object.DestroyImmediate(effect);
                    }

                }
                Object.Destroy(ball);
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Ball destroy with effect was cancelled");
            }
        }
    }
}