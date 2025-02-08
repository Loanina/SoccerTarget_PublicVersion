using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SceneLoadButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string sceneName;

        private void Awake()
        {
            if (button != null) return;
            button = GetComponent<Button>();
            if (button != null) return;
            Debug.LogWarning("Button component not found on " + gameObject.name);
            enabled = false;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("Scene name is not set for button: " + gameObject.name);
                return;
            }

            DOTween.KillAll();
            SceneLoader.LoadSceneByName(sceneName);
        }
    }
}