namespace Framework.State_Machine
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnAnimatorMove();
        void OnExit();
    }
}