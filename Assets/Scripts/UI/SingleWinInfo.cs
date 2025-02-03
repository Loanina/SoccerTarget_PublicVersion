using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SingleWinInfo : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float offScreenOffset = 100f;
        [SerializeField] private TextMeshProUGUI playtime;
        [SerializeField] private TextMeshProUGUI missed;
        
        public void OnEnable()
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, offScreenOffset);
            AnimateButtons();
        }

        private void AnimateButtons()
        {
            rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - offScreenOffset, animationDuration).SetEase(Ease.OutExpo);
        }

        public void SetPlayTime(int time)
        {
            playtime.text = time + " sec";
        }
        
        public void SetMissedAttempts(int misses)
        {
            missed.text = misses.ToString();
        }
    }
}