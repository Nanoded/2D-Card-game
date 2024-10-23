using CardGame.Characters;
using CardGame.BusEvents;

namespace CardGame.Cards
{
    [System.Serializable]
    public class CardsManipulator : IStartPlayerTurnHandler, IEndPlayerTurnHandler, ISelectCardHandler, IUseCardHandler, IDeselectCardHandler
    {
        [UnityEngine.SerializeField] private CardsManipulatorConfig _configs;

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
            _foldedDeck = new FoldedDeck();
            _deck.FillInDeck(_configs.CardSet, _configs.MaxCardsInDeck, _configs.CardPool);
            UpdateUI();
        }

        private void InitializeCardsHolders()
        {
            foreach (CardHolder holder in _configs.HoldersInHand)
            {
                holder.Initialize(_configs.CardPool);
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

        public void SelectCardHolder(CardHolder cardHolder)
        {
            if (_selectedCardHolder != null) _selectedCardHolder.DeselectCard();
            _selectedCardHolder = cardHolder;
            cardHolder.SelectCard();
            if(_currentCurrencyAmount >= _selectedCardHolder.CardInHolder.Cost)
            {
                EventBus.Invoke<IActivateCardUsage>(subscriber => subscriber.OnActivateCardUsageHandler(_selectedCardHolder.CardInHolder.CardType));
            }
        }

        public void DeselectCardHolder()
        {
            _selectedCardHolder?.DeselectCard();
            _selectedCardHolder = null;
        }

        public void UseCard(Character character)
        {
            if (_selectedCardHolder == null) return;
            _currentCurrencyAmount -= _selectedCardHolder.CardInHolder.Cost;
            _foldedDeck.AddCard(_selectedCardHolder.CardInHolder);
            _selectedCardHolder.CardInHolder.UseCard(character);
            _selectedCardHolder.FoldCard();
            UpdateUI();
        }

        public void RestoreCurrencyAmount()
        {
            _currentCurrencyAmount = _configs.CurrencyPerTurn;
            UpdateUI();
        }

        public void OnStartPlayerTurnHandler()
        {
            FillInHand();
            RestoreCurrencyAmount();
        }

        public void OnEndPlayerTurnHandler()
        {
            FoldCardsInHand();
            DeselectCardHolder();
        }

        public void OnSelectCardHandler(CardHolder cardHolder)
        {
            SelectCardHolder(cardHolder);
        }

        public void OnUseCardHandler(Character character)
        {
            UseCard(character);
        }

        public void OnDeselectCardHandler()
        {
            DeselectCardHolder();
        }
    }
}
