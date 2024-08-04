using System.Collections.Generic;
using UnityEngine;

namespace Framework.Entities
{
    public class CharacterFlags : MonoBehaviour
    {
        public List<Flag> flags = new List<Flag>();
        
        public bool TryGetFlag(Flag flag)
        {
            return flags.Contains(flag);
        }

        public void AddFlag(Flag flag)
        {
            flags.Add(flag);
        }

        public bool RemoveFlag(Flag flag)
        {
            return flags.Remove(flag);
        }
    }
}
