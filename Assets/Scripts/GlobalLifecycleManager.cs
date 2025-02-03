using System;
using System.Collections.Generic;
using System.Threading;
using Music;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GlobalLifecycleManager : IInitializable, IDisposable
{
    private readonly List<CancellationTokenSource> taskTokens = new();
    private readonly AudioManager audioManager;

    [Inject]
    public GlobalLifecycleManager(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }

    public void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Application.quitting += OnApplicationQuit;
    }

    public void Dispose()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Application.quitting -= OnApplicationQuit;
        CancelAllTasks();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CancelAllTasks();
        audioManager.StopGameSounds();
    }

    private void OnApplicationQuit()
    {
        CancelAllTasks();
        audioManager.StopAll();
    }

    public CancellationToken RegisterTask()
    {
        var cts = new CancellationTokenSource();
        taskTokens.Add(cts);
        return cts.Token;
    }

    private void CancelAllTasks()
    {
        foreach (var cts in taskTokens)
        {
            cts.Cancel();
            cts.Dispose();
        }
        taskTokens.Clear();
    }
}