using UnityEngine;

namespace CardGame.Characters
{
    [System.Serializable]
    public class CharactersHolderConfig
    {
        [SerializeField] private Character _playerPrefab;
        [SerializeField] private Character[] _enemiesPrefabs;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemiesSpawnPoints;

        public Character PlayerPrefab => _playerPrefab;
        public Character[] EnemiesPrefabs => _enemiesPrefabs;
        public Transform PlayerSpawnPoint => _playerSpawnPoint;
        public Transform[] EnemiesSpawnPoints => _enemiesSpawnPoints;
    }
}
