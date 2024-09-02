using Project.Utils;
using UnityEngine;

namespace Framework.Abilities
{
    public interface IAbility : ICommand
    {
        void Initialize();
        bool CanExecute();
    }
}
