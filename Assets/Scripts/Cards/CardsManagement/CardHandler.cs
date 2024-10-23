using CardGame.Characters;
using CardGame.BusEvents;
using UnityEngine;

namespace CardGame.Cards
{
    public class CardHandler : IClickHandler
    {
        private bool _canUseCard;

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
                if (character.IsDead || !_canUseCard) return;
                EventBus.Invoke<IUseCardHandler>(subscriber => subscriber.OnUseCardHandler(character));
                character.SelectCharacter();
                DeselectCard();
            }
        }

        private void SelectCard(CardHolder cardHolder)
        {
            DeselectCard();
            _canUseCard = true;
            EventBus.Invoke<ISelectCardHandler>(subscriber => subscriber.OnSelectCardHandler(cardHolder));
        }

        private void DeselectCard()
        {
            _canUseCard = false;
            EventBus.Invoke<IDeselectCardHandler>(subscriber => subscriber.OnDeselectCardHandler());
        }

        public void OnClickHandler(Collider2D hittedCollider)
        {
            if(hittedCollider == null) return;
            TrySelectCard(hittedCollider);
            TryUseCard(hittedCollider);
        }
    }
}
