using CardGame.Cards;
using CardGame.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame
{
    public class Deck : MonoBehaviour, IObservableState
    {
        [SerializeField] private CardSet _cardSet;
        [SerializeField] private int _maxCardsInDeck;

        private List<BaseCard> _cardsInDeck;

        private void FillInDeck()
        {
            _cardsInDeck = new();
            for (int i = 0; i < _maxCardsInDeck; i++)
            {
                int randomCardIndex = Random.Range(0, _cardSet.Cards.Length);
                BaseCard card = _cardSet.Cards.ElementAtOrDefault(randomCardIndex);

                _cardsInDeck.Add(_cardSet.Cards[randomCardIndex]);
            }
        }

        private void FillInAndShuffle()
        {
            
        }

        public BaseCard GetFirstCardOrNull()
        {
            var card = _cardsInDeck.FirstOrDefault();
            _cardsInDeck.Remove(card);
            return card;
        }

        public void UpdateState()
        {
            if(_cardsInDeck == null) FillInDeck();
            if (_cardsInDeck.Count > 0) return;
            FillInAndShuffle();
        }
    }
}
