using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.Service_Locator
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _global;
        private static Dictionary<Scene, ServiceLocator> _sceneContainers = new Dictionary<Scene, ServiceLocator>();

        private readonly ServiceManager _serviceManager = new ServiceManager();

        private const string KGlobalServiceLocatorName = "ServiceLocator [Global]";
        private const string KSceneServiceLocatorName = "ServiceLocator [Scene]";
        
        private static List<GameObject> _tmpSceneGameObjects;

        internal void ConfigureAsGlobal(bool dontDestroyOnLoad)
        {
            if (_global == this)
            {
                Debug.LogWarning("ServiceLocator.ConfigureAsGlobal: Already configured as global", this);
            }
            else if(_global != null)
            {
                Debug.LogError("ServiceLocator.ConfigureAsGlobal: Another ServiceLocator is already configured as global", this);
            }
            else
            {
                _global = this;
                
                if(dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
        }

        internal void ConfigureForScene()
        {
            var scene = gameObject.scene;

            if (_sceneContainers.ContainsKey(scene))
            {
                Debug.LogError("ServiceLocator.ConfigureForScene: Another ServiceLocator is already configured for this scene", this);
                return;
            }
            
            _sceneContainers.Add(scene, this);
        }

        public static ServiceLocator Global
        {
            get
            {
                if (_global != null) return _global;

                if (FindFirstObjectByType<ServiceLocatorGlobalBootstrapper>() is { } found)
                {
                    found.BootstrapOnDemand();

                    return _global;
                }

                var container = new GameObject(KGlobalServiceLocatorName, typeof(ServiceLocator));
                container.AddComponent<ServiceLocatorGlobalBootstrapper>().BootstrapOnDemand();

                return _global;
            }
        }

        public static ServiceLocator For(MonoBehaviour mb)
        {
            return mb.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(mb) ?? Global;
        }

        public static ServiceLocator ForSceneOf(MonoBehaviour mb)
        {
            var scene = mb.gameObject.scene;

            if (_sceneContainers.TryGetValue(scene, out var container) && container != mb)
            {
                return container;
            }
            
            _tmpSceneGameObjects.Clear();

            scene.GetRootGameObjects(_tmpSceneGameObjects);

            foreach (var go in _tmpSceneGameObjects.Where(go => go.GetComponent<ServiceLocatorSceneBootstrapper>() != null))
            {
                if (go.TryGetComponent(out ServiceLocatorSceneBootstrapper bootstrapper) &&
                    bootstrapper.Container != mb)
                {
                    bootstrapper.BootstrapOnDemand();

                    return bootstrapper.Container;
                }
            }

            return Global;
        }

        public ServiceLocator Register<T>(T service)
        {
            _serviceManager.Register(service);
            return this;
        }
        
        public ServiceLocator Register(Type type, object service)
        {
            _serviceManager.Register(type, service);
            return this;
        }

        public ServiceLocator Get<T>(out T service) where T : class
        {
            if (TryGetService(out service)) return this;

            if (TryGetNextInHierarchy(out ServiceLocator container))
            {
                container.Get<T>(out service);
                return this;
            }

            throw new ArgumentException($"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered");
        }

        private bool TryGetService<T>(out T service) where T : class
        {
            return _serviceManager.TryGet(out service);
        }

        private bool TryGetNextInHierarchy(out ServiceLocator container)
        {
            if (this == _global)
            {
                container = null;
                return false;
            }

            container = transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(this);
            return container != null;
        }
        
        private void OnDestroy()
        {
            if (this == _global)
            {
                _global = null;
            }else if (_sceneContainers.ContainsValue(this))
            {
                _sceneContainers.Remove(gameObject.scene);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _global = null;
            _sceneContainers = new Dictionary<Scene, ServiceLocator>();

            _tmpSceneGameObjects = new List<GameObject>();
        }
        
        #if UNITY_EDITOR
        [MenuItem("GameObject/ServiceLocator Add Global")]
        private static void AddGlobal()
        {
            var go = new GameObject(KGlobalServiceLocatorName, typeof(ServiceLocatorGlobalBootstrapper));
        }
        
        [MenuItem("GameObject/ServiceLocator Add Scene")]
        private static void AddScene()
        {
            var go = new GameObject(KSceneServiceLocatorName, typeof(ServiceLocatorSceneBootstrapper));
        }
        #endif
    }
}
