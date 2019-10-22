using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactable : MonoBehaviour {

    public float radius = 3f;
    Transform player;

    bool isFocus = false;
    bool hasInteracted = false;

    private void Update() {
        if (isFocus && !hasInteracted) {
            if (Vector3.Distance(player.position, transform.position) <= radius) {
                hasInteracted = true;
                Interact();
            }
        }
    }

    public virtual void Interact() {
        Debug.Log(this.name + " has been interacted with.");
    }


    public void OnFocused (Transform playerTrans) {
        isFocus = true;
        player = playerTrans;
        hasInteracted = false;
    }

    public void OnDefocus () {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
