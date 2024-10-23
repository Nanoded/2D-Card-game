namespace CardGame
{
    [System.Serializable]
    public class EndLevelScreensHandler
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
    }
}
