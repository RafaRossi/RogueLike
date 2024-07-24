using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Entities
{
    public abstract class Entity
    {
        [Header(header: "", order = 2)]
        public bool allowDuplicates = false;
    }
    
    public static class EntityHelpers
    {
        public static bool DoesHaveEntity<T, TU>(this Entity entity, List<EntityData<T, TU>> entityDatas, ref EntityData<T, TU> foundEntity) where T : Entity
        {
            foreach (var entityData in entityDatas.Where(entityData => entityData.Entity.Equals(entity)))
            {
                foundEntity = entityData;
                return true;
            }

            return false;
        }
    }
}
