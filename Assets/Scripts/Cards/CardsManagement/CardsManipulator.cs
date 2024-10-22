using CardGame.Characters;

namespace CardGame.Cards
{
    public class CardsManipulator
    {
        private CardsManipulatorConfig _configs;
        private CardHolder _selectedCardHolder;
        private Deck _deck;
        private FoldedDeck _foldedDeck;
        private int _currentCurrencyAmount;

        public CardHolder SelectedCardHolder => _selectedCardHolder;

        public CardsManipulator(CardsManipulatorConfig configs)
        {
            _configs = configs;
            CreateDecks();
            InitializeCardsHolders();
            FillInHand();
            RestoreCurrencyAmount();
        }

        private void CreateDecks()
        {
            _deck = new Deck(_configs.CardSet, _configs.MaxCardsInDeck, _configs.CardPool);
            _foldedDeck = new FoldedDeck();
            UpdateUI();
        }

        private void InitializeCardsHolders()
        {
            foreach (CardHolder holder in _configs.HoldersInHand)
            {
                holder.Initialize(this, _configs.CardPool);
            }
        }

        private void UpdateUI()
        {
            _configs.CardsInDeckText.text = _deck.CardsInDeck.Count.ToString();
            _configs.CardsInFoldedDeckText.text = _foldedDeck.CardsInFoldedDeck.Count.ToString();
            _configs.CurrencyAmountText.text = _currentCurrencyAmount.ToString();
        }

        public void FillInHand()
        {
            if(_deck.CardsInDeck.Count == 0)
            {
                _deck.FillInAndShuffle(_foldedDeck);
            }

            for(int i = 0; i < _configs.HoldersInHand.Length; i++)
            {
                var card = _deck.GetFirstCardOrNull();
                if (!card) break;
                _configs.HoldersInHand[i].FillInCard(card);
            }
            UpdateUI();
        }

        public void FoldCardsInHand()
        {
            _selectedCardHolder?.DeselectCard();
            foreach(CardHolder holder in _configs.HoldersInHand)
            {
                if(holder.CardInHolder == null) continue;
                _foldedDeck.AddCard(holder.CardInHolder);
                holder.FoldCard();
            }
            UpdateUI();
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
            _currentCurrencyAmount = _configs.CurrencyPerTurn;
            UpdateUI();
        }
    }
}
