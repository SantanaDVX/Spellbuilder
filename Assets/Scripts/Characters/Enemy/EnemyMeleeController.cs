using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(EnemyController))]
public class EnemyMeleeController : MonoBehaviour {

    public float initAttackDistance;
    public float initAttackAngle;
    public float maxAttackDistance;
    public float maxAttackAngle;
    EnemyController ec;

    public float meleeAttackSpeed = 1f;

    public int damage;
    public bool attacking = false;

    // Use this for initialization
    void Start () {
        ec = GetComponent<EnemyController>();        
    }

    // Update is called once per frame
    void Update () {
        if (!ec.actionsLocked) {
            float distance = Vector3.Distance(PlayerManager.Instance().player.transform.position, transform.position);
            if (distance <= initAttackDistance) {
                StartCoroutine(MeleeAttack());
            }
        }
    }

    IEnumerator MeleeAttack() {
        ec.LockActions();
        attacking = true;

        yield return new WaitForSeconds(meleeAttackSpeed);
        
        float distance = Vector3.Distance(PlayerManager.Instance().player.transform.position, transform.position);

        //TODO: Need to check angle
        if (distance <= maxAttackDistance) {
            PlayerManager.Instance().player.stats.TakeDamage(damage, DamageType.Physical);
        }

        attacking = false;
        ec.UnlockActions();
    }
    
    [CustomEditor(typeof(EnemyMeleeController))]
    public class DrawWireArc : Editor {
        private void OnSceneGUI() {
            if (true) {
                EnemyMeleeController myObj = (EnemyMeleeController)target;
  
                Handles.color = Color.green;
                Handles.DrawSolidArc(new Vector3(myObj.transform.position.x, 0.99f, myObj.transform.position.z), myObj.transform.up, myObj.transform.forward, myObj.maxAttackAngle / 2.0f, myObj.maxAttackDistance);
                Handles.DrawSolidArc(new Vector3(myObj.transform.position.x, 0.99f, myObj.transform.position.z), myObj.transform.up, myObj.transform.forward, myObj.maxAttackAngle / -2.0f, myObj.maxAttackDistance);
                myObj.maxAttackDistance = (float)Handles.ScaleValueHandle(myObj.maxAttackDistance, myObj.transform.position + myObj.transform.forward * myObj.maxAttackDistance + myObj.transform.forward * 0.2f, myObj.transform.rotation, 1, Handles.ConeHandleCap, 1);
                myObj.maxAttackAngle = (float)Handles.ScaleValueHandle(myObj.maxAttackAngle, myObj.transform.position + myObj.transform.forward * myObj.maxAttackDistance, myObj.transform.rotation, 1, Handles.CubeHandleCap, 1);

                Handles.color = Color.red;
                Handles.DrawSolidArc(new Vector3(myObj.transform.position.x, 1, myObj.transform.position.z), myObj.transform.up, myObj.transform.forward, myObj.initAttackAngle / 2.0f, myObj.initAttackDistance);
                Handles.DrawSolidArc(new Vector3(myObj.transform.position.x, 1, myObj.transform.position.z), myObj.transform.up, myObj.transform.forward, myObj.initAttackAngle / -2.0f, myObj.initAttackDistance);
                myObj.initAttackDistance = (float)Handles.ScaleValueHandle(myObj.initAttackDistance, myObj.transform.position + myObj.transform.forward * myObj.initAttackDistance + myObj.transform.forward * 0.2f, myObj.transform.rotation, 1, Handles.ConeHandleCap, 1);
                myObj.initAttackAngle = (float)Handles.ScaleValueHandle(myObj.initAttackAngle, myObj.transform.position + myObj.transform.forward * myObj.initAttackDistance, myObj.transform.rotation, 1, Handles.CubeHandleCap, 1);

            }
        }
    }
}
