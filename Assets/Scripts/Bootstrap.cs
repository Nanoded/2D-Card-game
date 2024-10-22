using CardGame.Cards;
using CardGame.Characters;
using UnityEngine;

namespace CardGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CardsManipulatorConfig _cardsManipulatorConfig;
        [SerializeField] private CharactersHolderConfig _charactersHolderConfig;
        private CardsManipulator _cardsManipulator;
        private CharactersHolder _charactersHolder;
        [SerializeField] private TurnHandler _turnHandler;

        private void Start()
        {
            _cardsManipulator = new CardsManipulator(_cardsManipulatorConfig);
            _charactersHolder = new CharactersHolder(_charactersHolderConfig);
            _turnHandler.Initialize(_cardsManipulator, _charactersHolder);
        }
    }
}
