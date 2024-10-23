using UnityEngine;
using UnityEngine.InputSystem;

namespace CardGame
{
    public class InputHandler
    {
        private Camera _mainCamera;
        private InputAction _mouseLeftClickAction;
        private InputAction _mousePosition;

        public delegate void OnClickHandler(Collider2D hittedCollider);
        public event OnClickHandler OnClick;

        public InputHandler()
        {
            _mainCamera = Camera.main;
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
                OnClick?.Invoke(hit.collider);
            }
        }

        public void DisableInput()
        {
            _mouseLeftClickAction.Disable();
            _mousePosition.Disable();
        }
    }
}
