using System.Threading.Tasks;
using UnityEngine;

namespace Core.Interfaces
{
    public interface IBallDestroyer
    {
        Task DestroyWithEffect(GameObject ball, Transform parent);
    }
}