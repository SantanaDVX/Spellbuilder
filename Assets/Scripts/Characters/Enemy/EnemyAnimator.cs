using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMeleeController))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour {
    const float locomotionAnimationSmoothTime = 0.1f;

    NavMeshAgent agent;
    EnemyMeleeController emc;
    Animator animator;

    Dictionary<string, float> clipTimes = new Dictionary<string, float>();

    void Start () {       
        agent = GetComponent<NavMeshAgent>();
        emc = GetComponent<EnemyMeleeController>();
        animator = GetComponent<Animator>();

        SetAnimClipTimes();
    }

    // Update is called once per frame
    void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        animator.SetBool("isAttacking", emc.attacking);
        
        if (emc.meleeAttackSpeed != 0) {
            animator.SetFloat("attackSpeed", clipTimes["Enemy_Attack"] / emc.meleeAttackSpeed);
        }
    }

    public void SetAnimClipTimes() {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips) {
            clipTimes[clip.name] = clip.length;
        }
    }
}
