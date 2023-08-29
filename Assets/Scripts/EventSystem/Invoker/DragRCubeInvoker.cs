using UnityEngine.Events;

public interface DragRCubeInvoker : Invoker {
  #region methods

  public RCubeDragEvent GetInputEvent();

  #endregion

  #region Invoker methods

  void Invoker.AddListener(UnityAction listener) => GetInputEvent().AddListener(listener);

  void Invoker.Invoke() => GetInputEvent().Invoke();

  #endregion
}