using UnityEngine;
using Zenject;

namespace Game.Ball
{
    public class BallFactory : IBallFactory
    {
        private readonly DiContainer container;
        private readonly BallSettings settings;
        
        public BallFactory(DiContainer container, BallSettings settings)
        {
            this.container = container;
            this.settings = settings;
        }

        public GameObject CreateBall(Transform parent)
        {
            return container.InstantiatePrefab(settings.ballReference, parent);
        }
    }
}