using System.Threading.Tasks;
using UnityEngine;

namespace Ball
{
    public interface IBallDestroyer
    {
        Task DestroyWithEffect(GameObject ball, Transform parent);
    }
}