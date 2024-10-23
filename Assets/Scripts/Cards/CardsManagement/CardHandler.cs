using CardGame.Characters;
using UnityEngine;

namespace CardGame.Cards
{
    public class CardHandler
    {
        private bool _canUseCard;

        public delegate void OnSelectCardHandler(CardHolder cardHolder);
        public event OnSelectCardHandler OnSelectCard;

        public delegate void OnUseCardHandler(Character character);
        public event OnUseCardHandler OnUseCard;

        public delegate void OnDeselectCardHandler();
        public event OnDeselectCardHandler OnDeselectCard;

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
                OnUseCard?.Invoke(character);
                if (character.IsDead || !_canUseCard) return;
                character.SelectCharacter();
                DeselectCard();
            }
        }

        private void SelectCard(CardHolder cardHolder)
        {
            DeselectCard();
            _canUseCard = true;
            OnSelectCard?.Invoke(cardHolder);
        }

        private void DeselectCard()
        {
            _canUseCard = false;
            OnDeselectCard?.Invoke();
        }

        public void OnMouseClickHandler(Collider2D hittedCollider)
        {
            TrySelectCard(hittedCollider);
            TryUseCard(hittedCollider);
        }
    }
}
