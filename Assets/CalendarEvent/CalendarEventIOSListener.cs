using System;
using UnityEngine;

class CalendarEventIOSListener : MonoBehaviour
{
    static int LastListenerId;

    CalendarEvent.DoneEventHandler Done;

    internal static string Create(CalendarEvent.DoneEventHandler Done)
    {
        var ListenerName = $"{nameof(CalendarEvent)}Listener{LastListenerId++}";
        var ListenerObject = new GameObject(ListenerName);
        DontDestroyOnLoad(ListenerObject);

        var Listener = ListenerObject.AddComponent<CalendarEventIOSListener>();
        Listener.Done = Done;

        return ListenerName;
    }

    // ReSharper disable once UnusedMember.Local
    void CallDoneAndDestroy(string EventId)
    {
        Done?.Invoke(Convert.ToInt32(EventId));
        Destroy(gameObject);
    }
}