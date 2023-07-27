using UnityEngine.Events;

public interface DragRCubeFaceInvoker : Invoker {
  #region methods

  public DragRCubeEvent GetInputEvent();

  #endregion

  #region Invoker methods

  void Invoker.AddListener(UnityAction listener) => GetInputEvent().AddListener(listener);

  void Invoker.Invoke() {
    GetInputEvent().Invoke();
  }

  #endregion
}