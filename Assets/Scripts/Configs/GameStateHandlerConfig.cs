using System;
using UnityEngine;

namespace CardGame
{
    [Serializable]
    public class GameStateHandlerConfig
    {
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _winScreen;

        public GameObject LoseScreen => _loseScreen;
        public GameObject WinScreen => _winScreen;
    }
}
