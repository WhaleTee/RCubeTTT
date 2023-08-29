using UnityEngine.Events;

public interface StartDragRCubeFaceInvoker : Invoker {
  #region methods

  public StartRCubeDragEvent GetInputEvent();

  #endregion

  #region Invoker methods

  void Invoker.AddListener(UnityAction listener) => GetInputEvent().AddListener(listener);

  void Invoker.Invoke() => GetInputEvent().Invoke();

  #endregion
}