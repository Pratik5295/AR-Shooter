using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // State enum for the FSM
    public enum EnemyState
    {
        Idle,
        MoveToTarget,
        Attack,
        Dead
    }

    [Header("Enemy Settings")]
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public float damage = 10f;

    [Header("Target")]
    public Transform target; // The player or object to attack

    private EnemyState currentState = EnemyState.Idle;
    protected float attackTimer = 0f;
    private Health health; // Reference to health component

    [Header("Optional Components")]
    public GameObject deathEffect;
    [SerializeField]
    private EnemyAnimation animator;

    public Action OnEnemyDeathEvent;

    void Start()
    {
        // Initialize health component
        health = GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("Health component not found on enemy.");
        }
    }

    void Update()
    {
        // FSM logic
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleBehavior();
                break;

            case EnemyState.MoveToTarget:
                MoveToTargetBehavior();
                break;

            case EnemyState.Attack:
                AttackBehavior();
                break;

            case EnemyState.Dead:
                DieBehavior();
                break;
        }

        // Check if health is depleted
        if (health != null && health.GetCurrentHealth() <= 0 && currentState != EnemyState.Dead)
        {
            ChangeState(EnemyState.Dead);
        }
    }

    /// <summary>
    /// Change the enemy's current state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    #region State Behaviors

    private void IdleBehavior()
    {
        if (animator != null)
        {
            animator.PlayAnimation(EnemyAnimState.IDLE);
        }

        //Check if target exists, if not, do a check to search for the target
        // If a target exists, transition to MoveToTarget
        if (target != null)
        {
            ChangeState(EnemyState.MoveToTarget);
        }
        else
        {
            //Asking game manager for a target
            if (GameManager.Instance != null)
            {
                target = GameManager.Instance.PlayerObject.transform;
            }
        }
    }

    private void MoveToTargetBehavior()
    {

        if (target == null)
        {
            if (animator != null)
            {
                animator.PlayAnimation(EnemyAnimState.IDLE);
            }

            return;
        }

        if (animator != null)
        {
            animator.PlayAnimation(EnemyAnimState.RUN);
        }

        // Move towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Face the target
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        // Check if within attack range
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            ChangeState(EnemyState.Attack);
        }
    }

    protected virtual void AttackBehavior()
    {
        // Stop moving
        attackTimer += Time.deltaTime;


        if (attackTimer >= attackCooldown)
        {
            if (animator != null)
            {
                animator.PlayAnimation(EnemyAnimState.ATTACK);
            }

            // Perform attack (e.g., deal damage to the target's health component)
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }

            Debug.Log($"{gameObject.name} attacked {target.name} for {damage} damage.");
            attackTimer = 0f;
        }

        // Check if target moves out of attack range
        if (Vector3.Distance(transform.position, target.position) > attackRange)
        {
            ChangeState(EnemyState.MoveToTarget);
        }
    }

    private void DieBehavior()
    {
        // Handle death (e.g., play death animation, spawn effects)
        Debug.Log($"{gameObject.name} has died.");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        //Fire the enemy death event to notify others
        OnEnemyDeathEvent?.Invoke();

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }

    #endregion

    #region Public Methods for Customization

    /// <summary>
    /// Set the target for the enemy to attack.
    /// </summary>
    /// <param name="newTarget">The target to set.</param>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        ChangeState(EnemyState.MoveToTarget);
    }

    #endregion
}
