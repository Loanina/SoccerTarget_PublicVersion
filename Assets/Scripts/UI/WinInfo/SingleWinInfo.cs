using Core.Interfaces;
using TMPro;
using UI.Elements;
using UnityEngine;

namespace UI.WinInfo
{
    public class SingleWinInfo : MonoBehaviour, IWinInfo
    {
        [SerializeField] private UIAnimator animator;
        [SerializeField] private TextMeshProUGUI playtime;
        [SerializeField] private TextMeshProUGUI missed;

        private void OnEnable() => Show();

        public void Show() => animator.AnimateIn();
        public void Hide() => animator.AnimateOut();

        public void SetPlayTime(int time) => playtime.text = $"{time} sec";
        public void SetMissedAttempts(int misses) => missed.text = misses.ToString();
    }
}