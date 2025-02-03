using DG.Tweening;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class SceneLoadButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string sceneName;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float offScreenOffset = 500f;

        public void OnEnable()
        {
            rectTransform.anchoredPosition = new Vector2(offScreenOffset + rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
            AnimateButtons();
            DOVirtual.DelayedCall(2f, () =>
            {
                if (button != null)
                {
                    button.onClick.AddListener(LoadScene);
                }
                else Debug.Log("Button component doesnt found");
            });
        }
        
        private void OnDisable()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(LoadScene);
            }
            else Debug.Log("Button component doesnt found");
        }

        private void AnimateButtons()
        {
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - offScreenOffset, animationDuration).SetEase(Ease.OutExpo);
        }

        private void LoadScene()
        {
            DOTween.KillAll();
            SceneLoader.LoadSceneByName(sceneName);
        }
    }
}