using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.Events
{
    public enum Target
    {
        LoginView,
        MainView,
    }

    public enum Value
    {
        UserName,
        Password,

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
}
