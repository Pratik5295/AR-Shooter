using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandling : MonoBehaviour
{
  [SerializeField] private InputActionReference moveInput;

  [SerializeField] private InputActionReference shootInput;

  public bool shoot;
  public Vector2 move;

     private void Update()
     {
         move = moveInput.action.ReadValue<Vector2>();

         shoot = shootInput.action.ReadValue<bool>();
     }

}
