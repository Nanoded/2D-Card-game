using CardGame.Characters;
using UnityEngine;

namespace CardGame.Cards
{
    public class AttackCard : BaseCard
    {
        [Header("Attack card settings")]
        [SerializeField] private int _damageAmount;

        public override void UseCard(Character character)
        {
            character.GetDamage(_damageAmount);
        }
    }
}
