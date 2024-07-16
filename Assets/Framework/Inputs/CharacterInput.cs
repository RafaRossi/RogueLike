using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInput : MonoBehaviour
{
    public List<AxisInputEvent> axisInputEvents = new List<AxisInputEvent>();
    public List<TriggerInputEvent> triggerInputEvents = new List<TriggerInputEvent>();

    private void Update()
    {
        foreach (var axisInputEvent in axisInputEvents)
        {
            var input = new Vector3(Input.GetAxisRaw(axisInputEvent.horizontalAxis), 0,
                Input.GetAxisRaw(axisInputEvent.verticalAxis));
            
            axisInputEvent.axisInputEvent?.Invoke(input);
        }

        foreach (var triggerInputEvent in triggerInputEvents)
        {
            if(Input.GetButtonDown(triggerInputEvent.triggerInputName)) triggerInputEvent.triggerEventInput?.Invoke();
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