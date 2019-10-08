using System;

public static class TimerExtensions
{
    public static void StopTimer(this System.Threading.Timer timer)
    {
        timer.Change(
            System.Threading.Timeout.Infinite,
            System.Threading.Timeout.Infinite);
    }

    public static void SetNextExecution(this System.Threading.Timer timer, TimeSpan executionTime)
    {
        timer.Change(
            (int)executionTime.TotalMilliseconds,
            System.Threading.Timeout.Infinite);
    }
}