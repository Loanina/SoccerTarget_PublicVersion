using UnityEngine;

namespace Game.Ball
{
    public interface IBallFactory
    {
        GameObject CreateBall(Transform parent);
    }
}