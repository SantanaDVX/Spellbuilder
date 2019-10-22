using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellController : MonoBehaviour {

    public List<SpellStats> spellList;
    List<float> spellListCooldowns;
    int spellListIndex = 0;
    public Transform spellCastOrigin;
    Camera cam;
    PlayerController playerController;
    PlayerStats stats;
    PlayerMotor motor;

    List<Vector3> debugs;

    private void Start() {
        cam = Camera.main;
        playerController = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStats>();
        motor = GetComponent<PlayerMotor>();
        spellListCooldowns = new List<float>();
        for (int i = 0; i < spellList.Count; i++) {
            spellListCooldowns.Add(0f);
        }

        debugs = new List<Vector3>();
    }

    private void Update() {
        for (int i = 0; i < spellListCooldowns.Count; i++) {
            spellListCooldowns[i] = spellListCooldowns[i] - Time.deltaTime;
        }
    }

    public IEnumerator castCurrentSpell() {
        SpellStats spell = spellList[spellListIndex];
        if (stats.currentMana < spell.manaCost) {
            Debug.Log("Not enough mana to cast " + spell.name);
        } else if (spellListCooldowns[spellListIndex] > 0) {
            Debug.Log(spell.name + " is on cooldown");
        } else {       

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //Initialise the enter variable
            float enter = 0.0f;

            new Plane(Vector3.up, spellCastOrigin.position.y).Raycast(ray, out enter);
                //Get the point that is clicked
            Vector3 mouseWorld = ray.GetPoint(enter);
            mouseWorld = new Vector3(mouseWorld.x,-1 * mouseWorld.y, mouseWorld.z);
            debugs.Add(mouseWorld);


            motor.CastTowardTarget(mouseWorld);
            yield return new WaitForSeconds(spell.castTime);

            Debug.Log("Cast " + spell.name);
            GameObject spellGO = Instantiate(spell.prefab, spellCastOrigin.position, transform.rotation);
            
            stats.SpendMana(spell.manaCost);
            spellListCooldowns[spellListIndex] = spell.cooldown;
            playerController.UnlockActions();
            /*
             * Check if interrupted (stunned?)
            } else {
                Debug.Log("About to cast spell " + spell.name + ", but was interrupted");
            }
            */
        }
       
    }

    /*
    private void OnDrawGizmosSelected() {
        foreach(Vector3 debug in debugs) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(debug, 1);
        }
    }
    */
}

