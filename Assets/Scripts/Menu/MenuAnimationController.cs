using DG.Tweening;
using UnityEngine;

namespace Menu
{
    public class MenuAnimationController : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float moveToMenuTime;
        [SerializeField] private Transform singleBall;
        [SerializeField] private Vector3 singleBallSpawnPoint;
        [SerializeField] private Vector3 singleBallMenuPoint;
        [SerializeField] private Transform multiBall;
        [SerializeField] private Vector3 multiBallSpawnPoint;
        [SerializeField] private Vector3 multiBallMenuPoint;
        [SerializeField] private float ballRadius = 0.7f;
        
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
            singleBall.position = singleBallSpawnPoint;
            multiBall.position = multiBallSpawnPoint;
        }
        
        private void MoveBallWithRotation(Transform ball, Vector3 startPoint, Vector3 endPoint, float movingTime, int rotationMultiplier)
        {
            var distance = Vector3.Distance(startPoint, endPoint);
            var rotationAngle = (distance / (2 * Mathf.PI * ballRadius)) * -360f;
            ball.DOMove(endPoint, movingTime)
                .SetEase(Ease.InOutQuad);
            ball.DORotate(new Vector3(0, 0, rotationAngle * rotationMultiplier), movingTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutQuad);
        }
        
        private void MoveBalls()
        {
            MoveBallWithRotation(singleBall, singleBallSpawnPoint, singleBallMenuPoint, moveToMenuTime, -1);
            MoveBallWithRotation(multiBall, multiBallSpawnPoint, multiBallMenuPoint, moveToMenuTime, 1);
        }
    }
}
