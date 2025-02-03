using System.Diagnostics;

public class Timer
{
    private readonly Stopwatch stopwatch;

    public Timer()
    {
        stopwatch = new Stopwatch();
    }
    
    public void Start()
    {
        stopwatch.Restart();
    }

    public void Stop()
    {
        stopwatch.Stop();
    }

    public float GetElapsedTime()
    {
        return (float)stopwatch.Elapsed.TotalSeconds;
    }
}