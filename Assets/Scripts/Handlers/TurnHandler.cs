using CardGame.Characters;
using System.Threading.Tasks;

namespace CardGame
{
    [System.Serializable]
    public class TurnHandler
    {
        [UnityEngine.SerializeField] private TurnHandlerConfig _config;

        public delegate void OnTurnHandler();
        public event OnTurnHandler OnStartPlayerTurn;
        public event OnTurnHandler OnEndPlayerTurn;

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
            OnStartPlayerTurn?.Invoke();
        }

        private void EndPlayerTurn()
        {
            OnEndPlayerTurn?.Invoke();
        }
    }
}
