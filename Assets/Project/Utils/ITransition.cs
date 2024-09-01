using Framework.State_Machine;

namespace Project.Utils
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}