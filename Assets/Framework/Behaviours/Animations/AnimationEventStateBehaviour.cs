using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class AnimationEventStateBehaviour : StateMachineBehaviour
{
    public string eventName;
    [Range(0f, 1f)] public float triggerTime;

    private bool _hasTriggered;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        _hasTriggered = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var currentTime = stateInfo.normalizedTime % 1;

        if (!_hasTriggered && currentTime >= triggerTime)
        {
            NotifyReceiver(animator);
            _hasTriggered = true;
        }
    }

    private void NotifyReceiver(Animator animator)
    {
        var receiver = animator.GetComponent<AnimationEventReceiver>();

        if (receiver != null)
        {
            receiver.OnAnimationEventTriggered(eventName);
        }
    }
}

[Serializable]
public class AnimationEvent
{
    public string eventName;
    public UnityEvent onAnimationEvent;
}
