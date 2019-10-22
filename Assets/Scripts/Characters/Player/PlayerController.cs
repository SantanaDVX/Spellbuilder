using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(SpellController))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    SpellController spellController;
    public PlayerStats stats;
    [SerializeField]
    bool actionsLocked = false;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;        
        motor = GetComponent<PlayerMotor>();
        spellController = GetComponent<SpellController>();
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update() {
        if (!GetActionsLocked()) {
            if (Input.GetMouseButton(0)) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null) {
                        SetFocus(interactable);
                    } else {
                        RemoveFocus();
                        motor.MoveToPoint(hit.point);
                    }
                }
            }     
            if (Input.GetMouseButtonDown(1)) {
                StartCoroutine(spellController.castCurrentSpell());
            }
        }
    }

    public bool GetActionsLocked() {
        return actionsLocked;
    }

    public void LockActions() {
        actionsLocked = true;
    }

    public void UnlockActions() {
        actionsLocked = false;
    }

    void SetFocus(Interactable interactable) {
        if (focus != interactable) {
            if (focus != null) {
                focus.OnDefocus();
            }
            focus = interactable;
            focus.OnFocused(transform);
            motor.FollowTarget(focus);
        }
    }

    void RemoveFocus() {
        if (focus != null) {
            focus.OnDefocus();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
