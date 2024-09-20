using UnityEngine;

namespace Common.ServiceLocator {
  [AddComponentMenu("ServiceLocator/ServiceLocator Global")]
  public class ServiceLocatorGlobal : Bootstrapper {
    [SerializeField] private bool destroyOnLoad;

    protected override void Bootstrap() {
      container.ConfigureAsGlobal(destroyOnLoad);
    }
  }
}