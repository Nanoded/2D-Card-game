using CardGame.Characters;

namespace CardGame
{
    public class GameStateHandler
    {
        private CharactersHolder _charactersHolder;
        private GameStateHandlerConfig _config;

        public GameStateHandler(GameStateHandlerConfig config)
        {
            _config = config;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _charactersHolder.OnPlayerDeath += LoseGame;
            _charactersHolder.OnAllEnemiesDefeated += WinGame;
        }

        private void WinGame()
        {
            _config.WinScreen.SetActive(true);
        }

        private void LoseGame()
        {
            _config.LoseScreen.SetActive(true);
        }

        public void UnsubscribeFromEvents()
        {
            _charactersHolder.OnPlayerDeath -= LoseGame;
            _charactersHolder.OnAllEnemiesDefeated -= WinGame;
        }
    }
}
