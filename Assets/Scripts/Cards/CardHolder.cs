using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

namespace CardGame.Cards
{
    public class CardHolder : MonoBehaviour
    {
        private const int c_maxSortingOrder = 100;

        [SerializeField] private float _cardPulloutHeight = 2;
        [SerializeField] private float _cardTweensSpeed = .5f;
        [SerializeField] private SortingGroup _sortingGroup;

        private BaseCard _cardInHolder;
        private bool _isSelected;
        private int _startSortingOrder;
        private Transform _cardPool;
        private Vector3 _positionInHand;

        public BaseCard CardInHolder => _cardInHolder;
        public bool IsSelected => _isSelected;

        public void Initialize(Transform cardPool)
        {
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
            transform.DOLocalMoveY(transform.up.y * _cardPulloutHeight, _cardTweensSpeed);
        }

        public void DeselectCard()
        {
            _isSelected = false;
            transform.DOMove(_positionInHand, _cardTweensSpeed);
            _sortingGroup.sortingOrder = _startSortingOrder;
        }

        public void FillInCard(BaseCard card)
        {
            _cardInHolder = card;
            _cardInHolder.transform.SetParent(transform, false);
            _cardInHolder.transform.DOLocalMove(Vector3.zero, _cardTweensSpeed);
            _cardInHolder.transform.DOLocalRotate(Vector3.zero, _cardTweensSpeed);
        }

        public void FoldCard()
        {
            _cardInHolder.transform.SetParent(null);
            _cardInHolder.transform.DOMove(_cardPool.position, _cardTweensSpeed);
            _cardInHolder = null;
            DeselectCard();
        }
    }
}
