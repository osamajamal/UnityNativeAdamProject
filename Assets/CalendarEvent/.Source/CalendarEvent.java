package calendarevent;

import android.app.Activity;
import android.widget.Toast;
import android.content.ContentResolver;
import android.content.ContentValues;
import android.provider.CalendarContract;
import java.util.TimeZone;
import android.net.Uri;

public class CalendarEvent
{
    public static int AddEvent(Activity UnityActivity, String Title, long BeginDate, long EndDate, int IsAllDay)
    {
        BeginDate *= 1000;
        EndDate *= 1000;
        Boolean AllDay = true;
        if (IsAllDay == 0) AllDay = false; 

        ContentValues Values = new ContentValues();

        Values.put(CalendarContract.Events.DTSTART, BeginDate);
        Values.put(CalendarContract.Events.DTEND, EndDate);
        Values.put(CalendarContract.Events.TITLE, Title);
        Values.put(CalendarContract.Events.EVENT_TIMEZONE, TimeZone.getDefault().getID());
        Values.put(CalendarContract.Events.CALENDAR_ID, 1);
        Values.put(CalendarContract.EXTRA_EVENT_ALL_DAY, AllDay);

        Uri Uri = UnityActivity.getContentResolver().insert(CalendarContract.Events.CONTENT_URI, Values);
        int EventId = Integer.parseInt(Uri.getLastPathSegment());

        Toast.makeText(UnityActivity, "Event added.", Toast.LENGTH_LONG).show();

        return EventId;
    }
}