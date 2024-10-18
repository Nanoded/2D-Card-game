using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace CardGame.Cards
{
    public class BaseCard : MonoBehaviour, IPointerDownHandler
    {
        private const float c_cardPulloutHeight = 2;
        private const float c_cardTweensSpeed = .5f;
        private const int c_maxSortingOrder = 100;

        [SerializeField] private CardType _cardType;

        private bool _isSelected;
        private int _startSortingOrder;
        private SortingGroup _sortingGroup;
        private Vector3 _positionInHand;

        public CardType CardType => _cardType;

        private void Start()
        {
            TryGetComponent(out _sortingGroup);
            if(_sortingGroup == null )
            {
                Debug.LogError("Can't find SortingGroup");
                Destroy(this);
            }
        }

        private void SelectCard()
        {
            _positionInHand = transform.position;
            _startSortingOrder = _sortingGroup.sortingOrder;
            _sortingGroup.sortingOrder = c_maxSortingOrder;
            transform.DOLocalMoveY(transform.up.y * c_cardPulloutHeight, c_cardTweensSpeed);
        }

        private void DeselectCard()
        {
            transform.DOMove(_positionInHand, c_cardTweensSpeed);
            _sortingGroup.sortingOrder = _startSortingOrder;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isSelected = !_isSelected;
            if(_isSelected) SelectCard();
            else DeselectCard();
        }

        public void UseCard()
        {

        }
    }
}
