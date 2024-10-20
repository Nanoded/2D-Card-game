using CardGame.Cards;
using UnityEngine;

namespace CardGame.Characters
{
    public class CharactersHolder : MonoBehaviour
    {
        [SerializeField] private Character _playerPrefab;
        [SerializeField] private Character[] _enemiesPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemiesSpawnPoints;

        private Character _player;
        private Character[] _enemies;
        private Facade _facade;

        public void Initialize(Facade facade)
        {
            _facade = facade;
            SpawnAndInitializePlayer();
            SpawnAndInitializeEnemies();
        }

        private void SpawnAndInitializePlayer()
        {
            _player = Instantiate(_playerPrefab, _playerSpawnPoint);
            _player.Initialize(_facade);
        }

        private void SpawnAndInitializeEnemies()
        {
            _enemies = new Character[_enemiesSpawnPoints.Length];
            for(int i = 0; i < _enemiesSpawnPoints.Length; i++)
            {
                _enemies[i] = Instantiate(_enemiesPrefab[i], _enemiesSpawnPoints[i]);
                _enemies[i].Initialize(_facade);
            }
        }

        private void ActivateEnemiesFrames()
        {
            foreach(Character enemy in _enemies)
            {
                enemy.SetActiveFrame(true);
            }
        }

        private void ActivatePlayerFrame()
        {
            _player.SetActiveFrame(true);
        }

        public void ActivateFrames(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Attack:
                    ActivateEnemiesFrames();
                    break;
                case CardType.Buff: 
                    ActivatePlayerFrame();
                    break;
                default: break;
            }
        }

        public void DeactivateFrames()
        {
            _player.SetActiveFrame(false);
            foreach(Character enemy in _enemies)
            { 
                enemy.SetActiveFrame(false); 
            }
        }
    }
}
