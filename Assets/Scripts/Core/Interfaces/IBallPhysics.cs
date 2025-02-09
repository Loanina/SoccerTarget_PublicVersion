using UnityEngine;

namespace Core.Interfaces
{
    public interface IBallPhysics
    {
        void ApplyKick(Rigidbody rigidbody, Vector3 direction, float distance);
        void ApplySpin(Rigidbody rigidbody, Vector3 direction);
    }
}