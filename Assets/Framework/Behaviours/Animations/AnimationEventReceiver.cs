using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventReceiver : MonoBehaviour
{
    [SerializeField] private List<AnimationEvent> animationEvents = new();

    public void OnAnimationEventTriggered(string eventName)
    {
        var matchingEvent = animationEvents.Find(e => e.eventName.Equals(eventName));
        
        matchingEvent?.onAnimationEvent?.Invoke();
    }
}