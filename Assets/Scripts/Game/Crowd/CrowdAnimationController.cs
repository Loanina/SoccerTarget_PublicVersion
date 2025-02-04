using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Crowd
{
    public class CrowdAnimationController : IInitializable, IDisposable
    {
        private readonly List<Animator> crowdAnimators;
        private static readonly int IsCheering = Animator.StringToHash("IsCheer");
        private CancellationTokenSource cts;

        [Inject]
        public CrowdAnimationController(List<Animator> crowdAnimators)
        {
            this.crowdAnimators = crowdAnimators;
        }

        public void Initialize()
        {
          cts = new CancellationTokenSource();
            _ = SittingAsync(cts.Token);
        }

        public void Dispose()
        {
            StopAllTasks();
            cts?.Dispose();
        }

        public void PlayCheer(float duration)
        {
            StopAllTasks();
            _ = CheerAsync(duration, cts.Token);
        }

        public void PlayInfiniteCheer()
        {
            StopAllTasks();
            foreach (var animator in crowdAnimators)
            {
                animator.SetBool(IsCheering, true);
            }
        }

        private void StopAllTasks()
        {
            cts?.Cancel();
            cts?.Dispose();
            cts = new CancellationTokenSource();
        }

        private async Task SittingAsync(CancellationToken token)
        {
            var delays = new float[crowdAnimators.Count];
            for (var i = 0; i < delays.Length; i++)
            {
                delays[i] = Random.Range(0f, 0.1f);
            }

            for (var i = 0; i < crowdAnimators.Count; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(delays[i]), token);
                if (token.IsCancellationRequested) return;
                crowdAnimators[i].SetTrigger("Sitting");
            }
        }

        private async Task CheerAsync(float duration, CancellationToken token)
        {
            var delays = new float[crowdAnimators.Count];
            for (var i = 0; i < delays.Length; i++)
            {
                delays[i] = Random.Range(0.001f, 0.05f);
            }

            for (var i = 0; i < crowdAnimators.Count; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(delays[i]), token);
                if (token.IsCancellationRequested) return;
                crowdAnimators[i].SetBool(IsCheering, true);
            }

            await Task.Delay(TimeSpan.FromSeconds(duration), token);
            if (token.IsCancellationRequested) return;

            for (var i = 0; i < crowdAnimators.Count; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(delays[i]), token);
                if (token.IsCancellationRequested) return;
                crowdAnimators[i].SetBool(IsCheering, false);
            }
        }
    }
}
