using System;
using System.Collections.Generic;

public class EventBus : IUtility
{
    private Dictionary<string, List<object>> events = new Dictionary<string, List<object>>();


    public void Subscribe<T>(Action<T> callback)
    {
        var key = typeof(T).Name;
 
        if(events.ContainsKey(key))
        {
            events[key].Add(callback);
        }
        else
        {
            events[key] = new List<object>() {callback};
        }   
    }

    public void InvokeEvent<T>(T signal)
    {
        var key = typeof(T).Name;

        if(events.ContainsKey(key))
        {
            foreach(var item in events[key])
            {
                var act = item as Action<T>;
                act?.Invoke(signal);
            }
        }
    }

    public void Unsubscribe<T>(Action<T> callback)
    {
        var key = typeof(T).Name;

        if(events.ContainsKey(key))
        {
            events[key].Remove(callback);
        }
    }
}
