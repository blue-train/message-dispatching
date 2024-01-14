# Message Dispatching
This is a simple implementation of an [Publish-Subscribe pattern](https://en.wikipedia.org/wiki/Publish%E2%80%93subscribe_pattern).  
You register actions to listen to messages and then send these messages.  
  
You can add it as a package to your Unity project using [project URL](https://github.com/blue-train/message-dispatching.git).
## How to use
```CSharp
// Create a message that implements `IMessage`
class ValueChangedMessage : IMessage { }

// If you need, create a message data that implements `IMessageData`
class ValueChangedMessageData : IMessageData { }

// Create a method that takes `IMessageData` as a parameter  
void OnValueChanged(IMessageData messageData) => var data = (ValueChangedMessageData)messageData;

// Register an action to a message 
MessageDispatcher.Register<ValueChangedMessage>(OnValueChanged);

// Send a message to invoke all related actions  
MessageDispatcher.Send<ValueChangedMessage>();

// Unregister an action from a message  
MessageDispatcher.Unregister<ValueChangedMessage>(OnValueChanged);
```
## Notes
- Requires at least Unity 2019.4
- Use methods or local functions instead of lambdas when calling `Register`/`Unregister`, otherwise, it will not work
- Can be used in non-Unity projects, it doesn't depend on any Unity package
