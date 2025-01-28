using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCharacterMoving(float _moving)
    {
        animator.SetFloat("Move", _moving);
    }
}
