using UnityEngine.Events;
 
/// <summary>
/// Interface for invoking <see cref="UserTurnStartEvent"/>.
/// </summary>
public interface UserTurnStartEventInvoker : Invoker, EventProvider<UserTurnStartEvent> {
 /// <summary>
 /// Adds a listener to the <see cref="UserTurnStartEvent"/>.
 /// </summary>
 /// <param name="listener">The listener to add.</param>
 void Invoker.AddListener(UnityAction listener) => GetEvent().AddListener(listener);

 /// <summary>
 /// Invokes the <see cref="UserTurnStartEvent"/>.
 /// </summary>
 void Invoker.Invoke() => GetEvent().Invoke();
}