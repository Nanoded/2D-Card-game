using CardGame.Cards;
using CardGame.Characters;
using UnityEngine;

namespace CardGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CardsManipulator _cardsManipulator;
        [SerializeField] private CharactersHolder _charactersHolder;
        [SerializeField] private TurnHandler _turnHandler;

        private void Start()
        {
            _turnHandler.Initialize(_cardsManipulator, _charactersHolder);
        }
    }
}
