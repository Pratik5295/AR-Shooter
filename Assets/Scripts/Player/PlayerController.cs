
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandling input;

    private GameObject _mainCamera;
    [SerializeField] private float speed = 6.0f;

    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private CharacterController controller;

    public float RotationSmoothTime = 0.12f;

    [SerializeField] private CharacterAnimations characterAnim;

    public float RotationSpeed = 240f;
    private Quaternion targetRotation;
    [SerializeField] private GameObject model;

    [SerializeField] private float rotationThreshold;

    private float boostAmount = 1;

    private void Start()
    {
      controller = GetComponent<CharacterController>();
      _mainCamera = Camera.main.gameObject;

      targetRotation = transform.rotation;

      model = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
      if(GameManager.Instance != null)
      {
         if (!GameManager.Instance.IsGameRunning())
          return;

              
      }

        Move();

    }

    private void LateUpdate()
    {
       Rotation();
    }

        private void Move()
        {
            float targetSpeed = speed;


            float moveHorizontal = input.move.x;
            float moveVertical = input.move.y;

            moveDirection = new Vector3(moveHorizontal, -1.0f, moveVertical); //y should be -1 to make player controller move downwards to prevent player getting stuck off the ground
            //moveDirection = transform.TransformDirection(moveDirection);

            if (new Vector3(moveDirection.x, 0.0f, moveDirection.z) == Vector3.zero) targetSpeed = 0.0f;



            // move the player
            controller.Move(moveDirection.normalized * (targetSpeed * Time.deltaTime));

            // update animator if using character
            if (characterAnim != null)
            {
                characterAnim.SetCharacterMoving(new Vector3(moveDirection.x, 0.0f, moveDirection.z).magnitude);
            }
        }

        private void Rotation()
        {
            float moveHorizontal = input.move.x;
            float moveVertical = input.move.y;

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
            if (movement.magnitude > 0.1f)
            {
                RotateModel(moveHorizontal, moveVertical);
            }
        }

        private void RotateToAngle(float targetAngle)
        {
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            model.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
        }

        private void RotateModel(float moveHorizontal, float moveVertical)
        {
            float targetAngle = 0;

            if (Mathf.Abs(moveHorizontal) > rotationThreshold && Mathf.Abs(moveVertical) < rotationThreshold)
            {
                // Rotate 90 degrees when only horizontal movement
                targetAngle = moveHorizontal > 0 ? 90 : -90;
            }
            else if (Mathf.Abs(moveVertical) > rotationThreshold && Mathf.Abs(moveHorizontal) < rotationThreshold)
            {
                // Rotate 90 degrees when only vertical movement
                targetAngle = moveVertical > 0 ? 0 : 180;
            }
            else if (Mathf.Abs(moveHorizontal) > rotationThreshold && Mathf.Abs(moveVertical) > rotationThreshold)
            {
                // Rotate 45 degrees when both axes are pressed
                targetAngle = Mathf.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg;
            }

            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
             model.transform.rotation = targetRotation;
        }
    }

