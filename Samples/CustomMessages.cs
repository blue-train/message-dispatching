namespace BlueTrain.MessageDispatching.Samples
{
    public class GameStartedMessage : IMessage { }

    public class ValueChangedMessage : IMessage { }

    public class ValueChangedMessageData : IMessageData
    {
        public int OldValue { get; set; }
        public int NewValue { get; set; }
    }

    public class UsingExamples
    {
        public void RegisterUnregisterExamples()
        {
            MessageDispatcher.Register<GameStartedMessage>(OnGameStarted);
            MessageDispatcher.Register<ValueChangedMessage>(OnValueChanged);

            MessageDispatcher.Unregister<GameStartedMessage>(OnGameStarted);
            MessageDispatcher.Unregister<ValueChangedMessage>(OnValueChanged);
        }

        public void SendMessageExamples()
        {
            MessageDispatcher.Send<GameStartedMessage>();
            MessageDispatcher.Send<ValueChangedMessage>(new ValueChangedMessageData { OldValue = 10, NewValue = 20 });
        }

        private void OnGameStarted(IMessageData messageData)
        {
            // do some stuff
        }

        private void OnValueChanged(IMessageData messageData)
        {
            var data = (ValueChangedMessageData)messageData;
            var values = (data.OldValue, data.NewValue);

            // do some stuff with values
        }
    }
}