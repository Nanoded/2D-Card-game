using CardGame.Cards;
using CardGame.Characters;

namespace CardGame
{
    public class TurnHandler
    {
        private CardsManipulator _cardsManipulator;
        private CharactersHolder _charactersHolder;

        public void Initialize(CardsManipulator cardsManipulator, CharactersHolder charactersHolder)
        {
            _cardsManipulator = cardsManipulator;
            _charactersHolder = charactersHolder;
        }

        private void EnemyTurn()
        {
            
        }

        public void StartTurn()
        {
            _cardsManipulator.FillInHand();
        }

        public void EndTurn()
        {
            _cardsManipulator.FoldCardsInHand();
            _cardsManipulator.SelectedCardHolder?.DeselectCard();
        }
    }
}
