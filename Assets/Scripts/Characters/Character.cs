using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] private AnimationsController _animationsController;
        [SerializeField] private Collider2D _damageHandlerCollider;
        [SerializeField] private DefenseUI _defenseUI;
        [SerializeField] private GameObject _frame;
        [SerializeField] private int _basicDefense;
        [SerializeField] private int _maxHealth;
        [SerializeField] private Healthbar _healthbar;
        private int _currentDefense;
        private int _currentHealth;
        protected CharactersHolder _characterHolder;

        public bool IsDead => _currentHealth <= 0;

        public void Initialize(CharactersHolder characterHolder)
        {
            _characterHolder = characterHolder;
            _currentHealth = _maxHealth;
            _currentDefense = _basicDefense;
            _healthbar.Initialize(_maxHealth);
            _defenseUI.UpdateDefenseAmount(_currentDefense);
        }

        private int ApplyDefenseToDamage(int damageAmount)
        {
            int damage = damageAmount - _currentDefense;
            damage = damage < 0 ? 0 : damage;
            _currentDefense -= damageAmount;
            if (_currentDefense < 0)
            {
                _currentDefense = 0;
            }
            _defenseUI.UpdateDefenseAmount(_currentDefense);
            return damage;
        }

        public void SelectCharacter()
        {
            _characterHolder.UseCard(this);
            EnableTakeDamage(false);
        }

        public void EnableTakeDamage(bool isActive)
        {
            _frame.SetActive(isActive);
            _damageHandlerCollider.enabled = isActive;
        }

        public void Heal(int healingAmount)
        {
            _currentHealth += healingAmount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            _healthbar.UpdateHP(_currentHealth);
            _animationsController.PlayBuffAnimation();
        }

        public void GetDamage(int damageAmount)
        {
            int damage = ApplyDefenseToDamage(damageAmount);
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                _characterHolder.CharacterDeathHandler(this);
                _animationsController.PlayDeathAnimation();
            }
            _healthbar.UpdateHP(_currentHealth);
            _animationsController.PlayHurtAnimation();
        }

        public void AddDefense(int defenseAmount)
        {
            _currentDefense += defenseAmount;
            _defenseUI.UpdateDefenseAmount(_currentDefense);
            _animationsController.PlayBuffAnimation();
        }

        public virtual void Attack()
        {
            _animationsController.PlayAttackAnimation();
        }
    }
}
