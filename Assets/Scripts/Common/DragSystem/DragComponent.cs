using UnityEngine;

namespace Common.DragSystem {
  public abstract class DragComponent : MonoBehaviour {
    public virtual bool IsDragAllowed() => true;
  }
}