using CardGame.Cards;
using UnityEngine;

namespace CardGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CardSet", menuName = "Create scriptable object/CardSet")]
    public class CardSet : ScriptableObject
    {
        [SerializeField] private BaseCard[] _cards;

        public BaseCard[] Cards => _cards;
    }
}
