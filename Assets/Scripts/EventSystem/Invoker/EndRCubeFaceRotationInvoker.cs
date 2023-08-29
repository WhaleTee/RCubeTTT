using UnityEngine.Events;

public interface EndRCubeFaceRotationInvoker : Invoker<string> {
  #region methods

  public EndRCubeFaceRotationEvent GetInputEvent();

  #endregion

  #region Invoker methods

  void Invoker<string>.AddListener(UnityAction<string> listener) => GetInputEvent().AddListener(listener);

  void Invoker<string>.Invoke(string faceGlobalId) => GetInputEvent().Invoke(faceGlobalId);

  #endregion
}