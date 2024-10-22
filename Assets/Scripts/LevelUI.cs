using CardGame.SceneManagement;
using UnityEngine;

namespace CardGame.UI
{
    public class LevelUI : MonoBehaviour
    {
        public void LoadNextLevel()
        {
            SceneHandler.Instance.StartNextLevel();
        }

        public void ReloadLevel()
        {
            SceneHandler.Instance.ReloadCurrentScene();
        }

        public void ReturnToMainMenu()
        {
            SceneHandler.Instance.ReturnToMainMenu();
        }
    }
}

