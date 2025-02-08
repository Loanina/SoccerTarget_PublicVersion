using System;
using Music;
using TouchScript.Layers;
using UI;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class SingleGameController  : IInitializable, IDisposable
{
    private readonly Player.Player player;
    private readonly GameObject loadingButtons;
    private readonly AudioManager audioManager;
    private readonly SingleWinInfo winInfo;
    private readonly FullscreenLayer fullscreenLayer;

    [Inject]
    public SingleGameController(Player.Player player, GameObject loadingButtons, AudioManager audioManager, SingleWinInfo winInfo, FullscreenLayer fullscreenLayer)
    {
        this.player = player;
        this.loadingButtons = loadingButtons;
        this.audioManager = audioManager;
        this.winInfo = winInfo;
        this.fullscreenLayer = fullscreenLayer;
    }

    public void Initialize()
    {
        player.OnEndGame += Win;
        loadingButtons.SetActive(false);
        audioManager.PlayGameMusic();
    }

    public void Dispose()
    {
        player.OnEndGame -= Win;
        audioManager.PlayMenuMusic();
    }
    
    private void Win(Player.Player player)
    {
        player.Win();
        Object.Destroy(fullscreenLayer);
        loadingButtons.SetActive(true);
        winInfo.SetPlayTime(player.GetPlaytime());
        winInfo.SetMissedAttempts(player.GetTotalAttempts() - 10);
    }
}