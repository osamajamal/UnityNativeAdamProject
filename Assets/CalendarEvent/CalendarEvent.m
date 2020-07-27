#import "CalendarEvent.h"

void AddEvent_Native(char* ListenerName, char* Title, double BeginDate, double EndDate, bool IsAllDay)
{
    NSString* ListenerNameNS = [NSString stringWithUTF8String: ListenerName];

    EKEventStore* Store = [EKEventStore new];

    EKEvent* Event = [EKEvent eventWithEventStore: Store];
    Event.calendar = [Store defaultCalendarForNewEvents];
    Event.title = [NSString stringWithUTF8String: Title];
    Event.startDate = [NSDate dateWithTimeIntervalSince1970: BeginDate];
    Event.endDate = [NSDate dateWithTimeIntervalSince1970: EndDate];
    Event.allDay = IsAllDay;

    [Store requestAccessToEntityType: EKEntityTypeEvent completion: ^(BOOL Granted, NSError* Error)
    {
        int EventId = 0;

        if (Granted)
        {
            [Store saveEvent: Event span: EKSpanThisEvent commit: YES error: nil];
            EventId = Event.eventIdentifier;
        }

        UnitySendMessage([ListenerNameNS UTF8String], "CallDoneAndDestroy", [[NSString stringWithFormat:@"%d", EventId] UTF8String]);
    }];
}