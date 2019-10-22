using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour {

    NavMeshAgent agent;
    public bool actionsLocked { get; private set; }

    // Use this for initialization
    void Start () {       
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        if (!actionsLocked) {
            agent.SetDestination(PlayerManager.Instance().player.transform.position);

            float distance = Vector3.Distance(PlayerManager.Instance().player.transform.position, transform.position);
            if (distance <= agent.stoppingDistance) {
                FacePlayer();
            }
        }
    }

    void FacePlayer() {
        Vector3 direction = (PlayerManager.Instance().player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    public void UnlockActions() {
        actionsLocked = false;
        agent.SetDestination(PlayerManager.Instance().player.transform.position);
    }

    public void LockActions() {
        actionsLocked = true;        
        agent.isStopped = true;
        agent.ResetPath();
    }
}
