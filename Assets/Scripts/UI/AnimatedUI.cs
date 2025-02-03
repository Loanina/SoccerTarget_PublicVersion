using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class AnimatedUI : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float offScreenOffset = 500f;
        
        public void OnEnable()
        {
            rectTransform.anchoredPosition = new Vector2(offScreenOffset + rectTransform.anchoredPosition.x,
                rectTransform.anchoredPosition.y);
            AnimateButtons();
        }
        
        private void AnimateButtons()
        {
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - offScreenOffset, animationDuration).SetEase(Ease.OutExpo);
        }
    }
}