using Audio.Managers;
using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Ball
{
    public class BallThrower
    {
        private readonly BallSpawner ballSpawner;
        private readonly IBallPhysics ballPhysics;
        private readonly AudioManager audioManager;
        private readonly BallSettings settings;

        [Inject]
        public BallThrower(BallSpawner ballSpawner, IBallPhysics ballPhysics, AudioManager audioManager, BallSettings settings)
        {
            this.ballSpawner = ballSpawner;
            this.ballPhysics = ballPhysics;
            this.audioManager = audioManager;
            this.settings = settings;
        }
        
        public void Throw(Vector3 targetPosition)
        {
            var ball = ballSpawner.Spawn();
            var rigidbody = ball.GetComponent<Rigidbody>();
            ball.transform.localPosition = settings.kickPoint;
            
            var localTargetPosition = ball.transform.parent.InverseTransformPoint(targetPosition);
            var direction = localTargetPosition - settings.kickPoint;
            var distanceToTarget = Vector3.Distance(settings.kickPoint, localTargetPosition);
            
            audioManager.PlayBallThrow();

            ballPhysics.ApplyKick(rigidbody, direction, distanceToTarget);
            ballPhysics.ApplySpin(rigidbody, direction);
        }
    }
}