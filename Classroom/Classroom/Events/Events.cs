﻿using Prism.Events;

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

    public class Argument
    {
        public Category Category { get; set; }
        public object Value { get; set; }
    }


    public enum Category
    {
        UserName,
        Password,
        MainCard,
        HistoryCard,
        Mic,
        Speaker,
        Camera,
        RecordStart,
        RecordPause,
        RecordResume,
        RecordStop,
    }

    public class EventArgument
    {
        public Target Target { get; set; }
        public Argument Argument { get; set; }
    }

    public class UIGotFocusEvent : PubSubEvent<EventArgument> { }
    public class WindowCloseEvent : PubSubEvent<EventArgument> { }
    public class WindowShowEvent : PubSubEvent<EventArgument> { }
    public class WindowHideEvent : PubSubEvent<EventArgument> { }
    public class CardSelectedEvent : PubSubEvent<EventArgument> { }

    public class StartClassEvent : PubSubEvent<EventArgument> { }
    public class MicStatusChangeEvent : PubSubEvent<EventArgument> { }
    public class CameraStatusChangeEvent : PubSubEvent<EventArgument> { }


    public class AudioSettingsOpenEvent : PubSubEvent<EventArgument> { }
    public class VideoSettingsOpenEvent : PubSubEvent<EventArgument> { }


    public class SelectedDeviceChangeEvent : PubSubEvent<EventArgument> { }
    public class RecordStatusChangeEvent : PubSubEvent<EventArgument> { }
}
