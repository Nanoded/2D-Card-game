using CardGame.Cards;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CardGame
{
    public class InputHandler
    {
        private Camera _mainCamera;
        private CardHandler _cardHandler;
        private InputAction _mouseLeftClickAction;
        private InputAction _mousePosition;

        public InputHandler(CardHandler cardHandler)
        {
            _mainCamera = Camera.main;
            _cardHandler = cardHandler;
            CreateInput();
        }

        private void CreateInput()
        {
            Input input = new Input();

            _mouseLeftClickAction = input.Player.LeftMouseClick;
            _mouseLeftClickAction.performed += context => Click();
            _mouseLeftClickAction.Enable();

            _mousePosition = input.Player.MousePosition;
            _mousePosition.Enable();
        }

        private void Click()
        {
            Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(_mousePosition.ReadValue<Vector2>());
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                _cardHandler.OnMouseClickHandler(hit.collider);
            }
        }

        public void DisableInput()
        {
            _mouseLeftClickAction.Disable();
            _mousePosition.Disable();
        }
    }
}
