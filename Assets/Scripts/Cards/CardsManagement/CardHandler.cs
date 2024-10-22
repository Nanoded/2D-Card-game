using CardGame.Characters;
using UnityEngine;

namespace CardGame.Cards
{
    public class CardHandler
    {
        private bool _canUseCard;
        private CardsManipulator _cardManipulator;
        private CharactersHolder _charactersHolder;

        public CardHandler(CardsManipulator cardsManipulator, CharactersHolder charactersHolder) 
        { 
            _cardManipulator = cardsManipulator;
            _charactersHolder = charactersHolder;
        }

        private void TrySelectCard(Collider2D hittedCollider)
        {
            if (hittedCollider.TryGetComponent(out CardHolder cardHolder))
            {
                if (cardHolder.CardInHolder == null) return;
                if (cardHolder.IsSelected)
                {
                    DeselectCard();
                    return;
                }
                SelectCard(cardHolder);
            }
        }

        private void TryUseCard(Collider2D hittedCollider)
        {
            if (hittedCollider.TryGetComponent(out Character character))
            {
                if (_cardManipulator.SelectedCardHolder != null)
                {
                    if (character.IsDead || !_canUseCard) return;
                    character.SelectCharacter();
                    _cardManipulator.UseCard(character);
                    DeselectCard();
                }
            }
        }

        private void SelectCard(CardHolder cardHolder)
        {
            DeselectCard();
            _cardManipulator.SelectCardHolder(cardHolder, out _canUseCard);
            if (_canUseCard)
            {
                _charactersHolder.ActivateFrames(cardHolder.CardInHolder.CardType);
            }
        }

        private void DeselectCard()
        {
            _charactersHolder.DeactivateFrames();
            _cardManipulator.DeselectCardHolder();
        }

        public void OnMouseClickHandler(Collider2D hittedCollider)
        {
            TrySelectCard(hittedCollider);
            TryUseCard(hittedCollider);
        }
    }
}
