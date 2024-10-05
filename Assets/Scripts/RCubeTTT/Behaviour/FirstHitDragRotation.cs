using Common.DragSystem.Component;
using Common.DragSystem.Component.Rotation;
using Common.ServiceLocator;
using UnityEngine;

namespace RCubeTTT.Behaviour {
  public class FirstHitDragRotation : MonoBehaviour, ServiceInstaller {
    [SerializeField] private GameObject target;
    [SerializeField] private Transform relative;
    [SerializeField] [Range(0, 50)] private float speed = 15f;

    private void Awake() {
      Install();
    }

    public void Install() {
      ServiceLocator.For(this).Register<DragComponent>(new FirstHitTargetDrag(target.GetInstanceID()));
      ServiceLocator.For(this).Register(new YDragRotation(target.transform, relative, speed, target.GetInstanceID()));
      ServiceLocator.For(this).Register(new XDragRotation(target.transform, relative, speed, target.GetInstanceID()));
    }
  }
}