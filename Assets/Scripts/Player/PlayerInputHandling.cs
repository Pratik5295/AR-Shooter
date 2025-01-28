using UnityEngine;
using UnityEngine.InputSystem;

    public class PlayerInputHandling : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;

        public Vector2 move;

        private void Update()
        {
            move = moveInput.action.ReadValue<Vector2>();
        }
    }

