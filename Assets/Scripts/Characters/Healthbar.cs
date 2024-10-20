using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame.Characters
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Image _healthbar;
        [SerializeField] private TextMeshProUGUI _healthText;

        private int _maxHealth;

        public void Initialize(int maxHealth)
        {
            _maxHealth = maxHealth;
            UpdateHP(_maxHealth);
        }

        public void UpdateHP(float currentHealth)
        {
            _healthbar.fillAmount = currentHealth / _maxHealth;
            _healthText.text = $"{currentHealth}/{_maxHealth}";
        }
    }
}
