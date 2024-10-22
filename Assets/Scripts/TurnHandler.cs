using CardGame.Cards;
using CardGame.Characters;
using System.Collections;
using UnityEngine;

namespace CardGame
{
    public class TurnHandler : MonoBehaviour
    {
        [SerializeField] private float _enemyTurnDelay;
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _winScreen;

        private bool _canUseCard;
        private Camera _mainCamera;
        private CardsManipulator _cardManipulator;
        private CharactersHolder _charactersHolder;

        public void Initialize(CardsManipulator cardManipulator, CharactersHolder charactersHolder)
        {
            _mainCamera = Camera.main;

            _cardManipulator = cardManipulator;
            _charactersHolder = charactersHolder;
            _charactersHolder.OnPlayerDeath += LoseGame;
            _charactersHolder.OnAllEnemiesDefeated += WinGame;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Click();
            }
        }

        private void Click()
        {
            Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                TrySelectCard(hit.collider);
                TrySelectCharacter(hit.collider);
            }
        }

        private void TrySelectCard(Collider2D hittedCollider)
        {
            if (hittedCollider.TryGetComponent(out CardHolder cardHolder))
            {
                if (cardHolder.CardInHolder == null) return;
                if (cardHolder.IsSelected)
                {
                    DeselectCard();
                    return;
                }
                SelectCard(cardHolder);
            }
        }

        private void TrySelectCharacter(Collider2D hittedCollider)
        {
            if (hittedCollider.TryGetComponent(out Character character))
            {
                if (_cardManipulator.SelectedCardHolder != null)
                {
                    if (character.IsDead || !_canUseCard) return;
                    character.SelectCharacter();
                    UseCard(character);
                }
            }
        }

        public void SelectCard(CardHolder cardHolder)
        {
            DeselectCard();
            _cardManipulator.SelectCardHolder(cardHolder, out _canUseCard);
            if(_canUseCard)
            {
                _charactersHolder.ActivateFrames(cardHolder.CardInHolder.CardType);
            }

        }

        public void DeselectCard()
        {
            _charactersHolder.DeactivateFrames();
            _cardManipulator.DeselectCardHolder();
        }

        public void UseCard(Character character)
        {
            _cardManipulator.UseCard(character);
            DeselectCard();
        }

        private void StartEnemyTurn()
        {
            StartCoroutine(EnemyTurnSequence());
        }

        private IEnumerator EnemyTurnSequence()
        {
            foreach (var enemy in _charactersHolder.Enemies)
            {
                if (enemy.IsDead) continue;
                if (enemy is not IEnemy iEnemy) continue;
                iEnemy.SelectRandomMove();
                yield return new WaitForSeconds(_enemyTurnDelay);
            }

            StartPlayerTurn();
        }

        public void StartPlayerTurn()
        {
            _cardManipulator.FillInHand();
            _cardManipulator.RestoreCurrencyAmount();
        }

        public void EndPlayerTurn()
        {
            _cardManipulator.FoldCardsInHand();
            _cardManipulator.SelectedCardHolder?.DeselectCard();
            StartEnemyTurn();
        }

        public void WinGame()
        {
            _winScreen.SetActive(true);
        }

        public void LoseGame()
        {
            _loseScreen.SetActive(true);
        }
    }
}
