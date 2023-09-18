using UnityEngine.Events;
 
/// <summary>
/// Interface for invoking <see cref="PlayerTurnStartEvent"/>.
/// </summary>
public interface PlayerTurnStartEventInvoker : Invoker<PlayerPlayData>, EventProvider<PlayerTurnStartEvent> {
 /// <summary>
 /// Adds a listener to the <see cref="PlayerTurnStartEvent"/>.
 /// </summary>
 /// <param name="listener">The listener to add.</param>
 void Invoker<PlayerPlayData>.AddListener(UnityAction<PlayerPlayData> listener) => GetEvent().AddListener(listener);

 /// <summary>
 /// Invokes the <see cref="PlayerTurnStartEvent"/>.
 /// </summary>
 /// <param name="context">The player turn callback context.</param>
 void Invoker<PlayerPlayData>.Invoke(PlayerPlayData context) => GetEvent().Invoke(context);
}