using DG.Tweening;
using Music;
using TouchScript.Gestures;
using UnityEngine;
using Zenject;

namespace Menu
{
    public class BallSceneLoader : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private PressGesture pressGesture;
        [SerializeField] private Transform ball;
        [SerializeField, Range(0, 100)] private float forceMultiplier = 10f;
        [SerializeField] private float activationTime = 0.5f;
        [SerializeField] private float rotationSpeed = 360f;
        [SerializeField] private float startDelayForKick = 1.5f;
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private Rigidbody ballRigidbody;
        private AudioManager audioManager;

        private bool canChooseScene = false;
    
        [Inject]
        public void Construct(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        private void LoadScene()
        {
            DOTween.KillAll();
            SceneLoader.LoadSceneByName(sceneName);
        }

        private void OnEnable() => pressGesture.Pressed += OnPressed;
        private void OnDisable() => pressGesture.Pressed -= OnPressed;

        private void Start()
        {
            ballRigidbody.isKinematic = true;
            DOVirtual.DelayedCall(startDelayForKick, () => canChooseScene = true);
        }

        private void OnPressed(object sender, System.EventArgs e)
        {
            if (!canChooseScene) return;
            canChooseScene = false;
            audioManager.PlayBallThrow();
            LaunchBall();
            DOVirtual.DelayedCall(activationTime + 1f, LoadScene);
        }
        
        private void LaunchBall()
        {
            ballRigidbody.isKinematic = false;
            var direction = (targetPosition - ball.position).normalized;
            var force = direction * forceMultiplier;
            ballRigidbody.AddTorque(new Vector3(rotationSpeed, 0, 0), ForceMode.VelocityChange);
            ballRigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
