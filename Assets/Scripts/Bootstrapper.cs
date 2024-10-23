using CardGame.BusEvents;
using CardGame.Cards;
using CardGame.Characters;
using UnityEngine;

namespace CardGame
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CardsManipulator _cardsManipulator;
        [SerializeField] private CharactersHolder _charactersHolder;
        [SerializeField] private EndLevelScreensHandler _endLevelScreensHandler;
        [SerializeField] private TurnHandler _turnHandler;
        private CardHandler _cardHandler;
        private InputHandler _inputHandler;

        private void Start()
        {
            _cardHandler = new CardHandler();
            _inputHandler = new InputHandler();
            SubscribeToEvents();
            InitializeAll();
        }

        private void OnDestroy()
        {
            _inputHandler.DisableInput();
            UnsubscribeFromEvents();
        }

        private void InitializeAll()
        {
            _cardsManipulator.Initialize();
            _charactersHolder.Initialize();
            _turnHandler.Initialize();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe(_cardHandler);
            EventBus.Subscribe(_cardsManipulator);
            EventBus.Subscribe(_charactersHolder);
            EventBus.Subscribe(_endLevelScreensHandler);
            EventBus.Subscribe(_turnHandler);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe(_cardHandler);
            EventBus.Unsubscribe(_cardsManipulator);
            EventBus.Unsubscribe(_charactersHolder);
            EventBus.Unsubscribe(_endLevelScreensHandler);
            EventBus.Unsubscribe(_turnHandler);
        }
    }
}
