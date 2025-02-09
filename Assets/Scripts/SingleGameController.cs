using Music;
using UI.WinInfo;
using TouchScript.Layers;
using UnityEngine;
using Zenject;

public class SingleGameController : BaseGameController
{
    private readonly Player.Player player;
    private readonly SingleWinInfo winInfo;
    private readonly FullscreenLayer fullscreenLayer;

    [Inject]
    public SingleGameController(Player.Player player, GameObject loadingButtons, AudioManager audioManager, 
        SingleWinInfo winInfo, FullscreenLayer fullscreenLayer)
        : base(audioManager, loadingButtons)
    {
        this.player = player;
        this.winInfo = winInfo;
        this.fullscreenLayer = fullscreenLayer;
    }

    public override void Initialize()
    {
        base.Initialize();
        player.OnEndGame += EndGame;
    }

    public override void Dispose()
    {
        player.OnEndGame -= EndGame;
        base.Dispose();
    }

    private void EndGame(Player.Player player)
    {
        player.Win();
        Object.Destroy(fullscreenLayer);
        base.ShowLoadingButtons();
        winInfo.SetPlayTime(player.GetPlaytime());
        winInfo.SetMissedAttempts(player.GetMissedAttempts());
    }
}