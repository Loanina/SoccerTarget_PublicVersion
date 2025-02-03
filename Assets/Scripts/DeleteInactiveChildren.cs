using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DeleteInactiveChildren : MonoBehaviour {
    [ContextMenu("Delete Inactive Children")]
    private void DeleteInactiveObjects() {
        // Start the deletion process
        RemoveInactiveObjects(transform);
    }

    private void RemoveInactiveObjects(Transform parent) {
        // Create a list to hold inactive children to delete
        List<Transform> inactiveChildren = new List<Transform>();

        foreach (Transform child in parent) {
            // Recursively check child objects
            if (!child.gameObject.activeSelf) {
                inactiveChildren.Add(child);
            } else {
                RemoveInactiveObjects(child);
            }
        }

        // Delete inactive children
        foreach (Transform inactiveChild in inactiveChildren) {
            DestroyImmediate(inactiveChild.gameObject);
        }

        Debug.Log($"Deleted inactive children under {parent.name}");
    }
}
