using UnityEngine;

namespace CardGame.Characters
{
    public class AnimationHash
    {
        public int AttackTrigger => Animator.StringToHash("Attack");
        public int BuffTrigger => Animator.StringToHash("Buff");
    }
}

