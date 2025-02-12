﻿using System;
using Core.Interfaces;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Gameplay.Ball
{
    public class BallSpawner : IInitializable, IDisposable
    {
        private readonly Transform ballsParent;
        private readonly IBallFactory ballFactory;
        private readonly IBallDestroyer ballDestroyer;
        private GameObject ballsContainer;

        [Inject]
        public BallSpawner(IBallFactory ballFactory, IBallDestroyer ballDestroyer, Transform ballsParent)
        {
            this.ballFactory = ballFactory;
            this.ballDestroyer = ballDestroyer;
            this.ballsParent = ballsParent;
        }

        public void Initialize()
        {
            ballsContainer = new GameObject("Balls");
            ballsContainer.transform.SetParent(ballsParent, false);
        }

        public void Dispose()
        {
            if (ballsContainer != null)
                Object.Destroy(ballsContainer);
        }

        public GameObject Spawn()
        {
            var ball = ballFactory.CreateBall(ballsContainer.transform);
            _ = ballDestroyer.DestroyWithEffect(ball, ballsContainer.transform);
            
            return ball;
        }
    }
}
