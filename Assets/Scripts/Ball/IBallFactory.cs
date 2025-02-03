using UnityEngine;

namespace Ball
{
    public interface IBallFactory
    {
        GameObject CreateBall(Transform parent);
    }
}