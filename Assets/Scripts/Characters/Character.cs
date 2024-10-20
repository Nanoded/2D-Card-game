using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame.Characters
{
    public abstract class Character : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private DefenseUI _defenseUI;
        [SerializeField] private GameObject _frame;
        [SerializeField] private int _maxHealth;
        [SerializeField] private Healthbar _healthbar;
        private int _currentDefense;
        private int _currentHealth;
        private Facade _facade;

        public void Initialize(Facade facade)
        {
            _facade = facade;
            _currentHealth = _maxHealth;
            _healthbar.Initialize(_maxHealth);
            _defenseUI.UpdateDefenseAmount(_currentDefense);
        }

        private void SelectCharacter()
        {
            _facade.UseCard(this);
            SetActiveFrame(false);
        }

        public void SetActiveFrame(bool isActive)
        {
            _frame.SetActive(isActive);
        }

        public void Heal(int healingAmount)
        {
            _currentHealth += healingAmount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            _healthbar.UpdateHP(_currentHealth);
        }

        public void GetDamage(int damageAmount)
        {
            int damage = damageAmount - _currentDefense;
            _currentDefense -= damageAmount;
            if (_currentDefense < 0)
            {
                _currentDefense = 0;
            }
            _defenseUI.UpdateDefenseAmount(_currentDefense);
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }
            _healthbar.UpdateHP(_currentHealth);
        }

        public void AddDefense(int defenseAmount)
        {
            _currentDefense += defenseAmount;
            _defenseUI.UpdateDefenseAmount(_currentDefense);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectCharacter();
        }
    }
}
