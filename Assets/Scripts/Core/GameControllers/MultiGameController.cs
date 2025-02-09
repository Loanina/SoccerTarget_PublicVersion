using Audio.Managers;
using Gameplay.Player;
using TouchScript.Layers;
using UI.WinInfo;
using UnityEngine;
using Zenject;

namespace Core.GameControllers
{
    public class MultiGameController : BaseGameController
    {
        private readonly Player firstPlayer;
        private readonly Player secondPlayer;
        private readonly MultiWinInfo winInfo;
        private readonly FullscreenLayer firstFullscreenLayer;
        private readonly FullscreenLayer secondFullscreenLayer;
        private Player winnerPlayer;

        [Inject]
        public MultiGameController(Player firstPlayer, Player secondPlayer, GameObject loadingButtons, 
            AudioManager audioManager, MultiWinInfo winInfo,
            FullscreenLayer firstFullscreenLayer, FullscreenLayer secondFullscreenLayer)
            : base(audioManager, loadingButtons)
        {
            this.firstPlayer = firstPlayer;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
            this.winInfo = winInfo;
            this.firstFullscreenLayer = firstFullscreenLayer;
            this.secondFullscreenLayer = secondFullscreenLayer;
        }

        public override void Initialize()
        {
            base.Initialize();
            firstPlayer.OnEndGame += OnPlayerEndGame;
            secondPlayer.OnEndGame += OnPlayerEndGame;
        }

        public override void Dispose()
        {
            firstPlayer.OnEndGame -= OnPlayerEndGame;
            secondPlayer.OnEndGame -= OnPlayerEndGame;
            base.Dispose();
        }

        private void OnPlayerEndGame(Player player)
        {
            if (winnerPlayer == null)
            {
                winnerPlayer = player;
            }
            else
            {
                winnerPlayer.Win();
                Object.Destroy(firstFullscreenLayer);
                Object.Destroy(secondFullscreenLayer);
                base.ShowLoadingButtons();
                var winnerText = winnerPlayer == firstPlayer ? "Red Team Wins" : "Blue Team Wins";
                winInfo.SetWinInfo(winnerText, firstPlayer.GetPlaytime(), secondPlayer.GetPlaytime(), 
                    firstPlayer.GetMissedAttempts(), secondPlayer.GetMissedAttempts());
            }
        }
    }
}
