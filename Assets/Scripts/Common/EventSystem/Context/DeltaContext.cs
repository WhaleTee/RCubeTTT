using UnityEngine;

namespace Common.EventSystem.Context {
  public readonly struct DeltaContext {
    public Vector3 delta { get; }

    public DeltaContext(Vector3 delta) {
      this.delta = delta;
    }
  }
}