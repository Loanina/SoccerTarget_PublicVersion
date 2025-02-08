using TMPro;
using UnityEngine;

namespace UI.WinInfo
{
    public class MultiWinInfo : MonoBehaviour, IWinInfo
    {
        [SerializeField] private UIAnimator animator;
        [SerializeField] private TextMeshProUGUI winnerInfo;
        [SerializeField] private TextMeshProUGUI redPlaytime;
        [SerializeField] private TextMeshProUGUI redMissed;
        [SerializeField] private TextMeshProUGUI bluePlaytime;
        [SerializeField] private TextMeshProUGUI blueMissed;

        private void OnEnable() => Show();

        public void Show() => animator.AnimateIn();
        public void Hide() => animator.AnimateOut();

        public void SetWinInfo(string winner, int redTime, int blueTime, int redMissed, int blueMissed)
        {
            winnerInfo.text = winner;
            this.redPlaytime.text = $"{redTime} sec";
            this.bluePlaytime.text = $"{blueTime} sec";
            this.redMissed.text = redMissed.ToString();
            this.blueMissed.text = blueMissed.ToString();
        }
    }
}