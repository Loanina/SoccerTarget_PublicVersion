using Music;
using UnityEngine;
using Zenject;

namespace Ball
{
    public class BallThrower
    {
        private readonly BallSpawner ballSpawner;
        private readonly BallSettings ballSettings;
        private readonly AudioManager audioManager;

        [Inject]
        public BallThrower(BallSpawner ballSpawner, BallSettings ballSettings, AudioManager audioManager)
        {
            this.ballSpawner = ballSpawner;
            this.ballSettings = ballSettings;
            this.audioManager = audioManager;
        }
        
        public void Throw(Vector3 targetPosition)
        {
            var ball = ballSpawner.Spawn();
            var rigidbody = ball.GetComponent<Rigidbody>();
            
            var kickPoint = ballSettings.kickPoint;
            ball.transform.localPosition = kickPoint;

            var localTargetPosition = ball.transform.parent.InverseTransformPoint(targetPosition);
            var direction = localTargetPosition - kickPoint;
            var distanceToTarget = Vector3.Distance(ballSettings.kickPoint, localTargetPosition);

            audioManager.PlayBallThrow();

            KickMoveStraight(rigidbody, direction, distanceToTarget);
            AddReverseSpin(rigidbody, direction);
        }
        
        private void KickMoveStraight(Rigidbody ballRigidbody, Vector3 direction, float distanceToTarget)
        {
            var force = Mathf.Lerp(ballSettings.minKickForce, ballSettings.maxKickForce, distanceToTarget / ballSettings.maxKickDistance);

            ballRigidbody.velocity = direction * force;
        }

        private void AddReverseSpin(Rigidbody ballRigidbody, Vector3 direction)
        {
            var spinAxis = Vector3.Cross(direction.normalized, Vector3.up).normalized;

            ballRigidbody.AddTorque(-spinAxis * ballSettings.spinForce, ForceMode.Impulse);
        }
    }
}