using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Entities
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField] private List<AttributeData> attributesData = new List<AttributeData>();
        [SerializeField] private List<Flag> flags = new List<Flag>();

        [Header("Events")]
        public UnityEvent<AttributeData> onReceivedAttribute = new UnityEvent<AttributeData>();
        public UnityEvent<AttributeData> onDisposeAttribute = new UnityEvent<AttributeData>();

        public UnityEvent<Flag> onReceivedFlag = new UnityEvent<Flag>();
        public UnityEvent<Flag> onDisposeFlag = new UnityEvent<Flag>();

        public bool AddAttributeData(Attribute attribute)
        {
            return AddAttributeData(new AttributeData { Entity = attribute });
        }

        public bool AddAttributeData(AttributeData attributeData, bool forceAllowDuplicates = false)
        {
            /*if (!attributeData.Entity.allowDuplicates)
            {
                if (attributesData.Any(a => attributeData.Entity == a.Entity))
                {
                    return false;
                }
            }*/
            
            if (attributesData.Any(a => attributeData.Entity == a.Entity))
            {
                return false;
            }
            
            attributesData.Add(attributeData);
            
            onReceivedAttribute?.Invoke(attributeData);
            
            return true;
        }

        public void RemoveAttributeData(Attribute attribute)
        {
            var datas = attributesData.Where(a => a.Entity == attribute);

            foreach (var data in datas)
            {
                attributesData.Remove(data);
                
                onDisposeAttribute?.Invoke(data);
            }
        }

        public bool AddFlag(Flag flag)
        {
            //if (!flag.allowDuplicates && flags.Contains(flag)) return false;

            if (flags.Contains(flag)) return false;
            
            flags.Add(flag);
            onReceivedFlag?.Invoke(flag);
            
            return true;
        }
        
        public void RemoveFlag(Flag flag)
        {
            if (flags.Contains(flag)) flags.Remove(flag);
            
            onDisposeFlag?.Invoke(flag);
        }

        public bool TryGetAttribute(Attribute attribute, out AttributeData attributeData)
        {
            foreach (var aData in attributesData.Where(aData => aData.Entity == attribute))
            {
                attributeData = aData;
                return true;
            }

            attributeData = null;
            return false;
        }

        public bool TryGetFlag(Flag flag) => flags.Contains(flag);
    }
}
