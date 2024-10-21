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
        private int _deadEnemies;

        public delegate void DeathEventHandler();
        public event DeathEventHandler OnPlayerDeath;
        public event DeathEventHandler OnAllEnemiesDefeated;

        public Character Player => _player;
        public Character[] Enemies => _enemies;

        public void Initialize()
        {
            SpawnAndInitializePlayer();
            SpawnAndInitializeEnemies();
        }

        private void SpawnAndInitializePlayer()
        {
            _player = Instantiate(_playerPrefab, _playerSpawnPoint);
            _player.Initialize(this);
        }

        private void SpawnAndInitializeEnemies()
        {
            _enemies = new Character[_enemiesSpawnPoints.Length];
            for(int i = 0; i < _enemiesSpawnPoints.Length; i++)
            {
                _enemies[i] = Instantiate(_enemiesPrefab[i], _enemiesSpawnPoints[i]);
                _enemies[i].Initialize(this);
            }
        }

        private void ActivateEnemiesFrames()
        {
            foreach(Character enemy in _enemies)
            {
                if(enemy.IsDead) continue;
                enemy.EnableTakeDamage(true);
            }
        }

        private void ActivatePlayerFrame()
        {
            _player.EnableTakeDamage(true);
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

        public void CharacterDeathHandler(Character character)
        {
            if(character is Player)
            {
                OnPlayerDeath?.Invoke();
            }
            else
            {
                _deadEnemies++;
                if(_deadEnemies >= _enemies.Length)
                {
                    OnAllEnemiesDefeated?.Invoke();
                }
            }
        }

        public void DeactivateFrames()
        {
            _player.EnableTakeDamage(false);
            foreach(Character enemy in _enemies)
            { 
                enemy.EnableTakeDamage(false); 
            }
        }

        public void UseCard(Character character)
        {
            if(character is not Characters.Player)
            {
                _player.Attack();
            }
        }
    }
}
