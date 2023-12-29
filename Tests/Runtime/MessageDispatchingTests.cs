using NUnit.Framework;

namespace BlueTrain.MessageDispatching.Tests
{
    public class MessageDispatchingTests
    {
        private class IncreasingValueRequestedMessage : IMessage { };

        private class IncreasingValueRequestedMessageData : IMessageData
        {
            public int IncreaseValueBy { get; set; }
        };

        private int _valueA;
        private int _valueB;

        [Test]
        public void RegisterUnregisterTwoHandlers()
        {
            _valueA = 0;
            _valueB = 0;

            MessageDispatcher.Register<IncreasingValueRequestedMessage>(IncreaseValueA);
            MessageDispatcher.Register<IncreasingValueRequestedMessage>(IncreaseValueB);
            MessageDispatcher.Send<IncreasingValueRequestedMessage>(); // a == 1, b == 1

            MessageDispatcher.Unregister<IncreasingValueRequestedMessage>(IncreaseValueA);
            MessageDispatcher.Send<IncreasingValueRequestedMessage>(); // a == 1, b == 2

            MessageDispatcher.Register<IncreasingValueRequestedMessage>(IncreaseValueA);
            MessageDispatcher.Unregister<IncreasingValueRequestedMessage>(IncreaseValueB);
            MessageDispatcher.Send<IncreasingValueRequestedMessage>(); // a == 2, b == 2

            MessageDispatcher.ClearAll();

            Assert.That(_valueA == 2 && _valueB == 2);
        }

        [Test]
        public void SendDataToTwoHandlers()
        {
            _valueA = 0;
            _valueB = 0;

            MessageDispatcher.Register<IncreasingValueRequestedMessage>(IncreaseValueAFromData);
            MessageDispatcher.Register<IncreasingValueRequestedMessage>(IncreaseValueBFromData);
            MessageDispatcher.Send<IncreasingValueRequestedMessage>(
                new IncreasingValueRequestedMessageData { IncreaseValueBy = 10 }); // a == 10, b == 10

            MessageDispatcher.Unregister<IncreasingValueRequestedMessage>(IncreaseValueAFromData);
            MessageDispatcher.Send<IncreasingValueRequestedMessage>(
                new IncreasingValueRequestedMessageData { IncreaseValueBy = 10 }); // a == 10, b == 20

            MessageDispatcher.Register<IncreasingValueRequestedMessage>(IncreaseValueAFromData);
            MessageDispatcher.Unregister<IncreasingValueRequestedMessage>(IncreaseValueBFromData);
            MessageDispatcher.Send<IncreasingValueRequestedMessage>(
                new IncreasingValueRequestedMessageData { IncreaseValueBy = 10 }); // a == 20, b == 20

            MessageDispatcher.ClearAll();

            Assert.That(_valueA == 20 && _valueB == 20);
        }

        [Test]
        public void UnsubscribeOnInvoke()
        {
            _valueA = 0;

            MessageDispatcher.Register<IncreasingValueRequestedMessage>(IncreaseValueAOnce);
            MessageDispatcher.Send<IncreasingValueRequestedMessage>();
            MessageDispatcher.Send<IncreasingValueRequestedMessage>();

            Assert.That(_valueA == 1);
        }

        private void IncreaseValueA(IMessageData messageData)
        {
            _valueA++;
        }

        private void IncreaseValueB(IMessageData messageData)
        {
            _valueB++;
        }

        private void IncreaseValueAFromData(IMessageData messageData)
        {
            var data = (IncreasingValueRequestedMessageData)messageData;
            _valueA += data.IncreaseValueBy;
        }

        private void IncreaseValueBFromData(IMessageData messageData)
        {
            var data = (IncreasingValueRequestedMessageData)messageData;
            _valueB += data.IncreaseValueBy;
        }

        private void IncreaseValueAOnce(IMessageData messageData)
        {
            _valueA++;

            MessageDispatcher.Unregister<IncreasingValueRequestedMessage>(IncreaseValueAOnce);
        }
    }
}