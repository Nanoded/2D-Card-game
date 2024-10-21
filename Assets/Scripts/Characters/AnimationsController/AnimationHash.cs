using UnityEngine;

namespace CardGame.Characters
{
    public class AnimationHash
    {
        public static int AttackTrigger => Animator.StringToHash("Attack");
        public static int BuffTrigger => Animator.StringToHash("Buff");
        public static int DeathBool => Animator.StringToHash("Death");
        public static int HurtTrigger => Animator.StringToHash("Hurt");
    }
}

