using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.PlayerUI
{
    public class AttemptsUIController : MonoBehaviour
    {
        [SerializeField] private GameObject puckUIPrefab;
        [SerializeField] private int maxAttemptsCount;
        [SerializeField] private Transform container;
        private readonly List<GameObject> attempts = new List<GameObject>();

        public void OnEnable()
        {
            for (var i = 0; i < maxAttemptsCount; i++)
            {
               var attempt =  Instantiate(puckUIPrefab, container);
               attempts.Add(attempt);
            }
        }

        public int GetMaxAttemptsCount() => maxAttemptsCount;

        public void DecreaseAttempt()
        {
            if (attempts.Count > 0)
            {
                Destroy(attempts[^1]);
                attempts.RemoveAt(attempts.Count-1);
            }
            else Debug.Log("Attempts empty, decrease failed");
        }
    }
}