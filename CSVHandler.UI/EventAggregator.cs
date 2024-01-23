namespace CSVHandler.UI
{
    internal class EventAggregator
    {
        private EventAggregator() { }

        private static EventAggregator? _instance;

        public static EventAggregator Instance
        {
            get => _instance ?? (_instance = new EventAggregator());
        }

        public event EventHandler? SaveToDbStarted;
        public event EventHandler? SaveToDbEnded;
        public event EventHandler? ShowFileDataStarted;
        public event EventHandler? ShowFileDataEnded;

        public void RaiseSaveToDbStartedEvent()
        {
            SaveToDbStarted?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseSaveToDbEndedEvent()
        {
            SaveToDbEnded?.Invoke(this, EventArgs.Empty);
        }
        
        public void RaiseShowFileDataStartedEvent()
        {
            ShowFileDataStarted?.Invoke(this, EventArgs.Empty);
        }
        
        public void RaiseShowFileDataEndedEvent()
        {
            ShowFileDataEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
