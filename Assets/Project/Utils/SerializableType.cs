using System;
using UnityEngine;

namespace Project.Utils
{
    [Serializable]
    public class SerializableType : ISerializationCallbackReceiver
    {
        [SerializeField] private string assemblyQualifiedName = string.Empty;
        
        public Type Type { get; private set; }
        
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            assemblyQualifiedName = Type?.AssemblyQualifiedName ?? assemblyQualifiedName;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (!TryGetType(assemblyQualifiedName, out var type))
            {
                Debug.LogError($"Type { assemblyQualifiedName } not found");
            }

            Type = type;
        }

        private static bool TryGetType(string typeString, out Type type)
        {
            type = Type.GetType(typeString);

            return type != null;
        }
    }
}