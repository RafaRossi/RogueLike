using UnityEngine;

namespace Framework.Behaviours.Animations
{
    public class AnimationComponent : MonoBehaviour
    {
        [SerializeField] private Animator animatorController;

        public void PlayAnimation(int animationHash, float fadeDuration = 0.2f)
        {
            animatorController.CrossFadeInFixedTime(animationHash, fadeDuration);
        }
    }
}
