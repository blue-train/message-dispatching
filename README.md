# Simple message dispatching system for Unity Engine
This is a simple implementation of an [Observer pattern](https://en.wikipedia.org/wiki/Observer_pattern), where you have subscribers that listen to events.  
You can add it as a package to your Unity project using [project URL](https://github.com/blue-train/message-dispatching.git).
## How to use
- Create a custom message and inherit it from MessageBase class: `GameStartedMessage : MessageBase`.
- Register an action to a message: `MessageDispatcher.Subscribe<GameStartedMessage>(OnGameStarted)`.
- Send a message to invoke all related actions: `MessageDispatcher.Send<GameStartedMessage>()`.
- Unregister an action from a message: `MessageDispatcher.Unsubscribe<GameStartedMessage>(OnGameStarted)`.
## Notes
- Requires at least Unity 2019.4.
- Use methods instead of lambdas when calling `Subscribe`/`Unsubscribe`, otherwise, it will not work.
- Although it was made firstly for Unity, you can use it anywhere because the only part related to the game engine is the tests folder.
