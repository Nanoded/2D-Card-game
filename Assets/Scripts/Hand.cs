using CardGame.ScriptableObjects;
using CardGame.Cards;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CardGame
{
    public class Hand : MonoBehaviour, IObservableState
    {
        [SerializeField] private int _maxCardsInHand;
        [SerializeField] private Deck _deck;

        private BaseCard _selectedCard;
        private List<BaseCard> _cardsInHand = new();
        private List<BaseCard> _foldedCards = new();

        public void FillInHand()
        {
            for(int i = 0; i < _maxCardsInHand; i++)
            {
                var card = _deck.GetFirstCardOrNull();
                if (!card) break;
                _cardsInHand.Add(card);
            }
        }

        public void Fold()
        {
            
        }

        public void SelectCard(BaseCard card)
        {
            _selectedCard = card;
        }

        public void UpdateState()
        {
            FillInHand();
        }
    }
}
