using System;
using Framework.Extensions;
using UnityEngine;

namespace Framework.Service_Locator
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour
    {
        private ServiceLocator _container;
        internal ServiceLocator Container => _container.OrNull() ?? (_container = GetComponent<ServiceLocator>());

        private bool hasBeenBootstrapped;

        private void Awake() => BootstrapOnDemand();

        public void BootstrapOnDemand()
        {
            if (hasBeenBootstrapped) return;

            hasBeenBootstrapped = true;

            Bootstrap();
        }

        protected abstract void Bootstrap();
    }

    [AddComponentMenu("ServiceLocator/ServiceLocator Global")]
    public class ServiceLocatorGlobalBootstrapper : Bootstrapper
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        
        protected override void Bootstrap()
        {
            Container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
    
    [AddComponentMenu("ServiceLocator/ServiceLocator Scene")]
    public class ServiceLocatorSceneBootstrapper : Bootstrapper
    {
        protected override void Bootstrap()
        {
            Container.ConfigureForScene();
        }
    }
}
