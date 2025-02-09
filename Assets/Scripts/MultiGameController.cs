using Music;
using UI.WinInfo;
using TouchScript.Layers;
using UnityEngine;
using Zenject;

public class MultiGameController : BaseGameController
{
    private readonly Player.Player firstPlayer;
    private readonly Player.Player secondPlayer;
    private readonly MultiWinInfo winInfo;
    private readonly FullscreenLayer firstFullscreenLayer;
    private readonly FullscreenLayer secondFullscreenLayer;
    private Player.Player winnerPlayer;

    [Inject]
    public MultiGameController(Player.Player firstPlayer, Player.Player secondPlayer, GameObject loadingButtons, 
        AudioManager audioManager, MultiWinInfo winInfo,
        FullscreenLayer firstFullscreenLayer, FullscreenLayer secondFullscreenLayer)
        : base(audioManager, loadingButtons)
    {
        Debug.Log($"firstPlayer: {firstPlayer}, secondPlayer: {secondPlayer}, audioManager: {audioManager}, winInfo: {winInfo}, loadind buttons: {loadingButtons}" +
                  $"first laer {firstFullscreenLayer} second layer {secondFullscreenLayer}");

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
        Debug.Log("Подписка на события игроков");
    }

    public override void Dispose()
    {
        Debug.Log("Отписка от событий игроков");
        firstPlayer.OnEndGame -= OnPlayerEndGame;
        secondPlayer.OnEndGame -= OnPlayerEndGame;
        base.Dispose();
    }

    private void OnPlayerEndGame(Player.Player player)
    {
        Debug.Log("END GAME CONTROLLER");
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
