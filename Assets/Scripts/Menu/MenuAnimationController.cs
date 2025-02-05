using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

namespace Menu
{
    public class MenuAnimationController : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float moveToMenuTime;
        [SerializeField] private float ballRadius = 0.7f;
        [SerializeField] private List<BallAnimationData> balls;

        private void Start()
        {
            ShowMenu();
        }

        [ContextMenu("Show Menu")]
        private void ShowMenu()
        {
            SetStartPositions();
            MoveBalls();
        }

        private void SetStartPositions()
        {
            foreach (var ball in balls)
            {
                ball.Transform.position = ball.SpawnPoint;
            }
        }

        private void MoveBalls()
        {
            foreach (var ball in balls)
            {
                MoveBallWithRotation(ball.Transform, ball.SpawnPoint, ball.MenuPoint, moveToMenuTime, ball.RotationMultiplier);
            }
        }

        private void MoveBallWithRotation(Transform ball, Vector3 startPoint, Vector3 endPoint, float movingTime, int rotationMultiplier)
        {
            var distance = Vector3.Distance(startPoint, endPoint);
            var rotationAngle = (distance / (2 * Mathf.PI * ballRadius)) * -360f;
            ball.DOMove(endPoint, movingTime).SetEase(Ease.InOutQuad);
            ball.DORotate(new Vector3(0, 0, rotationAngle * rotationMultiplier), movingTime, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
        }
    }

    [System.Serializable]
    public class BallAnimationData
    {
        public Transform Transform;
        public Vector3 SpawnPoint;
        public Vector3 MenuPoint;
        public int RotationMultiplier;
    }
}