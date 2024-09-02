namespace Project.Utils
{
    public interface ICommand
    {
        void Execute();
        void Cancel();
    }
}