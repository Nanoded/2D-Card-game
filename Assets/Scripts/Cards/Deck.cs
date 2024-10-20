using CardGame.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Cards
{
    public class Deck
    {
        private List<BaseCard> _cardsInDeck;

        public IReadOnlyList<BaseCard> CardsInDeck => _cardsInDeck;

        private void FillInDeck(CardSet cardSet, int cardsInDeck, Transform cardPool)
        {
            for (int i = 0; i < cardsInDeck; i++)
            {
                int randomCardIndex = Random.Range(0, cardSet.Cards.Length);
                BaseCard card = cardSet.Cards.ElementAtOrDefault(randomCardIndex);
                BaseCard instantiatedCard = Object.Instantiate(card);
                instantiatedCard.transform.position = cardPool.position;
                _cardsInDeck.Add(instantiatedCard);
            }
        }

        private void ShuffleCards()
        {
            for(int i = _cardsInDeck.Count - 1; i >= 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (_cardsInDeck[randomIndex], _cardsInDeck[i]) = (_cardsInDeck[i], _cardsInDeck[randomIndex]);
            }
        }

        public void CreateDeck(CardSet cardSet, int cardsInDeck, Transform cardPool)
        {
            _cardsInDeck = new();
            FillInDeck(cardSet, cardsInDeck, cardPool);
        }

        public void FillInAndShuffle(FoldedDeck foldedCards)
        {
            _cardsInDeck = foldedCards.CardsInFoldedDeck.ToList();
            foldedCards.ClearCards();
            ShuffleCards();
        }

        public BaseCard GetFirstCardOrNull()
        {
            var card = _cardsInDeck.FirstOrDefault();
            _cardsInDeck.Remove(card);
            return card;
        }
    }
}
