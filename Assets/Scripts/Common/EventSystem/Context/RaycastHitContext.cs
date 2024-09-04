using UnityEngine;

namespace Common.EventSystem.Context {
  public readonly struct RaycastHitContext {
    public RaycastHit hit { get; }

    public RaycastHitContext(RaycastHit hit) => this.hit = hit;
  }
}