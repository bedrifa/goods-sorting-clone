using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EventType
{
    OnItemDropped,
    OnItemMatched
}

public class EventManager : Singleton<EventManager>
{
    public override bool Persist => false;

    private Dictionary<EventType, List<IEventListener>> _eventListeners;

    protected override void Awake()
    {
        base.Awake();
        _eventListeners = new Dictionary<EventType, List<IEventListener>>();
    }

    public void AddEventListener(EventType eventType, IEventListener listener)
    {
        if (_eventListeners.ContainsKey(eventType))
        {
            if (!_eventListeners[eventType].Contains(listener))
                _eventListeners[eventType].Add(listener);
            else
                Debug.LogWarning($"{listener.GetType()} event listener already exist for {eventType} event");
        }
        else
        {
            _eventListeners.Add(eventType, new List<IEventListener>() { listener });
        }
    }

    public void RemoveEventListener(EventType eventType, IEventListener listener)
    {
        if (!_eventListeners.ContainsKey(eventType))
        {
            Debug.LogError($"{eventType} doesn't exist");
            return;
        }

        if (!_eventListeners[eventType].Contains(listener))
        {
            Debug.LogWarning($"{listener.GetType()} event listener doesn't exist for {eventType} event");
            return;
        }

        _eventListeners[eventType].Remove(listener);

        if (_eventListeners[eventType].Count == 0)
            _eventListeners.Remove(eventType);
    }

    public void TriggerEvent(EventType eventType, object eventData)
    {
        if (!_eventListeners.ContainsKey(eventType)) return;

        List<IEventListener> listeners = _eventListeners[eventType];
        foreach (IEventListener listener in listeners)
        {
            listener.OnEvent(eventType, eventData);
        }
    }
}