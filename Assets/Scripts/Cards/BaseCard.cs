using CardGame.Characters;
using UnityEngine;

namespace CardGame.Cards
{
    public abstract class BaseCard : MonoBehaviour
    {
        [SerializeField] private CardType _cardType;
        [SerializeField] private int _cost;

        public CardType CardType => _cardType;
        public int Cost => _cost;

        public abstract void UseCard(Character character);
    }
}
