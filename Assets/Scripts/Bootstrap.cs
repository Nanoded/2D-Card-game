using CardGame.Cards;
using CardGame.Characters;
using UnityEngine;

namespace CardGame
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private CardsManipulatorConfig _cardsManipulatorConfig;
        [SerializeField] private CharactersHolderConfig _charactersHolderConfig;
        [SerializeField] private GameStateHandlerConfig _gameStateHandlerConfig;
        [SerializeField] private TurnHandlerConfig _turnHandlerConfig;

        private CardHandler _cardHandler;
        private CardsManipulator _cardsManipulator;
        private CharactersHolder _charactersHolder;
        private GameStateHandler _gameStateHandler;
        private InputHandler _inputHandler;
        private TurnHandler _turnHandler;

        private void Start()
        {
            _cardsManipulator = new CardsManipulator(_cardsManipulatorConfig);
            _charactersHolder = new CharactersHolder(_charactersHolderConfig);
            _cardHandler = new CardHandler(_cardsManipulator, _charactersHolder);
            _gameStateHandler = new GameStateHandler(_gameStateHandlerConfig);
            _inputHandler = new InputHandler(_cardHandler);
            _turnHandler = new TurnHandler(_cardsManipulator, _charactersHolder, _turnHandlerConfig);
        }

        private void OnDestroy()
        {
            _inputHandler.DisableInput();
            _gameStateHandler.UnsubscribeFromEvents();
        }
    }
}
