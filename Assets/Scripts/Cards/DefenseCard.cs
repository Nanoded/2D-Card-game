using CardGame.Characters;
using UnityEngine;

namespace CardGame.Cards
{
    public class DefenseCard : BaseCard
    {
        [Header("Defense card settings")]
        [SerializeField] private int _defenseAmount;

        public override void UseCard(Character character)
        {
            character.AddDefense(_defenseAmount);
        }
    }
}
