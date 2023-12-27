using NUnit.Framework;

namespace BlueTrain.MessageDispatching.Tests
{
    public class MessageDispatchingTests
    {
        private int _valueA;
        private int _valueB;

        [Test]
        public void SubscribingAndUnsubscribingWorksWith2Handlers()
        {
            MessageDispatcher.Subscribe<MessageBase>(IncreaseValueA);
            MessageDispatcher.Subscribe<MessageBase>(IncreaseValueB);
            MessageDispatcher.Send<MessageBase>(); // a == 1, b == 1

            MessageDispatcher.Unsubscribe<MessageBase>(IncreaseValueA);
            MessageDispatcher.Send<MessageBase>(); // a == 1, b == 2

            MessageDispatcher.Subscribe<MessageBase>(IncreaseValueA);
            MessageDispatcher.Unsubscribe<MessageBase>(IncreaseValueB);
            MessageDispatcher.Send<MessageBase>(); // a == 2, b == 2

            Assert.That(_valueA == 2 && _valueB == 2);
        }

        private void IncreaseValueA()
        {
            _valueA++;
        }

        private void IncreaseValueB()
        {
            _valueB++;
        }
    }
}