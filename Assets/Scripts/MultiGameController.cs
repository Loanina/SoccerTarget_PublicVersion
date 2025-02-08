using System;
using DG.Tweening;
using Music;
using TouchScript.Layers;
using UI;
using UI.WinInfo;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class MultiGameController : IInitializable, IDisposable
{
    private readonly Player.Player firstPlayer;
    private readonly Player.Player secondPlayer;
    private Player.Player winnerPlayer;
    private readonly GameObject loadingButtons;
    private readonly AudioManager audioManager;
    private readonly MultiWinInfo winInfo;
    private readonly FullscreenLayer firstFullscreenLayer;
    private readonly FullscreenLayer secondFullscreenLayer;
    
    [Inject]
    public MultiGameController(Player.Player firstPlayer, Player.Player secondPlayer, GameObject loadingButtons, AudioManager audioManager,
        MultiWinInfo multiWinInfo, FullscreenLayer firstFullscreenLayer, FullscreenLayer secondFullscreenLayer)
    {
        this.firstPlayer = firstPlayer;
        this.secondPlayer = secondPlayer;
        this.loadingButtons = loadingButtons;
        this.audioManager = audioManager;
        winInfo = multiWinInfo;
        this.firstFullscreenLayer = firstFullscreenLayer;
        this.secondFullscreenLayer = secondFullscreenLayer;
    }

    public void Initialize()
    {
        firstPlayer.OnEndGame += OnPlayerEndGame;
        secondPlayer.OnEndGame += OnPlayerEndGame;
        loadingButtons.SetActive(false);
        audioManager.PlayGameMusic();
    }

    public void Dispose()
    {
        audioManager.PlayMenuMusic();
        firstPlayer.OnEndGame -= OnPlayerEndGame;
        secondPlayer.OnEndGame -= OnPlayerEndGame;
    }
    
    private void OnPlayerEndGame(Player.Player player)
    {
        if (winnerPlayer == null) winnerPlayer = player;
        else
        {
            winnerPlayer.Win();
            Object.Destroy(firstFullscreenLayer);
            Object.Destroy(secondFullscreenLayer);
            loadingButtons.SetActive(true);
            var winnerText = "";
            winnerText = winnerPlayer == firstPlayer ? "red team win" : "blue team win";
            winInfo.SetWinInfo(winnerText, firstPlayer.GetPlaytime(), secondPlayer.GetPlaytime(),
                firstPlayer.GetTotalAttempts() - 10, secondPlayer.GetTotalAttempts() - 10);
        }
    }
}