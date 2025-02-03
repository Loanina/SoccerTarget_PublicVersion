using System.Threading.Tasks;
using UnityEngine;

namespace Game.Ball
{
    public interface IBallDestroyer
    {
        Task DestroyWithEffect(GameObject ball, Transform parent);
    }
}