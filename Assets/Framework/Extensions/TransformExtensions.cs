using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Extensions
{
    public static class TransformExtensions{
        
        public static IEnumerable<Transform> Children(Transform parent)
        {
            return parent.Cast<Transform>();
        }

        public static void EnableChildren(this Transform parent)
        {
            parent.IterateOnChildren(child => child.gameObject.SetActive(true));
        }
        
        public static void DisableChildren(this Transform parent)
        {
            parent.IterateOnChildren(child => child.gameObject.SetActive(false));
        }
        
        public static void DestroyChildren(this Transform parent)
        {
            parent.IterateOnChildren(child => Object.Destroy(child.gameObject));
        }

        private static void IterateOnChildren(this Transform parent, System.Action<Transform> action)
        {
            for (var i = parent.childCount - 1; i >= 0 ; i--)
            {
                action(parent.GetChild(i));
            }
        }
    }
}
