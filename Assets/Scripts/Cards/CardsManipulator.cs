using CardGame.Characters;
using CardGame.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace CardGame.Cards
{
    public class CardsManipulator : MonoBehaviour
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

        private CardHolder _selectedCardHolder;
        private Deck _deck;
        private FoldedDeck _foldedDeck;
        private int _currentCurrencyAmount;

        public CardHolder SelectedCardHolder => _selectedCardHolder;

        public void Initialize()
        {
            CreateDecks();
            InitializeCardsHolders();
            FillInHand();
            RestoreCurrencyAmount();
        }

        private void CreateDecks()
        {
            _deck = new Deck();
            _deck.CreateDeck(_cardSet, _maxCardsInDeck, _cardPool);
            _foldedDeck = new FoldedDeck();
            UpdateUI();
        }

        private void InitializeCardsHolders()
        {
            foreach (CardHolder holder in _holdersInHand)
            {
                holder.Initialize(this, _cardPool);
            }
        }

        private void UpdateUI()
        {
            _cardsInDeckText.text = _deck.CardsInDeck.Count.ToString();
            _cardsInFoldedDeckText.text = _foldedDeck.CardsInFoldedDeck.Count.ToString();
            _currencyAmountText.text = _currentCurrencyAmount.ToString();
        }

        public void FillInHand()
        {
            if(_deck.CardsInDeck.Count == 0)
            {
                _deck.FillInAndShuffle(_foldedDeck);
            }

            for(int i = 0; i < _holdersInHand.Length; i++)
            {
                var card = _deck.GetFirstCardOrNull();
                if (!card) break;
                _holdersInHand[i].FillInCard(card);
            }
            UpdateUI();
        }

        public void FoldCardsInHand()
        {
            _selectedCardHolder?.DeselectCard();
            foreach(CardHolder holder in _holdersInHand)
            {
                if(holder.CardInHolder == null) continue;
                _foldedDeck.AddCard(holder.CardInHolder);
                holder.FoldCard();
            }
            _cardsInFoldedDeckText.text = _foldedDeck.CardsInFoldedDeck.Count.ToString();
        }

        public void SelectCardHolder(CardHolder cardHolder, out bool canUseCard)
        {
            if (_selectedCardHolder != null) _selectedCardHolder.DeselectCard();
            _selectedCardHolder = cardHolder;
            cardHolder.SelectCard();
            canUseCard = _currentCurrencyAmount >= _selectedCardHolder.CardInHolder.Cost;
        }

        public void DeselectCardHolder()
        {
            _selectedCardHolder?.DeselectCard();
            _selectedCardHolder = null;
        }

        public void UseCard(Character character)
        {
            _currentCurrencyAmount -= _selectedCardHolder.CardInHolder.Cost;
            _foldedDeck.AddCard(_selectedCardHolder.CardInHolder);
            _selectedCardHolder.UseCard(character);
            _selectedCardHolder.FoldCard();
            UpdateUI();
        }

        public void RestoreCurrencyAmount()
        {
            _currentCurrencyAmount = _currencyPerTurn;
            UpdateUI();
        }
    }
}
