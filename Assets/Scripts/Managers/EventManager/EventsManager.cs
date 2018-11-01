using System.Collections.Generic;

public class EventsManager
{
    public delegate void EventReceiver(params object[] parameterContainer);
    private static Dictionary<string, EventReceiver> _events;

    /// <summary>
    /// Inicializa el EventManager
    /// </summary>
    public static void Init()
    {
        SubscribeToEvent(EventType.CLEAR_ALL, Clear);
    }

    /// <summary>
    /// Llamamos a este método para suscribirnos a eventos
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void SubscribeToEvent(string eventType, EventReceiver listener)
    {
        if (_events == null)
            _events = new Dictionary<string, EventReceiver>();

        if (!_events.ContainsKey(eventType))
            _events.Add(eventType, null);

        _events[eventType] += listener;
    }

    /// <summary>
    /// Llamamos a este método para desuscribirnos de eventos
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void UnsubscribeToEvent(string eventType, EventReceiver listener)
    {
        if (_events != null)
        {
            if (_events.ContainsKey(eventType))
                _events[eventType] -= listener;
        }
    }

    /// <summary>
    /// Llamamos a esta función para disparar un evento
    /// </summary>
    /// <param name="eventType"></param>
    public static void TriggerEvent(string eventType)
    {
        TriggerEvent(eventType, null);
    }

    /// <summary>
    /// Dispara el evento
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="parametersWrapper"></param>
    public static void TriggerEvent(string eventType, params object[] parametersWrapper)
    {
        if (_events == null) return;

        if (_events.ContainsKey(eventType))
        {
            if (_events[eventType] != null)
                _events[eventType](parametersWrapper);
        }
    }

    /// <summary>
    /// Limpia el manager de cualquier evento
    /// </summary>
    public static void Clear()
    {
        _events.Clear();
    }

    private static void Clear(object[] parametersContainer)
    {
        Clear();
    }
}