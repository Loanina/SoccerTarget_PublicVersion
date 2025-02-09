using UnityEngine;

namespace Core.Interfaces
{
    public interface IBallFactory
    {
        GameObject CreateBall(Transform parent);
    }
}