using DG.Tweening;
using UnityEngine;

namespace UI.Elements
{
    public class UIAnimator : MonoBehaviour
    {
        private enum SlideDirection { Left, Right, Top, Bottom }

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float offScreenOffset = 500f;
        [SerializeField] private Ease easeTypeIn = Ease.OutExpo;
        [SerializeField] private Ease easeTypeOut = Ease.InExpo;
        [SerializeField] private SlideDirection slideDirection = SlideDirection.Top;

        private Vector2 originalPosition;

        private void Awake()
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            originalPosition = rectTransform.anchoredPosition;
        }

        private void OnEnable()
        {
            InitializeStartPosition();
            AnimateIn();
        }

        public void AnimateIn()
        {
            rectTransform.DOAnchorPos(originalPosition, animationDuration).SetEase(easeTypeIn);
        }

        public void AnimateOut()
        {
            rectTransform.DOAnchorPos(GetOffScreenPosition(), animationDuration).SetEase(easeTypeOut);
        }

        private void InitializeStartPosition()
        {
            rectTransform.anchoredPosition = GetOffScreenPosition();
        }

        private Vector2 GetOffScreenPosition()
        {
            var offset = slideDirection switch
            {
                SlideDirection.Right => new Vector2(-offScreenOffset, 0),
                SlideDirection.Left => new Vector2(offScreenOffset, 0),
                SlideDirection.Bottom => new Vector2(0, offScreenOffset),
                SlideDirection.Top => new Vector2(0, -offScreenOffset),
                _ => Vector2.zero
            };

            return originalPosition + offset;
        }
    }
}