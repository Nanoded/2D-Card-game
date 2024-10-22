using CardGame.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace CardGame.Cards
{
    [System.Serializable]
    public class CardsManipulatorConfig
    {
        [SerializeField] private CardHolder[] _holdersInHand;
        [SerializeField] private CardSet _cardSet;
        [SerializeField] private int _maxCardsInDeck;
        [SerializeField] private int _currencyPerTurn;
        [SerializeField] private Transform _cardPool;

        [Header("Visual settings")]
        [SerializeField] private TextMeshPro _cardsInDeckText;
        [SerializeField] private TextMeshPro _cardsInFoldedDeckText;
        [SerializeField] private TextMeshPro _currencyAmountText;

        public CardHolder[] HoldersInHand => _holdersInHand;
        public CardSet CardSet => _cardSet;
        public int MaxCardsInDeck => _maxCardsInDeck;
        public int CurrencyPerTurn => _currencyPerTurn;
        public Transform CardPool => _cardPool;
        public TextMeshPro CardsInDeckText => _cardsInDeckText;
        public TextMeshPro CardsInFoldedDeckText => _cardsInFoldedDeckText;
        public TextMeshPro CurrencyAmountText => _currencyAmountText;
    }
}
