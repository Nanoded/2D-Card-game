using CardGame.Cards;
using UnityEngine;

namespace CardGame.Characters
{
    public class CharactersHolder
    {
        private Character _player;
        private Character[] _enemies;
        private CharactersHolderConfig _config;
        private int _deadEnemies;

        public delegate void DeathEventHandler();
        public event DeathEventHandler OnPlayerDeath;
        public event DeathEventHandler OnAllEnemiesDefeated;

        public Character Player => _player;
        public Character[] Enemies => _enemies;

        public CharactersHolder(CharactersHolderConfig config)
        {
            _config = config;
            SpawnAndInitializePlayer();
            SpawnAndInitializeEnemies();
        }

        private void SpawnAndInitializePlayer()
        {
            _player = Object.Instantiate(_config.PlayerPrefab, _config.PlayerSpawnPoint);
            _player.Initialize(this);
        }

        private void SpawnAndInitializeEnemies()
        {
            _enemies = new Character[_config.EnemiesSpawnPoints.Length];
            for(int i = 0; i < _config.EnemiesSpawnPoints.Length; i++)
            {
                _enemies[i] = Object.Instantiate(_config.EnemiesPrefabs[i], _config.EnemiesSpawnPoints[i]);
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
