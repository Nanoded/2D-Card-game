using CardGame.Cards;
using CardGame.BusEvents;

namespace CardGame.Characters
{
    [System.Serializable]
    public class CharactersHolder : IEndPlayerTurnHandler, IDeselectCardHandler, IActivateCardUsage, IUseCardHandler
    {
        [UnityEngine.SerializeField] private CharactersHolderConfig _config;

        private Character _player;
        private Character[] _enemies;
        private int _deadEnemies;

        public Character Player => _player;
        public Character[] Enemies => _enemies;

        public void Initialize()
        {
            SpawnAndInitializePlayer();
            SpawnAndInitializeEnemies();
        }

        private void SpawnAndInitializePlayer()
        {
            _player = UnityEngine.Object.Instantiate(_config.PlayerPrefab, _config.PlayerSpawnPoint);
            _player.Initialize(this);
        }

        private void SpawnAndInitializeEnemies()
        {
            _enemies = new Character[_config.EnemiesSpawnPoints.Length];
            for(int i = 0; i < _config.EnemiesSpawnPoints.Length; i++)
            {
                _enemies[i] = UnityEngine.Object.Instantiate(_config.EnemiesPrefabs[i], _config.EnemiesSpawnPoints[i]);
                _enemies[i].Initialize(this);
            }
        }

        private void ActivateEnemies()
        {
            foreach(Character enemy in _enemies)
            {
                if(enemy.IsDead) continue;
                enemy.CanUseCard(true);
            }
        }

        private void ActivatePlayer()
        {
            _player.CanUseCard(true);
        }

        public void ActivateCharacters(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Attack:
                    ActivateEnemies();
                    break;
                case CardType.Buff: 
                    ActivatePlayer();
                    break;
                default: break;
            }
        }

        public void CharacterDeathHandler(Character character)
        {
            if(character is Player)
            {
                EventBus.Invoke<IEndLevelHandler>(subscriber => subscriber.OnEndLevelHandler(false));
            }
            else
            {
                _deadEnemies++;
                if(_deadEnemies >= _enemies.Length)
                {
                    EventBus.Invoke<IEndLevelHandler>(subscriber => subscriber.OnEndLevelHandler(true));
                }
            }
        }

        public void DeactivateFrames()
        {
            _player.CanUseCard(false);
            foreach(Character enemy in _enemies)
            {
                enemy.CanUseCard(false); 
            }
        }

        public void UseCard(Character character)
        {
            if(character is not Characters.Player)
            {
                _player.Attack();
            }
        }

        public void OnEndPlayerTurnHandler()
        {
            EventBus.Invoke<IStartEnemiesTurnHandler>(subscriber => subscriber.OnStartEnemiesTurnHandler(_enemies));
        }

        public void OnDeselectCardHandler()
        {
            DeactivateFrames();
        }

        public void OnActivateCardUsageHandler(CardType cardType)
        {
            ActivateCharacters(cardType);
        }

        public void OnUseCardHandler(Character character)
        {
            //OnDeselectCardHandler();
        }
    }
}
