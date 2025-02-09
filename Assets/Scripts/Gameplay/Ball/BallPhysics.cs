using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Ball
{
    public class BallPhysics : IBallPhysics
    {
        private readonly BallSettings settings;
        
        [Inject]
        public BallPhysics(BallSettings settings)
        {
            this.settings = settings;
        }
        
        public void ApplyKick(Rigidbody rigidbody, Vector3 direction, float distanceToTarget)
        {
            var force = Mathf.Lerp(settings.minKickForce, settings.maxKickForce, distanceToTarget / settings.maxKickDistance);

            rigidbody.velocity = direction * force;
        }

        public void ApplySpin(Rigidbody rigidbody, Vector3 direction)
        {
            var spinAxis = Vector3.Cross(direction.normalized, Vector3.up).normalized;
            rigidbody.AddTorque(-spinAxis * settings.spinForce, ForceMode.Impulse);
        }
    }
}