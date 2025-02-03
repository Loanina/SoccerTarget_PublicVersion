using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Ball
{
    public class BallDestroyer : IBallDestroyer
    {
        private readonly DiContainer container;
        private readonly BallSettings settings;

        public BallDestroyer(DiContainer container, BallSettings settings)
        {
            this.container = container;
            this.settings = settings;
        }

        public async Task DestroyWithEffect(GameObject ball, Transform parent)
        {
            await Task.Delay(TimeSpan.FromSeconds(settings.destroyDelay));

            if (settings.destroyEffect != null)
            {
                var effect = container.InstantiatePrefab(settings.destroyEffect, parent);
                effect.transform.position = ball.transform.position;
                Object.Destroy(effect, 2f);
            }
            Object.Destroy(ball);
        }
    }
}