using UnityEngine.Events;

public interface EndDragRCubeInvoker : Invoker {
  #region methods

  public EndDragRCubeEvent GetInputEvent();

  #endregion

  #region Invoker methods

  void Invoker.AddListener(UnityAction listener) => GetInputEvent().AddListener(listener);

  void Invoker.Invoke() {
    GetInputEvent().Invoke();
  }

  #endregion
}