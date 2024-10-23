using CardGame.Characters;
using CardGame.BusEvents;
using System.Threading.Tasks;

namespace CardGame
{
    [System.Serializable]
    public class TurnHandler : IStartEnemiesTurnHandler
    {
        [UnityEngine.SerializeField] private TurnHandlerConfig _config;

        public void Initialize()
        {
            _config.EndTurnButton.onClick.AddListener(EndPlayerTurn);
        }

        private async Task EnemyTurnSequence(Character[] enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsDead) continue;
                if (enemy is not IEnemy iEnemy) continue;
                iEnemy.SelectRandomMove();
                await Task.Delay(System.TimeSpan.FromSeconds(_config.EnemyTurnDelay));
            }

            StartPlayerTurn();
        }

        public async void StartEnemyTurn(Character[] enemies)
        {
            await EnemyTurnSequence(enemies);
        }

        public void StartPlayerTurn()
        {
            _config.EndTurnButton.interactable = true;
            EventBus.Invoke<IStartPlayerTurnHandler>(subscriber => subscriber.OnStartPlayerTurnHandler());
        }

        private void EndPlayerTurn()
        {
            EventBus.Invoke<IEndPlayerTurnHandler>(subscriber => subscriber.OnEndPlayerTurnHandler());
        }

        public void OnStartEnemiesTurnHandler(Character[] enemies)
        {
            _config.EndTurnButton.interactable = false;
            StartEnemyTurn(enemies);
        }
    }
}
