using CardGame.Cards;
using CardGame.Characters;
using System.Threading.Tasks;

namespace CardGame
{
    public class TurnHandler
    {
        private CardsManipulator _cardManipulator;
        private CharactersHolder _charactersHolder;
        private TurnHandlerConfig _config;

        public TurnHandler(CardsManipulator cardManipulator, CharactersHolder charactersHolder, TurnHandlerConfig config)
        {
            _cardManipulator = cardManipulator;
            _charactersHolder = charactersHolder;
            _config = config;
            _config.EndTurnButton.onClick.AddListener(EndPlayerTurn);
        }

        private async void StartEnemyTurn()
        {
            await EnemyTurnSequence();
        }

        private async Task EnemyTurnSequence()
        {
            foreach (var enemy in _charactersHolder.Enemies)
            {
                if (enemy.IsDead) continue;
                if (enemy is not IEnemy iEnemy) continue;
                iEnemy.SelectRandomMove();
                await Task.Delay(System.TimeSpan.FromSeconds(_config.EnemyTurnDelay));
            }

            StartPlayerTurn();
        }

        private void StartPlayerTurn()
        {
            _cardManipulator.FillInHand();
            _cardManipulator.RestoreCurrencyAmount();
        }

        private void EndPlayerTurn()
        {
            _cardManipulator.FoldCardsInHand();
            _cardManipulator.SelectedCardHolder?.DeselectCard();
            StartEnemyTurn();
        }
    }
}
