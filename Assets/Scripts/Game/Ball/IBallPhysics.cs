using UnityEngine;

namespace Game.Ball
{
    public interface IBallPhysics
    {
        void ApplyKick(Rigidbody rigidbody, Vector3 direction, float distance);
        void ApplySpin(Rigidbody rigidbody, Vector3 direction);
    }
}