using CardGame.Cards;
using System.Collections.Generic;

namespace CardGame.Cards
{
    public class FoldedDeck
    {
        private List<BaseCard> _foldedCards = new();

        public IReadOnlyList<BaseCard> CardsInFoldedDeck => _foldedCards;

        public void AddCard(BaseCard card) => _foldedCards.Add(card);

        public void ClearCards() => _foldedCards.Clear();
    }
}
