using CardGame.Cards;
using CardGame.Characters;
using UnityEngine;

namespace CardGame
{
    public class Bootstrap : MonoBehaviour
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
            _charactersHolder.OnPlayerDeath += _endLevelScreensHandler.LoseGame;
            _charactersHolder.OnAllEnemiesDefeated += _endLevelScreensHandler.WinGame;
            _inputHandler.OnClick += _cardHandler.OnMouseClickHandler;
            _cardHandler.OnSelectCard += _cardsManipulator.SelectCardHolder;
            _cardHandler.OnUseCard += _cardsManipulator.UseCard;
            _cardHandler.OnDeselectCard += OnDeselectCardHandler;
            _cardsManipulator.OnEnoughCurrency += _charactersHolder.ActivateFrames;
            _turnHandler.OnStartPlayerTurn += OnStartPlayerTurnHandler;
            _turnHandler.OnEndPlayerTurn += OnEndPlayerTurnHandler;
        }

        private void UnsubscribeFromEvents()
        {
            _charactersHolder.OnPlayerDeath -= _endLevelScreensHandler.LoseGame;
            _charactersHolder.OnAllEnemiesDefeated -= _endLevelScreensHandler.WinGame;
            _inputHandler.OnClick -= _cardHandler.OnMouseClickHandler;
            _cardHandler.OnSelectCard -= _cardsManipulator.SelectCardHolder;
            _cardHandler.OnUseCard -= _cardsManipulator.UseCard;
            _cardHandler.OnDeselectCard -= OnDeselectCardHandler;
            _cardsManipulator.OnEnoughCurrency -= _charactersHolder.ActivateFrames;
            _turnHandler.OnStartPlayerTurn -= OnStartPlayerTurnHandler;
            _turnHandler.OnEndPlayerTurn -= OnEndPlayerTurnHandler;
        }

        private void OnStartPlayerTurnHandler()
        {
            _cardsManipulator.FillInHand();
            _cardsManipulator.RestoreCurrencyAmount();
        }

        private void OnEndPlayerTurnHandler()
        {
            _cardsManipulator.FoldCardsInHand();
            _cardsManipulator.DeselectCardHolder();
            _turnHandler.StartEnemyTurn(_charactersHolder.Enemies);
        }

        private void OnDeselectCardHandler()
        {
            _charactersHolder.DeactivateFrames();
            _cardsManipulator.DeselectCardHolder();
        }
    }
}
