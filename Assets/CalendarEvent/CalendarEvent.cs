using System;
using UnityEngine;

public static class CalendarEvent
{
    static readonly string PlatformNotSupportedMessage = $"This platform isn't supported by {nameof(CalendarEvent)}";

#if UNITY_ANDROID
    static readonly AndroidJavaObject AndroidActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    static readonly AndroidJavaClass CalendarEventJavaClass = new AndroidJavaClass("calendarevent.CalendarEvent");
#endif

#if UNITY_IOS
    [System.Runtime.InteropServices.DllImport("__Internal")]
    static extern void AddEvent_Native(string ListenerName, string Title, double BeginDate, double EndDate, bool IsAllDay);
#endif

    internal static void AddEvent(string Title, DateTime BeginDate, DateTime EndDate, bool IsAllDay, DoneEventHandler Done = null)
    {
        if (Application.isEditor) throw new PlatformNotSupportedException(PlatformNotSupportedMessage);

        var UnixBeginDate = ((DateTimeOffset)BeginDate).ToUnixTimeSeconds();
        var UnixEndDate = ((DateTimeOffset)EndDate).ToUnixTimeSeconds();

#if UNITY_IOS
        AddEvent_Native(CalendarEventIOSListener.Create(Done), Title, UnixBeginDate, UnixEndDate, IsAllDay);
#elif UNITY_ANDROID
        var EventId = CalendarEventJavaClass.CallStatic<int>("AddEvent", AndroidActivity, Title, UnixBeginDate, UnixEndDate, IsAllDay ? 1 : 0);
        Done?.Invoke(EventId);
#else
        throw new PlatformNotSupportedException(PlatformNotSupportedMessage);
#endif
    }

    internal delegate void DoneEventHandler(int EventId);
}