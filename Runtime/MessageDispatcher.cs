using System;
using System.Collections.Generic;

namespace BlueTrain.MessageDispatching
{
    public static class MessageDispatcher
    {
        private static readonly Dictionary<Type, MessageHandler> MessageHandlers = new Dictionary<Type, MessageHandler>();

        public static void Subscribe<TMessage>(MessageHandler handler) where TMessage : MessageBase
        {
            var type = typeof(TMessage);

            if (MessageHandlers.ContainsKey(type))
            {
                MessageHandlers[type] += handler;
            }
            else
            {
                MessageHandlers.Add(type, handler);
            }
        }

        public static void Unsubscribe<TMessage>(MessageHandler handler) where TMessage : MessageBase
        {
            var type = typeof(TMessage);

            if (MessageHandlers.ContainsKey(type))
            {
                MessageHandlers[type] -= handler;
            }
        }

        public static void Send<TMessage>() where TMessage : MessageBase
        {
            MessageHandlers[typeof(TMessage)]?.Invoke();
        }
    }
}