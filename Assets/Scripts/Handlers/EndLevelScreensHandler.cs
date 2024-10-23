using CardGame.BusEvents;

namespace CardGame
{
    [System.Serializable]
    public class EndLevelScreensHandler : IEndLevelHandler
    {
        [UnityEngine.SerializeField] private GameStateHandlerConfig _config;

        public void WinGame()
        {
            _config.WinScreen.SetActive(true);
        }

        public void LoseGame()
        {
            _config.LoseScreen.SetActive(true);
        }

        public void OnEndLevelHandler(bool isWin)
        {
            if (isWin)
            {
                WinGame();
            }
            else
            {
                LoseGame();
            }
        }
    }
}
