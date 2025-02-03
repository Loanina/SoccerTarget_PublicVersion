using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MultiWinInfo : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float offScreenOffset = 100f;
        [SerializeField] private TextMeshProUGUI winnerInfo;
        [SerializeField] private TextMeshProUGUI redPlaytime;
        [SerializeField] private TextMeshProUGUI redMissed;
        [SerializeField] private TextMeshProUGUI bluePlaytime;
        [SerializeField] private TextMeshProUGUI blueMissed;
        
        public void OnEnable()
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, offScreenOffset);
            AnimateButtons();
        }

        private void AnimateButtons()
        {
            rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - offScreenOffset, animationDuration).SetEase(Ease.OutExpo);
        }

        public void SetWinInfo(string winnerInfo, int redPlaytime, int bluePlaytime, int redMissed, int blueMissed)
        {
            this.winnerInfo.text = winnerInfo;
            this.redPlaytime.text = redPlaytime + " sec";
            this.bluePlaytime.text = bluePlaytime + " sec";
            this.redMissed.text = redMissed.ToString();
            this.blueMissed.text = blueMissed.ToString();
        }
    }
}