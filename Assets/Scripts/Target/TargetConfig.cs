using System.Collections.Generic;
using UnityEngine;

namespace Target
{
    [CreateAssetMenu (fileName = "TargetConfig", menuName = "Settings/Target config")]
    public class TargetConfig : ScriptableObject
    {
        [Range(0f, 10f)] public float timeToRemoveEffects;
        public GameObject targetReference;
        public GameObject destroyEffect;
        public List<Vector3> spawnPoints;
    }
}