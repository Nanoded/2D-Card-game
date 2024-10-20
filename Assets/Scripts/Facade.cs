using CardGame.Cards;
using CardGame.Characters;
using UnityEngine;

namespace CardGame
{
    public class Facade : MonoBehaviour
    {
        private CardsManipulator _cardManipulator;
        private CharactersHolder _charactersHolder;

        public void Initialize(CardsManipulator cardManipulator, CharactersHolder charactersHolder)
        {
            _cardManipulator = cardManipulator;
            _charactersHolder = charactersHolder;
            _cardManipulator.Initialize(this);
            _charactersHolder.Initialize(this);
        }

        public void SelectCard()
        {
            _charactersHolder.ActivateFrames(_cardManipulator.SelectedCardHolder.CardInHolder.CardType);
        }

        public void DeselectCard()
        {
            _charactersHolder.DeactivateFrames();
        }

        public void UseCard(Character character)
        {
            _cardManipulator.UseCard(character);
        }

        private void StartEnemyTurn()
        {
            
        }

        public void StartPlayerTurn()
        {
            _cardManipulator.FillInHand();
        }

        public void EndPlayerTurn()
        {
            _cardManipulator.FoldCardsInHand();
            _cardManipulator.SelectedCardHolder?.DeselectCard();
            StartEnemyTurn();
        }
    }
}
