using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Service_Locator
{
    public class ServiceManager
    {
        private Dictionary<Type, object> services = new Dictionary<Type, object>();

        public IEnumerable<object> RegisteredServices => services.Values;

        public bool TryGet<T>(out T service) where T : class
        {
            var type = typeof(T);

            if (services.TryGetValue(type, out var obj))
            {
                service = obj as T;
                return true;
            }

            service = null;
            return false;
        }

        public T Get<T>() where T : class
        {
            var type = typeof(T);

            if (services.TryGetValue(type, out var service))
            {
                return service as T;
            }

            throw new ArgumentException($"ServiceManager.Get: Service of type {type.FullName} not registered");
        }

        public ServiceManager Register<T>(T service)
        {
            var type = typeof(T);

            if (!services.TryAdd(type, service))
            {
                Debug.LogError($"ServiceManager.Register: Service of Type { type.FullName } already registered");
            }

            return this;
        }
        
        public ServiceManager Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
            {
                throw new ArgumentException("Type of service doesn't match the type of service interface");
            }
            
            if (!services.TryAdd(type, service))
            {
                Debug.LogError($"ServiceManager.Register: Service of Type { type.FullName } already registered");
            }

            return this;
        }
    }
}
