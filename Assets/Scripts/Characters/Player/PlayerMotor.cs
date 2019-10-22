using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    NavMeshAgent agent;
    public Transform target;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        playerController = GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (playerController.GetActionsLocked()) {
            target = null;
        }

        if (target != null) {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point) {
        agent.updateRotation = true;
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable focus) {
        agent.stoppingDistance = focus.radius * .8f;
        agent.updateRotation = false;
        target = focus.transform;
    }

    public void StopFollowingTarget() {
        agent.stoppingDistance = 0f;
        target = null;
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    public void CastTowardTarget(Vector3 point) {
        target = null;
        agent.updateRotation = false;
        transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        playerController.LockActions();
        agent.isStopped = true;
        agent.ResetPath();
    }
}
