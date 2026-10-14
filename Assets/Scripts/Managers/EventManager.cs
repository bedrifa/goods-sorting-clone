using System;
using System.Collections.Generic;

public static class EventManager
{
    public static readonly Dictionary<Type, Delegate> _eventListeners = new Dictionary<Type, Delegate>();
    
    public static void AddEventListener<T>(Action<T> listener) where T : struct
    {
        Type eventType = typeof(T);

        if (_eventListeners.TryGetValue(eventType, out Delegate currentDelegate))
        {
            _eventListeners[eventType] = Delegate.Combine(currentDelegate, listener);
        }
        else
        {
            _eventListeners.Add(eventType, listener);
        }
    }

    public static void RemoveEventListener<T>(Action<T> listener) where T : struct
    {
        Type eventType = typeof(T);
        
        if (_eventListeners.TryGetValue(eventType, out Delegate currentDelegate))
        {
            Delegate temp = Delegate.Remove(currentDelegate, listener);

            if (temp == null)
                _eventListeners.Remove(eventType); 
            else
                _eventListeners[eventType] = temp;
        }
    }

    public static void TriggerEvent<T>(T eventData) where T : struct
    {
        Type eventType = typeof(T);

        if(_eventListeners.TryGetValue(eventType, out Delegate currentDelegate))
        {
            Action<T> action = currentDelegate as Action<T>;
            action?.Invoke(eventData);
        }
    }
    public static void Clear()
    {
        _eventListeners.Clear();
    }
}