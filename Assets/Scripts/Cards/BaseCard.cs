using CardGame.Characters;
using TMPro;
using UnityEngine;

namespace CardGame.Cards
{
    public abstract class BaseCard : MonoBehaviour
    {
        [SerializeField] private CardType _cardType;
        [SerializeField] private int _cost;
        [SerializeField] private TextMeshPro _costText;

        public CardType CardType => _cardType;
        public int Cost => _cost;

        private void Start()
        {
            _costText.text = _cost.ToString();
        }

        public abstract void UseCard(Character character);
    }
}
