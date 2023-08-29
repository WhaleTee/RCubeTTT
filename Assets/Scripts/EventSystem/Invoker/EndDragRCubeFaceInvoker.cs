using UnityEngine.Events;

public interface EndDragRCubeFaceInvoker : Invoker {
  #region methods

  public EndRCubeDragEvent GetInputEvent();

  #endregion

  #region Invoker methods

  void Invoker.AddListener(UnityAction listener) => GetInputEvent().AddListener(listener);

  void Invoker.Invoke() => GetInputEvent().Invoke();

  #endregion
}