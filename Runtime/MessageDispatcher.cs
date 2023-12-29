using System;
using System.Collections.Generic;

namespace BlueTrain.MessageDispatching
{
    public static class MessageDispatcher
    {
        private static readonly Dictionary<Type, MessageHandler> Handlers = new Dictionary<Type, MessageHandler>();

        public static void Register<TMessage>(MessageHandler handler) where TMessage : IMessage
        {
            var type = typeof(TMessage);

            if (Handlers.ContainsKey(type))
            {
                Handlers[type] += handler;
            }
            else
            {
                Handlers.Add(type, handler);
            }
        }

        public static void Unregister<TMessage>(MessageHandler handler) where TMessage : IMessage
        {
            var type = typeof(TMessage);

            if (Handlers.ContainsKey(type))
            {
                Handlers[type] -= handler;
            }
        }

        public static void Send<TMessage>(IMessageData messageData = null) where TMessage : IMessage
        {
            Handlers[typeof(TMessage)]?.Invoke(messageData);
        }

        public static void ClearAll()
        {
            Handlers.Clear();
        }
    }
}