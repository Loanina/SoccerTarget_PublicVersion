using UnityEngine;

namespace Game.Ball
{
    [CreateAssetMenu (fileName = "BallSettings", menuName = "Settings/Ball settings")]
    public class BallSettings : ScriptableObject
    {
        public GameObject ballReference;
        public Vector3 kickPoint;
        [Range(1f, 50f)] public float minKickForce = 5f;
        [Range(1f, 100f)] public float maxKickForce = 25f;
        [Range(1f, 50f)] public float maxKickDistance = 10f;
        [Range(0f, 100f)] public float spinForce = 20f;
        public Vector3 localSpawnPoint;
        public GameObject destroyEffect;
        [Range(1f, 20f)] public float destroyDelay;
    }
}