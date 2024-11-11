using UnityEngine;
using System.Collections.Generic;
using Utilities;

public class GlobalTimer : Singleton<GlobalTimer>
{
    private List<Timer> timers = new List<Timer>();

    public CountdownTimer GetCountdownTimer()
    {
        var timer = new CountdownTimer();
        timers.Add(timer);
        return timer;
    }

    private void Update()
    {
        foreach (var timer in timers)
        {
            if (timer.IsRunning)
                timer.Tick(Time.deltaTime);
        }
    }
}
