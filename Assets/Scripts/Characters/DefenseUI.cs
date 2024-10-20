using TMPro;
using UnityEngine;

namespace CardGame.Characters
{
    public class DefenseUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _defenseAmountText;

        public void UpdateDefenseAmount(int defenseAmount)
        {
            _defenseAmountText.text = defenseAmount.ToString();
        }
    }
}
