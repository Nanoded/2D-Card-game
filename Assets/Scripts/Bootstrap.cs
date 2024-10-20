using CardGame.Cards;
using CardGame.Characters;
using System.Collections;
using UnityEngine;

namespace CardGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CardsManipulator _cardsManipulator;
        [SerializeField] private CharactersHolder _charactersHolder;
        [SerializeField] private Facade _facade;

        private void Start()
        {
            _facade.Initialize(_cardsManipulator, _charactersHolder);
        }
    }
}
