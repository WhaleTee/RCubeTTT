using UnityEngine;

namespace Common.EventSystem.Context {
  public readonly struct PositionContext {
    public Vector3 screenPosition { get; }

    public PositionContext(Vector3 screenPosition) {
      this.screenPosition = screenPosition;
    }
  }
}