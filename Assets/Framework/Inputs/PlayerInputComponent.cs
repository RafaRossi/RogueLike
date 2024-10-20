using System;
using System.Collections.Generic;
using Framework.Behaviours.Animations;
using Framework.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Inputs
{
    public class PlayerInputComponent : BaseComponent<PlayerInputComponent>
    {
        private PlayerInput _currentInputData;

        public void ChangeInput(PlayerInput newInput)
        {
            _currentInputData = newInput;
        }
        
        private void Update()
        {
            _currentInputData?.Update();
        }
    }
    
    [Serializable]
    public class PlayerInput
    {
        public List<AxisInputEvent> axisInputEvents = new List<AxisInputEvent>();
        public List<TriggerInputEvent> triggerInputEvents = new List<TriggerInputEvent>();

        public List<MouseInputEvent> mouseInputEvents = new List<MouseInputEvent>();

        public void Update()
        {
            foreach (var triggerInputEvent in triggerInputEvents)
            {
                if(Input.GetButtonDown(triggerInputEvent.triggerInputName)) triggerInputEvent.triggerEventInput?.Invoke();
            }
        
            foreach (var axisInputEvent in axisInputEvents)
            {
                var input = new Vector3(Input.GetAxisRaw(axisInputEvent.horizontalAxis), 0,
                    Input.GetAxisRaw(axisInputEvent.verticalAxis));
            
                axisInputEvent.axisInputEvent?.Invoke(input);
            }

            foreach (var mouseInputEvent in mouseInputEvents)
            {
                mouseInputEvent.positionEventInput?.Invoke(Input.mousePosition);
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var mouseInputEvent in mouseInputEvents)
                {
                    mouseInputEvent.triggerClickEventInput?.Invoke();
                }
            }
        }
        
        public class Builder
        {
            private readonly List<AxisInputEvent> _axisInputEvents = new();
            private readonly List<MouseInputEvent> _mouseInputEvents = new();
            private readonly List<TriggerInputEvent> _triggerInputEvents = new();
            
            public Builder WithAxisInputEvent(AxisInputEvent axisInputEvent)
            {
                _axisInputEvents.Add(axisInputEvent);

                return this;
            }

            public Builder WithAxisInputEvent(List<AxisInputEvent> axisInputEvents)
            {
                foreach (var axisInputEvent in axisInputEvents)
                {
                    _axisInputEvents.Add(axisInputEvent);
                }

                return this;
            }

            public Builder WithTriggerInputEvent(TriggerInputEvent triggerInputEvent)
            {
                _triggerInputEvents.Add(triggerInputEvent);

                return this;
            }

            public Builder WithTriggerInputEvents(List<TriggerInputEvent> triggerInputEvents)
            {
                foreach (var triggerInputEvent in triggerInputEvents)
                {
                    _triggerInputEvents.Add(triggerInputEvent);
                }
                return this;
            }

            public Builder WithMouseInputEvent(MouseInputEvent mouseInputEvent)
            {
                _mouseInputEvents.Add(mouseInputEvent);
                return this;
            }
            
            public Builder WithMouseInputEvents(List<MouseInputEvent> mouseInputEvents)
            {
                foreach (var mouseInputEvent in mouseInputEvents)
                {
                    _mouseInputEvents.Add(mouseInputEvent);
                }

                return this;
            }

            public PlayerInput Build()
            {
                return new PlayerInput
                {
                    axisInputEvents = _axisInputEvents,
                    triggerInputEvents = _triggerInputEvents,
                    mouseInputEvents = _mouseInputEvents
                };
            }
        }
    }

    [Serializable]
    public class AxisInputEvent
    {
        public AxisInputEvent(UnityEvent<Vector3> axisInputEvent, string horizontalAxis = "Horizontal", string verticalAxis = "Vertical")
        {
            this.horizontalAxis = horizontalAxis;
            this.verticalAxis = verticalAxis;

            this.axisInputEvent = axisInputEvent;
        }
        
        public string horizontalAxis;
        public string verticalAxis;
    
        public UnityEvent<Vector3> axisInputEvent;
    }

    [Serializable]
    public class TriggerInputEvent
    {
        public TriggerInputEvent(string triggerInputName, UnityEvent triggerEventInput)
        {
            this.triggerInputName = triggerInputName;
            this.triggerEventInput = triggerEventInput;
        }
        public string triggerInputName;
    
        public UnityEvent triggerEventInput;
    }

    [Serializable]
    public class MouseInputEvent
    {
        public UnityEvent<Vector3> positionEventInput;
        public UnityEvent triggerClickEventInput;

        public MouseInputEvent(UnityEvent<Vector3> positionEventInput, UnityEvent triggerClickEventInput)
        {
            this.positionEventInput = positionEventInput;
            this.triggerClickEventInput = triggerClickEventInput;
        }

        public MouseInputEvent(UnityEvent<Vector3> positionEventInput)
        {
            this.positionEventInput = positionEventInput;
        }
        
        public MouseInputEvent(UnityEvent triggerClickEventInput)
        {
            this.triggerClickEventInput = triggerClickEventInput;
        }
    }
}