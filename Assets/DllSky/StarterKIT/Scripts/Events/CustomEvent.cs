namespace DllSky.StarterKITv2.Events
{
    public delegate void CustomEventHandler(CustomEvent _event);


    public class CustomEvent
    {
        public string EventType
        {
            get;
            private set;
        }

        public object EventData
        {
            get;
            private set;
        }

        public CustomEvent(string type, object data = null)
        {
            EventType = type;
            EventData = data;
        }
    }
}