using Prism.Events;

namespace Classroom.Events
{

    public enum Target
    {
        LoginView,
        LoginViewModel,
        MainView,
        MainViewModel,
        MeetingView,
        MeetingViewModel,
    }

    public enum Value
    {
        UserName,
        Password,
        MainCard,
        HistoryCard,
    }

    public class EventArgument
    {
        public Target Target { get; set; }
        public Value Value { get; set; }
    }

    public class UIGotFocusEvent : PubSubEvent<EventArgument> { }
    public class WindowCloseEvent : PubSubEvent<EventArgument> { }
    public class WindowShowEvent : PubSubEvent<EventArgument> { }
    public class WindowHideEvent : PubSubEvent<EventArgument> { }
    public class CardSelectedEvent : PubSubEvent<EventArgument> { }










    public class StartClassEvent : PubSubEvent<EventArgument> { }
    public class MicStatusChangeEvent : PubSubEvent<EventArgument> { }
    public class CameraStatusChangeEvent : PubSubEvent<EventArgument> { }
}
