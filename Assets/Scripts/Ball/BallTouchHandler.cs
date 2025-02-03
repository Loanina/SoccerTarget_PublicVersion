﻿using TouchScript.Gestures;
using UnityEngine;
using Zenject;

namespace Ball
{
    public class BallTouchHandler : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private PressGesture pressGesture;
        [SerializeField, Range(0, 3f)] private float freezeTime;
        private bool isFreezing;
        private BallThrower ballThrower;
        private Player.Player player;
    
        [Inject]
        public void Construct(BallThrower ballThrower, Player.Player player)
        {
            this.ballThrower = ballThrower;
            this.player = player;
        }

        private void Awake()
        {
            pressGesture.Pressed += OnTapped;
        }
        
        private void OnDestroy()
        {
            pressGesture.Pressed -= OnTapped;
        }

        private void OnTapped(object sender, System.EventArgs e)
        {
            if (!player.CanShoot || isFreezing) return;
            var ray = camera.ScreenPointToRay(pressGesture.ScreenPosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            ballThrower.Throw(hit.point);
            player.IncreaseTotalAttempts();
            Freeze();
        }

        private void Freeze()
        {
            isFreezing = true;
            Invoke(nameof(ResetTouch), freezeTime);
        }

        private void ResetTouch()
        {
            isFreezing = false;
        }
    }
}