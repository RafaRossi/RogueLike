namespace Framework.State_Machine
{
    public abstract class BaseState : IState
    {
        public abstract void OnEnter();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void OnAnimatorMove();

        public abstract void OnExit();
    }
}