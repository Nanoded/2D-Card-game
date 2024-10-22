using CardGame.Characters;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace CardGame.Cards
{
    public class CardHolder : MonoBehaviour
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
        public bool IsSelected => _isSelected;

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

        public void SelectCard()
        {
            _isSelected = true;
            _sortingGroup.sortingOrder = c_maxSortingOrder;
            transform.DOLocalMoveY(transform.up.y * c_cardPulloutHeight, c_cardTweensSpeed);
        }

        public void DeselectCard()
        {
            _isSelected = false;
            transform.DOMove(_positionInHand, c_cardTweensSpeed);
            _sortingGroup.sortingOrder = _startSortingOrder;
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
    }
}
