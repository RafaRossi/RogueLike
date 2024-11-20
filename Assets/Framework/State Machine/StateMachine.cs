using System;
using System.Collections.Generic;
using System.Linq;
using Project.Utils;
using UnityEngine;

namespace Framework.State_Machine
{
    public class StateMachine
    {
        private StateNode _current;
        private readonly Dictionary<Type, StateNode> _nodes = new();

        private readonly HashSet<ITransition> _anyTransition = new();

        public void Update()
        {
            var transition = GetTransition();

            if (transition != null)
            {
                ChangeState(transition.To);
            }
            
            _current.State?.Update();
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
        }

        private void ChangeState(IState state)
        {
            if(state == _current.State) return;
            
            _current.State?.OnExit();
            
            SetState(state);
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State.OnEnter();
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransition.Where(transition => transition.Condition.Evaluate()))
            {
                return transition;
            }

            return _current.Transitions.FirstOrDefault(transition => transition.Condition.Evaluate());
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }
        
        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransition.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}