using UnityEngine;

namespace Common.ServiceLocator {
  [DisallowMultipleComponent]
  [RequireComponent(typeof(ServiceLocator))]
  public abstract class Bootstrapper : MonoBehaviour {
    private ServiceLocator locator;
    internal ServiceLocator container => locator ? locator : locator = GetComponent<ServiceLocator>();

    private bool hasBeenBootstrapped;

    private void Awake() => BootstrapOnDemand();

    public void BootstrapOnDemand() {
      if (hasBeenBootstrapped) return;

      hasBeenBootstrapped = true;
      Bootstrap();
    }

    protected abstract void Bootstrap();
  }
}