using UnityEngine;

namespace CardGame.Characters
{
    public class AnimationsController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(AnimationHash.AttackTrigger);
        }

        public void PlayBuffAnimation()
        {
            _animator.SetTrigger(AnimationHash.BuffTrigger);
        }

        public void PlayDeathAnimation()
        {
            _animator.SetBool(AnimationHash.DeathBool, true);
        }

        public void PlayHurtAnimation()
        {
            _animator.SetTrigger(AnimationHash.HurtTrigger);
        }
    }
}

