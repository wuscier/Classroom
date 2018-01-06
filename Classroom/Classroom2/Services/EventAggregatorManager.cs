using Prism.Events;

namespace Classroom.Services
{
    public class EventAggregatorManager
    {
        private EventAggregatorManager() {
            EventAggregator = new EventAggregator();
        }
        public static readonly EventAggregatorManager Instance = new EventAggregatorManager();

        public EventAggregator EventAggregator { get; set; }
    }
}
