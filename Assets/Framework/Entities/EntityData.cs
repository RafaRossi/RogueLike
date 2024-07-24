using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Entities
{
    [System.Serializable]
    public abstract class EntityData<T, TU>
    {
        [field:SerializeField] public T Entity { get; set; }
        [field:SerializeField] public TU Value { get; set; }
    }
}
