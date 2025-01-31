using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimState
{
    IDLE = 0,
    RUN = 1,
    ATTACK = 2
}

[System.Serializable]
public class EnemyAnim
{
    public EnemyAnimState State;

    public AnimationClip animationClip;
}

public class EnemyAnimation : MonoBehaviour
{
    public EnemyAnim[] animations;
    [SerializeField]
    private Animator animator;
    private EnemyAnimState currentState;
    private Dictionary<EnemyAnimState, AnimationClip> animationDict;

    void Awake()
    {
        animationDict = new Dictionary<EnemyAnimState, AnimationClip>();

        foreach (var anim in animations)
        {
            animationDict[anim.State] = anim.animationClip;
        }
    }

    public void PlayAnimation(EnemyAnimState newState)
    {
        if (currentState == newState) return;
        currentState = newState;

        if (animationDict.TryGetValue(newState, out AnimationClip clip))
        {
            animator.Play(clip.name);
        }
        else
        {
            Debug.LogWarning($"No animation clip assigned for state: {newState}");
        }
    }
}
