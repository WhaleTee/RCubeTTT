using Common.DragSystem.Component;
using Common.ServiceLocator;
using UnityEngine;

namespace RCubeTTT.Behaviour {
  public class RCubeSideFace : MonoBehaviour, ServiceInstaller {
    private void Awake() {
      Install();
    }

    public void Install() {
      var currentTransform = transform;

      ServiceLocator.For(this)
      .Register<DragComponent>(new AlongAxisDrag(currentTransform.parent.gameObject.GetInstanceID(), currentTransform, Axis.Y, Camera.main));
    }
  }
}