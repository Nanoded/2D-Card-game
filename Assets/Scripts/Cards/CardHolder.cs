using CardGame.Characters;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace CardGame.Cards
{
    public class CardHolder : MonoBehaviour, IPointerClickHandler
    {
        private const float c_cardPulloutHeight = 2;
        private const float c_cardTweensSpeed = .5f;
        private const int c_maxSortingOrder = 100;

        [SerializeField] private SortingGroup _sortingGroup;

        private BaseCard _cardInHolder;
        private bool _isSelected;
        private CardsManipulator _cardManipulator;
        private int _startSortingOrder;
        private Transform _cardPool;
        private Vector3 _positionInHand;

        public BaseCard CardInHolder => _cardInHolder;

        public void Initialize(CardsManipulator cardManipulator, Transform cardPool)
        {
            _cardManipulator = cardManipulator;
            _cardPool = cardPool;
        }

        private void Start()
        {
            _positionInHand = transform.position;
            _startSortingOrder = _sortingGroup.sortingOrder;
        }

        private void SelectCard()
        {
            _sortingGroup.sortingOrder = c_maxSortingOrder;
            transform.DOLocalMoveY(transform.up.y * c_cardPulloutHeight, c_cardTweensSpeed);
            _cardManipulator.SelectCardHolder(this);
        }

        public void DeselectCard()
        {
            transform.DOMove(_positionInHand, c_cardTweensSpeed);
            _sortingGroup.sortingOrder = _startSortingOrder;
            _cardManipulator.DeselectCardHolder();
        }

        public void FillInCard(BaseCard card)
        {
            _cardInHolder = card;
            _cardInHolder.transform.SetParent(transform, false);
            _cardInHolder.transform.DOLocalMove(Vector3.zero, c_cardTweensSpeed);
            _cardInHolder.transform.DOLocalRotate(Vector3.zero, c_cardTweensSpeed);
        }

        public void FoldCard()
        {
            _cardInHolder.transform.SetParent(null);
            _cardInHolder.transform.DOMove(_cardPool.position, c_cardTweensSpeed);
            _cardInHolder = null;
            DeselectCard();
        }

        public void UseCard(Character character)
        {
            _cardInHolder.UseCard(character);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_cardInHolder == null) return;
            _isSelected = !_isSelected;
            if (_isSelected) SelectCard();
            else DeselectCard();
        }
    }
}
