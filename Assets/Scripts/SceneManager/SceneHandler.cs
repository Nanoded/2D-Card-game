using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardGame.SceneManagement
{
    public class SceneHandler
    {
        private int _currentSceneIndex;
        private static SceneHandler _instance;

        public static SceneHandler Instance
        {
            get
            {
                _instance ??= new SceneHandler();
                return _instance;
            }
        }

        private void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void StartNextLevel()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            _currentSceneIndex++;
            if(_currentSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                _currentSceneIndex = 0;
            }
            LoadScene(_currentSceneIndex);
        }

        public void ReturnToMainMenu()
        {
            _currentSceneIndex = 0;
            LoadScene(_currentSceneIndex);
        }

        public void ReloadCurrentScene()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            LoadScene(_currentSceneIndex);
        }
    }
}

