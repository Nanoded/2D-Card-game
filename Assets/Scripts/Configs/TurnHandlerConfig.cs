using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    [System.Serializable]
    public class TurnHandlerConfig
    {
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private float _enemyTurnDelay;

        public Button EndTurnButton => _endTurnButton;
        public float EnemyTurnDelay => _enemyTurnDelay;
    }
}
