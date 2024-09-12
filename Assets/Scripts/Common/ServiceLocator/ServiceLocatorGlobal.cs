using UnityEngine;

namespace Common.ServiceLocator {
    [AddComponentMenu("ServiceLocator/ServiceLocator Global")]
    public class ServiceLocatorGlobal : Bootstrapper {
        [SerializeField] bool dontDestroyOnLoad = true;
        
        protected override void Bootstrap() {
            container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
}