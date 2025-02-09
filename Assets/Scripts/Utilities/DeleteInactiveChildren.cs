using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class DeleteInactiveChildren : MonoBehaviour {
        [ContextMenu("Delete Inactive Children")]
        private void DeleteInactiveObjects() {
            RemoveInactiveObjects(transform);
        }

        private void RemoveInactiveObjects(Transform parent) {
            var inactiveChildren = new List<Transform>();

            foreach (Transform child in parent) {
                if (!child.gameObject.activeSelf) {
                    inactiveChildren.Add(child);
                } else {
                    RemoveInactiveObjects(child);
                }
            }

            foreach (var inactiveChild in inactiveChildren) {
                DestroyImmediate(inactiveChild.gameObject);
            }

            Debug.Log($"Deleted inactive children under {parent.name}");
        }
    }
}
