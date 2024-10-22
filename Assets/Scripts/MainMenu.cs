using CardGame.SceneManagement;
using UnityEngine;

namespace CardGame.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneHandler.Instance.StartNextLevel();
        }
    }
}

