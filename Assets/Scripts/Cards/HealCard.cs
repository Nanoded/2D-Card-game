using CardGame.Characters;
using UnityEngine;

namespace CardGame.Cards
{
    public class HealCard : BaseCard
    {
        [Header("Heal card settings")]
        [SerializeField] private int _healAmount;

        public override void UseCard(Character character)
        {
            character.Heal(_healAmount);
        }
    }
}
