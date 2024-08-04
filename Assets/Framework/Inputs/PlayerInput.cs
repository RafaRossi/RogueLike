using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        public List<AxisInputEvent> axisInputEvents = new List<AxisInputEvent>();
        public List<TriggerInputEvent> triggerInputEvents = new List<TriggerInputEvent>();

        public List<MouseInputEvent> mouseInputEvents = new List<MouseInputEvent>();

        private void Update()
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
                mouseInputEvent.triggerClickEventInput?.Invoke();
            }
        }
    }

    [Serializable]
    public class AxisInputEvent
    {
        public string horizontalAxis;
        public string verticalAxis;
    
        public UnityEvent<Vector3> axisInputEvent = new UnityEvent<Vector3>();
    }

    [Serializable]
    public class TriggerInputEvent
    {
        public string triggerInputName;
    
        public UnityEvent triggerEventInput = new UnityEvent();
    }

    [Serializable]
    public class MouseInputEvent
    {
        public UnityEvent<Vector3> positionEventInput = new UnityEvent<Vector3>();
        public UnityEvent triggerClickEventInput = new UnityEvent();
    }
}