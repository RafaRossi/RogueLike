using System;
using Framework.Entities;
using UnityEngine;

public abstract class BaseComponent<T> : MonoBehaviour, IComponent where T : IComponent
{
    [field:SerializeField] public ComponentController ComponentController { get; set; }

    protected virtual void Awake()
    {
        ComponentController.AddEntityOfType(typeof(T), this);
    }
}

namespace Framework.Behaviours.Animations
{
    public class AnimationComponent : BaseComponent<AnimationComponent>
    {
        [SerializeField] private Animator animatorController;

        public void PlayAnimationCrossFade(int animationHash, float fadeDuration = 0.2f)
        {
            animatorController.CrossFadeInFixedTime(animationHash, fadeDuration);
        }

        public void PlayAnimation(int id, float value)
        {
            animatorController.SetFloat(id, value);
        }
    }
}
